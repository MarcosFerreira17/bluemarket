using System.ComponentModel.DataAnnotations;
namespace bluemarket.DTO
{
    public class FornecedorDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome de fornecedor é obrigatório.")]
        [StringLength(50, ErrorMessage = "Excedeu o tamanho de nome, tente um nome menor.")]
        [MinLength(3, ErrorMessage = "Nome de categoria muito pequeno, tente um nome maior que 2 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [Phone(ErrorMessage = "Numero de telefone inválido.")]
        public string Telefone { get; set; }

    }
}