using System.ComponentModel.DataAnnotations;
namespace bluemarket.DTO
{
    public class VendaDTO
    {
        public float Total;
        public float Troco;
        public SaidaDTO[] Produtos;
    }
}