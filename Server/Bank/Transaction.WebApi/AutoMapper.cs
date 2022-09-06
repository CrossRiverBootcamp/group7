using AutoMapper;
using Transaction.Service.Models;
using Transaction.WebApi.DTO;

namespace Transaction.WebAp;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<TransactionDTO, TransactionModel>();
        CreateMap< TransactionModel , Storage.Entites.Transaction>();
    }
}
