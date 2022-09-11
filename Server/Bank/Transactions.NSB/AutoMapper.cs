using AutoMapper;
using NSB.Event;
using Transaction.Service.Models;
using Transaction.Storage.Entites;

namespace Transaction.NSB;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<UpdateTransactionModel, UpdateTransactionStatus>();
        CreateMap<AccountUpdated, UpdateTransactionModel>();
    }
}
