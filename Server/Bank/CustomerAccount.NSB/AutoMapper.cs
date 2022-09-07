using AutoMapper;
using CustomerAccount.Service.Models;
using NSB.Command;

namespace CustomerAccount.NSB;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<UpdateAccount, UpdateBalanceModel>();
   

    }
}
