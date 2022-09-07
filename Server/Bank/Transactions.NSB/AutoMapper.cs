using AutoMapper;
using NSB.Event;
using Transaction.Service.Models;

namespace Transaction.NSB;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AccountUpdated, TransactionModel>();
   

    }
}
