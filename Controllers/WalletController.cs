using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalletApi.Models.DTOs;
using WalletApi.Models.Entities;
using WalletApi.Services;
using WalletApi.Services.Interfaces;

namespace WalletApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "noob, elite")]
    public class WalletController(IWalletService walletService) : ControllerBase
    {
        private readonly IWalletService _walletService = walletService;
            
        [HttpPut("fund-wallet/{id}")]
       
        public async Task<IActionResult> FundWallet(string id, [FromBody] FundWalletDTO model)
        {
            // Call the service method to fund the wallet
            var response = await _walletService.FundWallet(id, model, User);

            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpPut("withdraw-fund/{id}")]
        public async Task<IActionResult> WithdrawFund(string id, [FromBody] WithdrawFundDTO model)
        {
            // Call the service method to fund the wallet
            var response = await _walletService.WithdrawFund(id, model, User);

            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }


    }



}

