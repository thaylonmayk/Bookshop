namespace Bookshop.Domain.Entities
{
    public class LivroAutorEntity
    {
        public int CodLivro { get; set; }
        public int CodAutor { get; set; }
        public LivroEntity Livro { get; set; }
        public AutorEntity Autor { get; set; }
    }
}
