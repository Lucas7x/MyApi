using MyApi.Models;

namespace MyApi.DTOs
{
    public class DebtInsDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        public DebtInDebtorDTO Debtor { get; set; }
        public DebtInWalletDTO? Wallet { get; set; }
    }
}
