using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WalletApi.Helpers;
using WalletApi.Models.DTOs;
using WalletApi.Models.Entities;
using WalletApi.Services.Interfaces;

namespace WalletApi.Services
{
    public class AdminService(UserManager<AppUser> userManager, IRepositoryService repositoryService, IWalletService walletService) : IAdminService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IRepositoryService _repositoryService = repositoryService;
        private readonly IWalletService _walletService = walletService;
        public async Task<bool> PromoteOrDemoteUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains("admin"))
            {
                return false;
            }

            if (roleName.ToLower() == "admin")
            {
                if (userRoles.Contains("noob") || userRoles.Contains("elite"))
                {
                    return false;
                }

            }   

            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!removeResult.Succeeded)
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, roleName.ToLower());

            return result.Succeeded;
        }

        public async Task<FundWalletResult> FundUserWallet(string id, FundWalletDTO model)
        {
            var response = new FundWalletResult();

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    response.Message = $"No record found for user with id: {id}";
                    return response;
                }

                // Check if currency is valid
                if (!UtilityMethods.IsValidCurrencyCode(model.Currency))
                {
                    response.Message = "Invalid currency code!";
                    return response;
                }

                
                var userRoles = await _userManager.GetRolesAsync(user);
                var currencyExchangeRates = _walletService.LatestConversions;

                if (userRoles.Contains("noob"))
                {
                    var mainCurrency = user.MainCurrency;

                    var wallet = await _repositoryService.GetWalletByUserIdAndCurrencyAsync(user.Id, mainCurrency);

                    // Check if the transaction currency is different from the user's main currency
                    if (model.Currency != mainCurrency)
                    {
                        // Get the latest conversion rates


                        var convertedAmount = _walletService.ConvertCurrency(model.Amount, model.Currency, user.MainCurrency, currencyExchangeRates);

                        model.Amount = convertedAmount;
                    }


                    wallet.Balance += model.Amount;

                    // Update wallet balance
                    var updatedWallet = await _repositoryService.UpdateAsync(wallet);

                    if (!updatedWallet)
                    {
                        response.Message = "An error occurred while updating the wallet";
                        return response;
                    }

                    // Update user's total balance
                    user.TotalBalance += model.Amount;
                    await _userManager.UpdateAsync(user);

                    response.Message = $"Wallet funded with {mainCurrency} {model.Amount} successfully for user with id: {id}";
                    response.IsSuccess = true;
                }

                else if (userRoles.Contains("elite"))
                {

                    //var wallet = await _repositoryService.GetByCurrencyAsync(model.Currency);
                    var wallet = await _repositoryService.GetWalletByUserIdAndCurrencyAsync(user.Id, model.Currency);

                    if (wallet == null)
                    {

                        var newWallet = new Wallet { AppUserId = user.Id, Currency = model.Currency, Balance = model.Amount };
                        await _repositoryService.AddAsync(newWallet);

                        var convertedAmount = _walletService.ConvertCurrency(model.Amount, model.Currency, user.MainCurrency, currencyExchangeRates);


                        user.TotalBalance += convertedAmount; // Update total balance

                        await _userManager.UpdateAsync(user);

                        response.Message = $"New Wallet funded with {model.Currency} {model.Amount} successfully for user with id: {id}";
                        response.IsSuccess = true;
                    }
                    else
                    {
                        wallet.Balance += model.Amount;
                        var walletBalance = await _repositoryService.UpdateAsync(wallet);

                        if (!walletBalance)
                        {
                            response.Message = "An error occurred while updating the wallet";
                            return response;
                        }

                        user.TotalBalance += model.Amount; // Update total balance
                        await _userManager.UpdateAsync(user);

                        response.Message = $"Wallet funded with {model.Currency} {model.Amount} successfully for user with id: {id}";
                        response.IsSuccess = true;
                    }


                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
