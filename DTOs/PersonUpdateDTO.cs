using System.ComponentModel.DataAnnotations;

namespace MyApi.DTOs
{
    public class PersonUpdateDTO
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
