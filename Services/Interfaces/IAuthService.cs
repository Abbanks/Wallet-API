 using System.Security.Claims;
using WalletApi.Models.DTOs;
using WalletApi.Models.Entities;

namespace WalletApi.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJWT(AppUser user, List<string> roles, List<Claim> claims);

        Task<RegisterResult> SignUp(RegisterDTO model);  

        Task<LoginResult> Login(LoginDTO model);

        Task<Dictionary<string, string>> ValidateLoggedInUser(ClaimsPrincipal userClaims, string userId);
    }
}
 