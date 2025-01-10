using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Domain.Interfaces.Business
{
    public interface ICanalBusiness : IBusiness<CanalEntity, CanalRequest, CanalResponse>
    {
    }
}
