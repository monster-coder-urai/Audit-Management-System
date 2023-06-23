using AMS.CoreBusiness;
using System.Transactions;

namespace AMS.UseCases.Transactions.Interfaces
{
    public interface IEditTransactionUseCase
    {
        Task ExecuteAsync(BankTransaction transaction);
    }
}