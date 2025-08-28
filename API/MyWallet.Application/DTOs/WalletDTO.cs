namespace MyWallet.Application.DTOs
{
    public class WalletDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Balance { get; set; }
        public double Income { get; set; }
        public int? OwnerId { get; set; }
        public PersonDTO? Owner { get; set; }
    }
}
