using AMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AMS.UseCases.PluginInterfaces
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(BankTransaction transaction);
        Task<BankTransaction> GetTransactionByIdAsync(int transactionId);
        Task<IEnumerable<BankTransaction>> GetTransactionsAsync(string branchName, DateTime? dateFrom, DateTime? dateTo);
        Task<IEnumerable<BankTransaction>> GetTransactionsByBranchNameAsync(string branchName);
        Task UpdateTransactionAsync(BankTransaction transaction);
    }
}
