using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.Interfaces.Business
{
    public interface IBusiness<TEntity, TRequest, TResponse>
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse> GetByIdAsync(int id);
        Task<TResponse> AddAsync(TRequest entity);
        Task<TResponse> UpdateAsync(TRequest entity);
        Task DeleteAsync(int id);
    }

}
