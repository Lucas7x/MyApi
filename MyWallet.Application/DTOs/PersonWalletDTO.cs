namespace MyWallet.Application.DTOs
{
    public class PersonWalletDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Balance { get; set; }
        public double Income { get; set; }
    }
}
