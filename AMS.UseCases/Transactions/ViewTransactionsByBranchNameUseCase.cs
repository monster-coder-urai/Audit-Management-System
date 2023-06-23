using AMS.CoreBusiness;
using AMS.UseCases.PluginInterfaces;
using AMS.UseCases.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AMS.UseCases.Transactions
{
    public class ViewTransactionsByBranchNameUseCase : IViewTransactionsByBranchNameUseCase
    {
        private readonly ITransactionRepository transactionRepository;

        public ViewTransactionsByBranchNameUseCase(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<BankTransaction>> ExecuteAsync(string BranchName = "")
        {
            return await transactionRepository.GetTransactionsByBranchNameAsync(BranchName);
        }
    }
}
