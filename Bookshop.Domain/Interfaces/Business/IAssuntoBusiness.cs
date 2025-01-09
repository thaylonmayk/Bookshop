using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Domain.Interfaces.Business
{
    public interface IAssuntoBusiness : IBusiness<AssuntoEntity, AssuntoRequest, AssuntoResponse>
    {
        Task<AssuntoResponse> GetByDescricaoAsync(string descricao);
    }
}
