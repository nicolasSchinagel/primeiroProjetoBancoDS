using System.ComponentModel.DataAnnotations;

namespace projetoBancoDS.Models
{
    public abstract class Cliente
    {
        [Required]
        [Display(Name = "Id do Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Nome do Cliente")]
        [StringLength(200)]
        public string? NomeCliente { get; set; }

        [Required]
        [StringLength(9)]
        [Display(Name = "CEP do endereço")]
        public string? CEP { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Logradouro do Endereço")]
        public string? Logradouro { get; set; }

        [Required]
        [Display(Name = "Número do Endereço")]
        public short Numero { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "País do Endereço")]
        public string? Pais { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Estado do Endereço")]
        public string? Estado { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Cidade do Endereço")]
        public string? Cidade { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Bairro do Endereço")]
        public string? Bairro { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Número de telefone")]
        public string? NumeroTel { get; set; }
    }
    public class ClientePF : Cliente
    {
        [Required]
        public long CPF { get; set; }
    }
    public class ClientePJ : Cliente
    {
        [Required]
        public long CNPJ { get; set; }

    }
}
