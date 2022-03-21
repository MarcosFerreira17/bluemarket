using System.ComponentModel.DataAnnotations;
namespace bluemarket.DTO
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome de categoria é obrigatório.")]
        [StringLength(50, ErrorMessage = "Excedeu o tamanho de nome, tente um nome menor.")]
        [MinLength(3, ErrorMessage = "Nome de categoria muito pequeno, tente um nome maior que 2 caracteres.")]
        public string Nome { get; set; }
        // [Required]
        // public bool Status { get; set; }

    }
}