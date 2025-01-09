using System.Data;

namespace Bookshop.Domain.Interfaces.Repositories
{
    public interface IRelatorioRepository
    {
        Task<DataTable> GetRelatorioDataAsync();
    }
}
