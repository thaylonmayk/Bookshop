namespace Bookshop.Domain.Entities.View
{
    public class LivroAutorAssuntoView
    {
        public int AutorCod { get; set; }
        public string AutorNome { get; set; }
        public string AutorSobrenome { get; set; }
        public int LivroCod { get; set; }
        public string LivroTitulo { get; set; }
        public string LivroEditora { get; set; }
        public string LivroAnoPublicacao { get; set; }
        public int LivroEdicao { get; set; }
        public string LivroSinopse { get; set; }
        public decimal LivroValor { get; set; }
        public int AssuntoCod { get; set; }
        public string AssuntoDescricao { get; set; }
    }
}
