using System.Security.Claims;
using WalletApi.Models.DTOs;
using WalletApi.Models.Entities;

namespace WalletApi.Services.Interfaces
{
    public interface IWalletService
    {

        Task<FundWalletResult> FundWallet(string id, FundWalletDTO model, ClaimsPrincipal loggedInUser);

        Task<WithdrawFundResult> WithdrawFund(string id, WithdrawFundDTO model, ClaimsPrincipal loggedInUser);

        Dictionary<string, decimal> LatestConversions { get; }
        decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency, Dictionary<string, decimal> currencyExchangeRates);
    }    
}