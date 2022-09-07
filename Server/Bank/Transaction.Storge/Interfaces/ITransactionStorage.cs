

namespace Transaction.Storage.Interfaces;

public interface ITransactionStorage
{
    public Task<Storage.Entites.Transaction> createTransaction(Entites.Transaction transaction);

}
