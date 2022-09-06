

namespace Transaction.Storage.Interfaces;

public interface ITransactionStorage
{
    public Task<bool> createTransaction(Entites.Transaction transaction);

}
