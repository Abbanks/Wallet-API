using Microsoft.AspNetCore.Mvc;
using WalletApi.Models.DTOs;
using WalletApi.Services.Interfaces;

namespace WalletApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> SignUp(RegisterDTO model)
        {
            try
            {
                var result = await _authService.SignUp(model);
                return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);    
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            try
            {
                var loginResult = await _authService.Login(model);  
                return Ok(loginResult);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
