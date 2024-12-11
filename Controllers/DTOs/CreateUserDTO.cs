using System.ComponentModel.DataAnnotations;

namespace MyApi.Controllers.DTOs
{
    public class CreateUserDTO
    {
        /// <summary>
        /// Nome completo do usuário.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Endereço de e-mail único.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }

        /// <summary>
        /// Situação atual do usuário.
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}
