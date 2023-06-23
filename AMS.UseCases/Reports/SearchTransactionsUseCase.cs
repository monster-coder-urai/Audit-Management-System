using AMS.CoreBusiness;
using AMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AMS.UseCases.Reports
{
    public class SearchTransactionsUseCase : ISearchTransactionsUseCase
    {
        private readonly ITransactionRepository transactionRepository;

        public SearchTransactionsUseCase(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<BankTransaction>> ExecuteAsync(
            string BranchName,
            DateTime? dateFrom,
            DateTime? dateTo
            )
        {
            if (dateTo.HasValue) { dateTo = dateTo.Value.AddDays(1); }
            return await this.transactionRepository.GetTransactionsAsync(
                BranchName, dateFrom, dateTo);
        }

    }
}
