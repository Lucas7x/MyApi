using System.ComponentModel.DataAnnotations;

namespace MyApi.Controllers.DTOs
{
    public class UpdateUserDTO 
    {
        /// <summary>
        /// Nome completo do usuário.
        /// </summary>
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        /// <summary>
        /// Endereço de e-mail único.
        /// </summary>
        [EmailAddress]
        public string? Email { get; set; }
    }
}
