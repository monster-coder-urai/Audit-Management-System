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
    public class ViewTransactionsByIdUseCase : IViewTransactionsByIdUseCase
    {
        private readonly ITransactionRepository transactionRepository;

        public ViewTransactionsByIdUseCase(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public async Task<BankTransaction> ExecuteAsync(int transactionId)
        {
            return await this.transactionRepository.GetTransactionByIdAsync(transactionId);
        }
    }
}
