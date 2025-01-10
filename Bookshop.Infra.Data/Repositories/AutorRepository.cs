using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Infra.Data.Repositories
{

    public class AutorRepository : BaseRepository<AutorEntity>, IAutorRepository
    {
        public AutorRepository(MainContext context) : base(context) { }

        public async Task<AutorEntity> GetByNomeAndSobrenomeAsync(string nome, string sobrenome)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Nome == nome && a.Sobrenome == sobrenome);
        }
    }
}