using AMS.CoreBusiness;
using AMS.UseCases.PluginInterfaces;
using System.Transactions;
using System.Xml.Linq;

namespace AMS.Plugins.InMemory
{
    public class TransactionRepository : ITransactionRepository
    {
        private List<BankTransaction> _transactions;

        public TransactionRepository()
        {
            _transactions = new List<BankTransaction>()
            {
                new BankTransaction{TransactionId = 1, BranchId = 1, BranchName = "City1", Amount = 1000,  Date = new DateTime(2023,1,1)},
                new BankTransaction{TransactionId = 2, BranchId = 2, BranchName = "City2", Amount = 2000,  Date = new DateTime(2023,2,2)},
                new BankTransaction{TransactionId = 3, BranchId = 3, BranchName = "City3", Amount = 3000,  Date = new DateTime(2023,3,3)},
                new BankTransaction{TransactionId = 4, BranchId = 4, BranchName = "City4", Amount = 4000,  Date = new DateTime(2023,4,4)}
            };
        }

        public Task AddTransactionAsync(BankTransaction transaction)
        {
             var maxId = _transactions.Max(x => x.TransactionId);
            transaction.TransactionId = maxId + 1;

            _transactions.Add(transaction);

            return Task.CompletedTask;
        }

        public async Task<BankTransaction> GetTransactionByIdAsync(int transactionId)
        {
            var tran = _transactions.First(x => x.TransactionId == transactionId);
            var newtran = new BankTransaction
            {
                TransactionId = tran.TransactionId,
                BranchId = tran.BranchId,
                BranchName = tran.BranchName,
                Amount = tran.Amount,
                Date = tran.Date
            };

            return await Task.FromResult(newtran);
        }

        public async Task<IEnumerable<BankTransaction>> GetTransactionsAsync(string branchName, DateTime? dateFrom, DateTime? dateTo)
        {
            var transaction = (await GetTransactionsByBranchNameAsync(string.Empty)).ToList();

            var query = from tran in this._transactions
                        where
                            (string.IsNullOrWhiteSpace(branchName) || tran.BranchName.ToLower().IndexOf(branchName.ToLower()) >= 0)
                            &&
                            (!dateFrom.HasValue || tran.Date >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || tran.Date <= dateTo.Value.Date)
                        select new BankTransaction
                        {
                            TransactionId = tran.TransactionId,
                            BranchId = tran.BranchId,
                            BranchName = tran.BranchName,
                            Amount = tran.Amount,
                            Date = tran.Date
                        };

            return query;
        }

        public async Task<IEnumerable<BankTransaction>> GetTransactionsByBranchNameAsync(string branchName)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                return await Task.FromResult(_transactions);
            else
                return _transactions.Where(x => x.BranchName.Contains(branchName, StringComparison.OrdinalIgnoreCase));
        }

        public Task UpdateTransactionAsync(BankTransaction transaction)
        {
            if (_transactions.Any(x => x.TransactionId != transaction.TransactionId &&
              x.BranchName.Equals(transaction.BranchName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            var tran = _transactions.FirstOrDefault(x => x.TransactionId == transaction.TransactionId);
            if (tran != null)
            {
                tran.BranchId = transaction.BranchId;
                tran.BranchName = transaction.BranchName;
                tran.Amount = transaction.Amount;
                tran.Date = transaction.Date;
            }

            return Task.CompletedTask;
        }
    }
}