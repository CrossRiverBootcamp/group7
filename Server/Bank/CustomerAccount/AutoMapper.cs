using AutoMapper;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.WebApi.DTO;
using NSB.Command;

namespace CustomerAccount.WebAp;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<CustomerDTO, CustomerModel>();
        CreateMap<LoginDTO, LoginModel>();
        CreateMap<AccountCustomerInfoModel, AccountCustomerInfoDTO>();
        CreateMap<OperationHistoryModel, OperationHistoryDTO>();
        CreateMap<CustomerInfoModel, CustomerInfoDTO>();


        CreateMap<CustomerModel, Customer>();
        CreateMap<AccountModel, Account>();
        CreateMap<Account, AccountCustomerInfoModel>().
        ForMember(des => des.FirstName, opts => opts
                        .MapFrom(src => src.Customer.FirstName))
               .ForMember(des => des.LastName, opts => opts
                        .MapFrom(src => src.Customer.LastName))
               .ForMember(des => des.Email, opts => opts
                        .MapFrom(src => src.Customer.Email));
        CreateMap<UpdateAccount, UpdateBalanceModel>();
        CreateMap<OperationHistory, OperationHistoryModel>().
            ForMember(des => des.Amount, opts => opts
                    .MapFrom(src => src.TransactionAmount))
            .ForMember(des => des.Date, opts => opts
                    .MapFrom(src => src.OperationTime));
        CreateMap<Account, CustomerInfoModel>().
        ForMember(des => des.FirstName, opts => opts
                    .MapFrom(src => src.Customer.FirstName))
           .ForMember(des => des.LastName, opts => opts
                    .MapFrom(src => src.Customer.LastName))
           .ForMember(des => des.Email, opts => opts
                    .MapFrom(src => src.Customer.Email));
        CreateMap<EmailVerificationModel, EmailVerification>();
        CreateMap<CustomerModel, EmailVerificationModel>();

    }
}
