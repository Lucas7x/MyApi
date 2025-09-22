using System.ComponentModel.DataAnnotations;

namespace MyWallet.Application.DTOs
{
    public class PersonCreateDTO
    {
        /// <summary>
        /// Nome completo do usuário.
        /// </summary>
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Nome precisa ter entre 3 e 100 dígitos.")]
        public string Name { get; set; }

        /// </summary>
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string? Email { get; set; }

    }
}
