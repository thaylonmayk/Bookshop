using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop.Domain.Entities
{
    public class CanalPrecoEntity : BaseEntity
    {
        public int CodLivro { get; set; }
        public int CodCanal { get; set; }
        public decimal Valor { get; set; }
        [ForeignKey("CodLivro")]
        public virtual LivroEntity Livros { get; set; }
        [ForeignKey("CodCanal")]
        public virtual CanalEntity Canais { get; set; }
    }
}
