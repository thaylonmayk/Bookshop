namespace Bookshop.Domain.Entities
{
    public class CanalPrecoEntity : BaseEntity
    {
        public int CodLivro { get; set; }
        public int CodCanal { get; set; }
        public decimal Valor { get; set; }
        public LivroEntity Livros { get; set; }
        public CanalEntity Canais { get; set; }
    }
}
