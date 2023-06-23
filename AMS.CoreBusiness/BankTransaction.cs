using System.ComponentModel.DataAnnotations;

namespace AMS.CoreBusiness
{
    public class BankTransaction
    {
        public int TransactionId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public string BranchName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Amount must be greater or equal to 0")]
        public int Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}