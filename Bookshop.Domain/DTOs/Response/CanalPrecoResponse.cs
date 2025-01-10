namespace Bookshop.Domain.DTOs.Response
{
    public class CanalPrecoResponse
    {
        public int Cod { get; set; }
        public int CodLivro { get; set; }
        public int CodCanal { get; set; }
        public decimal Valor { get; set; }
        public LivroResponse Livro { get; set; }
        public CanalResponse Canal { get; set; }
    }
}
