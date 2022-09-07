

namespace Transaction.Storage.Interfaces;

public interface ITransactionStorage
{
    public Task<Entites.Transaction> createTransaction(Entites.Transaction transaction);
    public Task<bool> updateTransaction(Entites.Transaction transaction);

}
