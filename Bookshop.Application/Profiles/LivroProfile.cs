using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Application.Profiles
{
    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<LivroRequest, LivroEntity>();
            CreateMap<LivroEntity, LivroResponse>();
        }
    }
}