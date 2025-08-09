namespace MyApi.DTOs
{
    public class GetPersonWalletDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Balance { get; set; }
        public double Income { get; set; }
    }
}
