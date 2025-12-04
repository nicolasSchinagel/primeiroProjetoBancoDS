using System.ComponentModel.DataAnnotations;

namespace projetoBancoDS.Models
{
    public class LoginPF
    {
        [Required]
        [Display(Name = "Nome do Cliente")]
        [StringLength(200)]
        public string? NomeCliente { get; set; }

        [Required]
        public long CPF { get; set; }
    }
}
