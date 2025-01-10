namespace Bookshop.Domain.Entities
{
    public class LivroAssuntoEntity
    {
        public int CodLivro { get; set; }
        public int CodAssunto { get; set; }
        public LivroEntity Livro { get; set; }
        public AssuntoEntity Assunto { get; set; }
    }
}
