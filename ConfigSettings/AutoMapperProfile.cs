using AutoMapper;
using WalletApi.Models.DTOs;
using WalletApi.Models.Entities;

namespace WalletApi.ConfigSettings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO, AppUser>().ReverseMap();
            CreateMap<FundWalletDTO, Wallet>().ReverseMap();    
        }   
    }
}
