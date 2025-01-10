namespace Bookshop.Domain.DTOs.Requests
{
    public class CanalPrecoRequest
    {
        public int Cod { get; set; }
        public int CodLivro { get; set; }
        public int CodCanal { get; set; }
        public decimal Valor { get; set; }
    }
}
