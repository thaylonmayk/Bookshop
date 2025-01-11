namespace Bookshop.Domain.Entities
{
    public class LivroEntity : BaseEntity
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string AnoPublicacao { get; set; }
        public int Edicao { get; set; }
        public string Sinopse { get; set; }
        public decimal Valor { get; set; }
        public virtual ICollection<LivroAutorEntity> LivroAutores { get; set; }
        public virtual ICollection<LivroAssuntoEntity> LivroAssuntos { get; set; }
        public virtual ICollection<CanalPrecoEntity> CanalPrecos { get; set; }
    }
}
