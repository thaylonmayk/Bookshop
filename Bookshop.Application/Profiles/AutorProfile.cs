using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Application.Profiles
{
    public class AutorProfile : Profile
    {
        public AutorProfile()
        {
            CreateMap<AutorRequest, AutorEntity>().ForMember(dest => dest.Cod, opt => opt.Ignore());
            CreateMap<AutorEntity, AutorResponse>();
        }
    }
}