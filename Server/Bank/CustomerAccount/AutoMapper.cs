using AutoMapper;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.WebApi.DTO;

namespace CustomerAccount.WebAp;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<CustomerDTO, CustomerModel>();
        CreateMap<CustomerModel, Customer>();
        CreateMap<AccountModel, Account>();
        CreateMap<LoginDTO, LoginModel>();
        CreateMap<Account, AccountCustomerInfoModel>().
        ForMember(des => des.FirstName, opts => opts
                        .MapFrom(src => src.Customer.FirstName))
               .ForMember(des => des.LastName, opts => opts
                        .MapFrom(src => src.Customer.LastName))
               .ForMember(des => des.Email, opts => opts
                        .MapFrom(src => src.Customer.Email));
        CreateMap<AccountCustomerInfoModel, AccountCustomerInfoDTO>();

    }
}
