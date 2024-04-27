using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WalletApi.Models.DTOs;

namespace WalletApi.Services.Interfaces
{
    public interface ICurrencyApiService
    {
        Task<ApiResponse<T>> MakeRequestAsync<T>(ApiRequest request);
    }
}
