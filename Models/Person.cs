namespace MyApi.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsActive { get; set; }

        public List<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
