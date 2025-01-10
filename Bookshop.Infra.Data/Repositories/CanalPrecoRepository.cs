using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;

namespace Bookshop.Infra.Data.Repositories
{
    public class CanalPrecoRepository : BaseRepository<CanalPrecoEntity>, ICanalPrecoRepository
    {
        public CanalPrecoRepository(MainContext context) : base(context) { }
    }
}
