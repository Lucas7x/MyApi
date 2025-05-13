using MyApi.Models;

namespace MyApi.Controllers.DTOs
{
    public class GetDebtInsDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        public GetDebtInDebtorDTO Debtor { get; set; }
        public GetDebtInWalletDTO? Wallet { get; set; }
    }
}
