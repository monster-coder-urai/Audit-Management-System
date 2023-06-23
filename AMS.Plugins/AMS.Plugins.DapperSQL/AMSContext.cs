using AMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace AMS.Plugins.DapperSQL
{
    public class AMSContext : DbContext
    {
        public AMSContext(DbContextOptions<AMSContext> options) : base(options)
        {

        }
        public DbSet<BankTransaction> transactions { get; set; }
        public DbSet<BranchRecord> branchRecords { get; set; }
        public DbSet<YearRecord> yearRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankTransaction>()
                .HasKey(pi => new { pi.TransactionId });
            modelBuilder.Entity<BranchRecord>()
                .HasKey(pi => new { pi.BranchId });
            modelBuilder.Entity<YearRecord>()
                .HasKey(pi => new { pi.Year });
            modelBuilder.Entity<BankTransaction>().HasData(
                new BankTransaction { TransactionId = 1, BranchId = 1, BranchName = "City1", Amount = 1000, Date = new DateTime(2023, 1, 1) },
                new BankTransaction { TransactionId = 2, BranchId = 2, BranchName = "City2", Amount = 2000, Date = new DateTime(2023, 2, 2) },
                new BankTransaction { TransactionId = 3, BranchId = 3, BranchName = "City3", Amount = 3000, Date = new DateTime(2023, 3, 3) },
                new BankTransaction { TransactionId = 4, BranchId = 4, BranchName = "City4", Amount = 4000, Date = new DateTime(2023, 4, 4) }
                );

            modelBuilder.Entity<BranchRecord>().HasData(
                new BranchRecord { BranchId = 1, EditLimit = 1 },
                new BranchRecord { BranchId = 2, EditLimit = 1 },
                new BranchRecord { BranchId = 3, EditLimit = 1 },
                new BranchRecord { BranchId = 4, EditLimit = 1 }
                );

            modelBuilder.Entity<YearRecord>().HasData(
                new YearRecord { Year = 2023, EditLimit = 3 },
                new YearRecord { Year = 2024, EditLimit = 3 }
                );

        }
    }
}