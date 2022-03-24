using System.ComponentModel.DataAnnotations;
namespace bluemarket.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome do produto tem que tem menos que 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Nome do produto muito pequeno, tente um nome maior que 2 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O categoria do produto é obrigatório.")]
        public int Categoria { get; set; }
        [Required(ErrorMessage = "O fornecedor do produto é obrigatório.")]
        public int Fornecedor { get; set; }
        [Required(ErrorMessage = "O preço de custo do produto é obrigatório.")]
        public float PrecoDeCusto { get; set; }
        [Required(ErrorMessage = "O preço de venda do produto é obrigatório.")]
        public string PrecoDeCustoString { get; set; }
        [Required(ErrorMessage = "O preço de venda do produto é obrigatório.")]
        public float PrecoDeVenda { get; set; }
        [Required(ErrorMessage = "O preço de venda do produto é obrigatório.")]
        public string PrecoDeVendaString { get; set; }
        [Required(ErrorMessage = "Medição do produto é obrigatória.")]
        [Range(0, 2, ErrorMessage = "Medição inválida.")]
        public int Medicao { get; set; }

    }
}

