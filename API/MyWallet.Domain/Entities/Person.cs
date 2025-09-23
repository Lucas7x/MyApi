namespace MyWallet.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Password { get; set; }

        public List<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
