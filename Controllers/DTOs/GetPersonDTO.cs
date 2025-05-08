namespace MyApi.Controllers.DTOs
{
    public class GetPersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsActive { get; set; }
        public List<GetPersonWalletDTO> Wallets { get; set; }
    }
}
