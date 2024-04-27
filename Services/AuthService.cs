using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalletApi.Helpers;
using WalletApi.Models.DTOs;
using WalletApi.Models.Entities;
using WalletApi.Services.Interfaces;

namespace WalletApi.Services
{
    public class AuthService(IConfiguration config, UserManager<AppUser> userManager,
        IMapper mapper, IRepositoryService repositoryService) : IAuthService
    {
        private readonly IConfiguration _config = config;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly IRepositoryService _repositoryService = repositoryService; 

        public string GenerateJWT(AppUser user, List<string> roles, List<Claim> claims)
        {
            var myClaims = new List<Claim>();

            myClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            myClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            myClaims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                myClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            foreach (var claim in claims)
            {
                myClaims.Add(new Claim(claim.Type, claim.Value));
            }
            var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: myClaims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public async Task<LoginResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);
                return new LoginResult
                {
                    IsSuccess = true,
                    Message = "Login successful",
                    Token = GenerateJWT(user, roles.ToList(), claims.ToList()),
                    UserId = user.Id,
                    Roles = roles.ToList()
                };

            }

            return new LoginResult
            {
                IsSuccess = false,
                Message = "Invalid login details"
            };
        }

        public async Task<RegisterResult> SignUp(RegisterDTO model)
        {
            var response = new RegisterResult();

            try
            {
                //Check if email already exists
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    response.Message = "Email already exists!";
                    return response;
                }

                // Check if main currency is valid
                if (!UtilityMethods.IsValidCurrencyCode(model.MainCurrency))
                {
                    response.Message = "Invalid currency code!";
                    return response;
                }

                var appUser = _mapper.Map<AppUser>(model);
                var addResult = await _userManager.CreateAsync(appUser, model.Password);

                if (!addResult.Succeeded)
                {
                    var errList = new StringBuilder();
                    foreach (var err in addResult.Errors)
                    {
                        errList.AppendLine(err.Description);
                    }

                    response.Message = errList.ToString();
                    return response;
                }

                //Add role to user
                await _userManager.AddToRoleAsync(appUser, model.UserType.ToLower());
  
                // Create wallet for user
                var newWallet = new Wallet { AppUserId = appUser.Id, Currency = model.MainCurrency, Balance = 0 };
                await _repositoryService.AddAsync(newWallet);
         
                response.Message = $"User added with Id: {appUser.Id}";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Dictionary<string, string>> ValidateLoggedInUser(ClaimsPrincipal userClaims, string userId)
        {
            var loggedInUser = await _userManager.GetUserAsync(userClaims);

            if (loggedInUser == null || loggedInUser.Id != userId)
            {
                return new Dictionary<string, string>
                {
                    {"Code", "400"},
                    {"Message", "Access denied! Id provided does not match loggedIn user." }
                };
            }

            return new Dictionary<string, string>
            {
                { "Code", "200" },
                { "Message", "Ok" }
            };
        }
    }
}
