using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Application.Profiles
{
    using AutoMapper;

    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<LivroRequest, LivroEntity>()
                .ForMember(dest => dest.LivroAutores, opt => opt.MapFrom(src =>
                    src.Autores.Select(cod => new LivroAutorEntity { CodAutor = cod }).ToList()))
                .ForMember(dest => dest.LivroAssuntos, opt => opt.MapFrom(src =>
                    src.Assuntos.Select(cod => new LivroAssuntoEntity { CodAssunto = cod }).ToList()));

            CreateMap<LivroEntity, LivroResponse>()
                    .ForMember(dest => dest.Autores, opt => opt.MapFrom(src =>
                    src.LivroAutores.Select(la =>
                    new AutorResponse
                    {
                        Cod = la.CodLivro,
                        Nome = la.Autor.Nome,
                        Sobrenome = la.Autor.Sobrenome
                    })))
                    .ForMember(dest => dest.Assuntos, opt => opt.MapFrom(src =>
                    src.LivroAssuntos.Select(la =>
                    new AssuntoResponse
                    {
                        Cod = la.CodAssunto,
                        Descricao = la.Assunto.Descricao
                    })));
        }
    }

}