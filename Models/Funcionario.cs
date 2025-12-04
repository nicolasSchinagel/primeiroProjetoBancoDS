using System.ComponentModel.DataAnnotations;
namespace projetoBancoDS.Models
{
    // O funcionário dentro dessa classe terá objetivo apenas para login, como cadastramos ele dentro do banco como norma, apenas dados para login são necessários
    public class Funcionario
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Nome do Funcionário")]
        public string? NomeFuncionario { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(200)]
        [Display(Name = "Senha do Funcionário")]
        public string? SenhaFuncionario { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(250)]
        [Display(Name = "Email")]
        public string? EmailFuncionario { get; set; }
    }
}
