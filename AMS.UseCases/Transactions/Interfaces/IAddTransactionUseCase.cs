using AMS.CoreBusiness;
using System.Transactions;

namespace AMS.UseCases.Transactions.Interfaces
{
    public interface IAddTransactionUseCase
    {
        Task ExecuteAsync(BankTransaction transaction);
    }
}