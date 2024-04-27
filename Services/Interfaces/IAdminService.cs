using System.Security.Claims;
using WalletApi.Models.DTOs;

namespace WalletApi.Services.Interfaces
{
    public interface IAdminService
    {
        Task<bool> PromoteOrDemoteUser(string userId, string roleName);

        Task<FundWalletResult> FundUserWallet(string id, FundWalletDTO model);
    }
}
