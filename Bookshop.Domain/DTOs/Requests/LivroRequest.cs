namespace Bookshop.Domain.DTOs.Requests
{
    public class LivroRequest
    {
        public int Cod { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string AnoPublicacao { get; set; }
        public int Edicao { get; set; }
        public string Sinopse { get; set; }
        public decimal Valor { get; set; }
        public List<int> Autores { get; set; }
        public List<int> Assuntos { get; set; }
    }
}
