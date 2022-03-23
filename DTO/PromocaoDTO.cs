using System.ComponentModel.DataAnnotations;

namespace bluemarket.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome da promoção é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome da promoção deve ter menos que 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Nome de promoção muito pequeno, tente um nome maior que 2 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A categoria do produto é obrigatória.")]
        public int Produto { get; set; }
        [Required(ErrorMessage = "A porcentagem de desconto do produto é obrigatória.")]
        [Range(0, 100)]
        public float Porcentagem { get; set; }
    }
}