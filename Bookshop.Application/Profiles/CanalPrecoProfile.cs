using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Application.Profiles
{
    public class CanalPrecoProfile : Profile
    {
        public CanalPrecoProfile()
        {
            CreateMap<CanalPrecoRequest, CanalPrecoEntity>();
            CreateMap<CanalPrecoEntity, CanalPrecoResponse>();
        }
    }
}
