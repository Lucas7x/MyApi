namespace MyWallet.Domain.Entities
{
    public class DebtIn
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        public int DebtorId { get; set; }
        public Person Debtor { get; set; }

        public int? WalletId { get; set; }
        public Wallet? Wallet { get; set; }

        public void Pay(Wallet wallet)
        {
            if (PaidAt != null)
                throw new InvalidOperationException("Esta dívida já foi paga");

            this.PaidAt = DateTime.Now;
            this.Wallet = wallet;
            this.WalletId = wallet?.Id;
            wallet?.AddToBalance(this.Amount);
        }
    }
}
