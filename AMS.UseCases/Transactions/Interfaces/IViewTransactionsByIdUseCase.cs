using AMS.CoreBusiness;
using System.Transactions;

namespace AMS.UseCases.Transactions.Interfaces
{
    public interface IViewTransactionsByIdUseCase
    {
        Task<BankTransaction> ExecuteAsync(int transactionId);
    }
}