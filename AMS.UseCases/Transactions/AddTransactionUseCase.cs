﻿using AMS.CoreBusiness;
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
    public class AddTransactionUseCase : IAddTransactionUseCase
    {
        private readonly ITransactionRepository transactionRepository;

        public AddTransactionUseCase(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public async Task ExecuteAsync(BankTransaction transaction)
        {
            await this.transactionRepository.AddTransactionAsync(transaction);
        }
    }
}
