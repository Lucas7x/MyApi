using MyApi.Models;

namespace MyApi.Controllers.DTOs
{
    public class GetWalletDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Balance { get; set; }
        public double Income { get; set; }
        public int? OwnerId { get; set; }
        public GetPersonDTO? Owner { get; set; }
    }
}
