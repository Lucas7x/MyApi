namespace MyWallet.Application.DTOs
{
    public class DebtInCreateDTO
    {
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime? PaidAt { get; set; }
        public int DetborId { get; set; }
        public int? WalletId { get; set; }
    }
}
