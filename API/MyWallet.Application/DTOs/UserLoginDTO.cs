using System.ComponentModel.DataAnnotations;

namespace MyWallet.Application.DTOs
{
    public class UserLoginDTO
    {
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Required(ErrorMessage = "É necessário informar um e-mail.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É necessário informar uma senha.")]
        public string Password { get; set; }
    }
}
