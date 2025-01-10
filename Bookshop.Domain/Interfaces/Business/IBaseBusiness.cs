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
