using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Application.Profiles
{
    public class AssuntoProfile : Profile
    {
        public AssuntoProfile()
        {
            CreateMap<AssuntoRequest, AssuntoEntity>().ForMember(dest => dest.Cod, opt => opt.Ignore());
            CreateMap<AssuntoEntity, AssuntoResponse>();
        }
    }
}
