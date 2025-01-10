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
            CreateMap<AssuntoRequest, AssuntoEntity>();
            CreateMap<AssuntoEntity, AssuntoResponse>();
        }
    }
}
