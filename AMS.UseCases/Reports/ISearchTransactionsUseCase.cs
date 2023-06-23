using AMS.CoreBusiness;

namespace AMS.UseCases.Reports
{
    public interface ISearchTransactionsUseCase
    {
        Task<IEnumerable<BankTransaction>> ExecuteAsync(string BranchName, DateTime? dateFrom, DateTime? dateTo);
    }
}