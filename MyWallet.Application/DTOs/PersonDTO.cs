namespace MyWallet.Application.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsActive { get; set; }
        public List<PersonWalletDTO> Wallets { get; set; }
    }
}
