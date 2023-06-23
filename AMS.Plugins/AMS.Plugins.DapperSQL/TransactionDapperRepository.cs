using AMS.CoreBusiness;
using AMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace AMS.Plugins.DapperSQL
{
    public class TransactionDapperRepository : ITransactionRepository
    {

        private readonly IDbContextFactory<AMSContext> contextFactory;

        public TransactionDapperRepository(IDbContextFactory<AMSContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task AddTransactionAsync(BankTransaction transaction)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.transactions.Add(transaction);
            await db.SaveChangesAsync();
        }

        public async Task<BankTransaction> GetTransactionByIdAsync(int transactionId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var tran = await db.transactions.FindAsync(transactionId);
            if (tran != null) return tran;

            return new BankTransaction();
        }

        public async Task<IEnumerable<BankTransaction>> GetTransactionsAsync(string branchName, DateTime? dateFrom, DateTime? dateTo)
        {
            using var db = this.contextFactory.CreateDbContext();
            var query = from tran in db.transactions
                        where
                            (string.IsNullOrWhiteSpace(branchName) || tran.BranchName.ToLower().IndexOf(branchName.ToLower()) >= 0)
                            &&
                            (!dateFrom.HasValue || tran.Date >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || tran.Date <= dateTo.Value.Date)
                        select tran;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<BankTransaction>> GetTransactionsByBranchNameAsync(string branchName)
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.transactions.Where(x => x.BranchName.ToLower().IndexOf(branchName.ToLower()) >= 0).ToListAsync();
        }

        public async Task UpdateTransactionAsync(BankTransaction transaction)
        {
            using var db = this.contextFactory.CreateDbContext();
            var tran = await db.transactions.FindAsync(transaction.TransactionId);
            if (tran != null)
            {
                tran.BranchId = transaction.BranchId;
                tran.BranchName = transaction.BranchName;
                tran.Date = transaction.Date;
                tran.Amount = transaction.Amount;

                await db.SaveChangesAsync();

            }
        }
    }
}