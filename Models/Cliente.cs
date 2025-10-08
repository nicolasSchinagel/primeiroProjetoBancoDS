using System.ComponentModel.DataAnnotations;

namespace projetoBancoDS.Models
{
    public abstract class Cliente
    {
        [Required]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(200)]
        public string? NomeCliente { get; set; }

        [Required]
        [StringLength(9)]
        public string? CEP { get; set; }

        [Required]
        public string? Logradouro { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public string? Pais { get; set; }

        [Required]
        public string? Estado { get; set; }

        [Required]
        public string? Cidade { get; set; }

        [Required]
        public string? Bairro { get; set; }

        [Required]
        public string? NumeroTel { get; set; }
    }
    public class ClientePF : Cliente
    {
        [Required]
        public int CPF { get; set; }
    }
    public class ClientePJ : Cliente
    {
        [Required]
        public int CNPJ { get; set; }
    }
}
