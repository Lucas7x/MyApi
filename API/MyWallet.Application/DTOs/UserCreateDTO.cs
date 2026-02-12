using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWallet.Application.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "É necessário informar um nome.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }
        
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        public string Phone { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "É necessário informar uma senha.")]
        [MinLength(4, ErrorMessage = "A senha deve ter no mínimo 4 caracteres.")]
        [MaxLength(100, ErrorMessage = "A senha deve ter no máximo 100 caracteres.")]
        public string Password { get; set; }
    }
}
