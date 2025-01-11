using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Application.Profiles
{
    public class CanalProfile : Profile
    {
        public CanalProfile()
        {
            CreateMap<CanalRequest, CanalEntity>();
            CreateMap<CanalEntity, CanalResponse>();
        }
    }
}
