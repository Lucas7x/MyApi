using System.ComponentModel.DataAnnotations;

namespace MyWallet.Application.DTOs
{
    public class WalletCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Balance { get; set; }
        public double Income { get; set; }
        public int OwnerId { get; set; }
    }
}
