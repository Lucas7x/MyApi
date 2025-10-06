namespace MyWallet.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
