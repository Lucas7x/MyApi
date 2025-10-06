namespace MyWallet.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; } = "";

        public List<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
