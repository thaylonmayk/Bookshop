using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Bookshop.Application.Business
{
    public class LivroBusiness : BaseBusiness<LivroEntity, LivroRequest, LivroResponse>, ILivroBusiness
    {
        private readonly IAutorBusiness _autorBusiness;
        private readonly IAssuntoBusiness _assuntoBusiness;
        public LivroBusiness(ILivroRepository repository, IMapper mapper, IValidator<LivroRequest> validator, IAutorBusiness autorBusiness, IAssuntoBusiness assuntoBusiness)
            : base(repository, mapper, validator)
        {
            _autorBusiness = autorBusiness;
            _assuntoBusiness = assuntoBusiness;
        }

        public override async Task<LivroResponse> AddAsync(LivroRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
            List<AutorResponse> autores = await AddAutores(request);
            List<AssuntoResponse> assuntos = await AddAssuntos(request);
            var livroEntity = _mapper.Map<LivroEntity>(request);
            livroEntity.LivroAutores = autores.Select(a => new LivroAutorEntity { CodLivro = livroEntity.Cod, CodAutor = a.Cod }).ToList();
            livroEntity.LivroAssuntos = assuntos.Select(s => new LivroAssuntoEntity { CodLivro = livroEntity.Cod, CodAssunto = s.Cod }).ToList();
            var novoLivro = await _repository.AddAsync(livroEntity);
            return _mapper.Map<LivroResponse>(novoLivro);
        }

        public override async Task<LivroResponse> UpdateAsync(LivroRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
            List<AutorResponse> autores = await AddAutores(request);
            List<AssuntoResponse> assuntos = await AddAssuntos(request);
            var livroEntity = _mapper.Map<LivroEntity>(request);
            livroEntity.LivroAutores = autores.Select(a => new LivroAutorEntity
            {
                CodLivro = livroEntity.Cod,
                CodAutor = a.Cod
            }).ToList();

            livroEntity.LivroAssuntos = assuntos.Select(s => new LivroAssuntoEntity
            {
                CodLivro = livroEntity.Cod,
                CodAssunto = s.Cod
            }).ToList();

            var livroAtualizado = await _repository.UpdateAsync(livroEntity);
            return _mapper.Map<LivroResponse>(livroAtualizado);
        }
        public override async Task DeleteAsync(int id)
        {
            var livro = await _repository.GetByIdAsync(id);
            if (livro == null)
            {
                throw new ArgumentException("Livro não encontrado.");
            }
            await DeleteAutores(livro);
            await DeleteAssuntos(livro);
            await _repository.DeleteAsync(id);
        }

        private async Task<List<AutorResponse>> AddAutores(LivroRequest request)
        {
            var autores = new List<AutorResponse>();
            foreach (var autorRequest in request.Autores)
            {
                AutorResponse autorResponse;
                if (autorRequest.Cod.HasValue && autorRequest.Cod > 0)
                {
                    autorResponse = await _autorBusiness.GetByIdAsync(autorRequest.Cod.Value);
                }
                else
                {
                    autorResponse = await _autorBusiness.AddAsync(autorRequest);
                }

                autores.Add(autorResponse);
            }

            return autores;
        }
        private async Task<List<AssuntoResponse>> AddAssuntos(LivroRequest request)
        {
            var assuntos = new List<AssuntoResponse>();
            foreach (var assuntoRequest in request.Assuntos)
            {
                AssuntoResponse assuntoResponse;
                if (assuntoRequest.Cod.HasValue && assuntoRequest.Cod > 0)
                {
                    assuntoResponse = await _assuntoBusiness.GetByIdAsync(assuntoRequest.Cod.Value);
                }
                else
                {
                    assuntoResponse = await _assuntoBusiness.AddAsync(assuntoRequest);
                }

                assuntos.Add(assuntoResponse);
            }

            return assuntos;
        }

        private async Task DeleteAutores(LivroEntity livro)
        {
            foreach (var livroAutor in livro.LivroAutores)
            {
                await _autorBusiness.DeleteAsync(livroAutor.CodAutor);
            }
        }
        private async Task DeleteAssuntos(LivroEntity livro)
        {
            foreach (var livroAssunto in livro.LivroAssuntos)
            {
                await _assuntoBusiness.DeleteAsync(livroAssunto.CodAssunto);
            }
        }
    }
}