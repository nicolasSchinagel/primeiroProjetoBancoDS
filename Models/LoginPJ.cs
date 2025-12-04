using System.ComponentModel.DataAnnotations;

namespace projetoBancoDS.Models
{
    public class LoginPJ
    {
        [Required]
        [Display(Name = "Nome do Cliente")]
        [StringLength(200)]
        public string? NomeCliente { get; set; }

        [Required]
        public long CNPJ { get; set; }
    }
}
