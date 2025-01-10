using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;

namespace Bookshop.Infra.Data.Repositories
{
    public class CanalRepository : BaseRepository<CanalEntity>, ICanalRepository
    {
        public CanalRepository(MainContext context) : base(context) { }

    }
}
