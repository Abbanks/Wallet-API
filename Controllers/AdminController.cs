using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletApi.Models.DTOs;
using WalletApi.Services.Interfaces;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
         private readonly IAdminService _adminService = adminService;

        [HttpPut("promote-or-demote-user/{userId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PromoteOrDemoteUser(string userId, [FromBody] ChangeUserRoleDTO model)
        {
            var response = await _adminService.PromoteOrDemoteUser(userId, model.NewRole);

            return response ? Ok("User role updated successfully") : BadRequest("An error occurred");
        }

        [HttpPut("fund-user-wallet/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> FundUserWallet(string id, [FromBody] FundWalletDTO model)
        {
            var response = await _adminService.FundUserWallet(id, model);

            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

    }
}
