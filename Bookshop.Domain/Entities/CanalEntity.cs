namespace Bookshop.Domain.Entities
{
    public class CanalEntity : BaseEntity
    {
        public string Nome { get; set; }
        public ICollection<CanalPrecoEntity> CanalPrecos { get; set; }
    }
}
