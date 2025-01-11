namespace Bookshop.Domain.Entities
{
    public class CanalEntity : BaseEntity
    {
        public string Nome { get; set; }
        public virtual ICollection<CanalPrecoEntity> CanalPrecos { get; set; }
    }
}
