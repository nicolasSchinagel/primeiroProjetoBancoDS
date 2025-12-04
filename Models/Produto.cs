using System.ComponentModel.DataAnnotations;
namespace projetoBancoDS.Models
{
    public class Produto
    {
        [Required]
        [Display (Name = "Id do Produto")]
        public int IdProduto { get; set; }

        [Required]
        [StringLength (200)]
        [Display (Name = "Nome do Produto")]
        public string? NomeProduto { get; set; }

        [Required]
        [StringLength (50)]
        [Display(Name = "Tipo de Pedra")]

        public string? TipoProduto { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Material do Produto")]
        public string? MaterialProduto { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Marca do Produto")]
        public string? MarcaProduto { get; set; }

        [Required]
        [Display(Name = "Preço do Produto")]
        public decimal PrecoProduto { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Categoria do Produto")]
        public string? CategoriaProduto { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tamanho do Produto")]

        public string? TamanhoProduto { get; set; }

        [Required]
        [Display(Name = "Peso do Produto")]
        public decimal PesoProduto { get; set; }

        [Required]
        [Display(Name = "Quantidade do Produto")]
        public int QtdProduto{ get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Status do Produto")]
        public string? StatusProduto { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Fornecedor do Produto")]
        public string? NomeFornecedor { get; set; }
    }
}
