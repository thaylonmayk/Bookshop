using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Infra.Data.Repositories
{
    public class LivroRepository : BaseRepository<LivroEntity>, ILivroRepository
    {
        public LivroRepository(MainContext context) : base(context) { }

        public override async Task<LivroEntity> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(l => l.LivroAutores)
                    .ThenInclude(la => la.Autor)
                .Include(l => l.LivroAssuntos)
                    .ThenInclude(la => la.Assunto)
                .FirstOrDefaultAsync(l => l.Cod == id);
        }
    }

}
