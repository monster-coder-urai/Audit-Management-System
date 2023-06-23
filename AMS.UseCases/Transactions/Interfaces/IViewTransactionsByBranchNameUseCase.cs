using AMS.CoreBusiness;

namespace AMS.UseCases.Transactions.Interfaces
{
    public interface IViewTransactionsByBranchNameUseCase
    {
        Task<IEnumerable<BankTransaction>> ExecuteAsync(string BranchName = "");
    }
}