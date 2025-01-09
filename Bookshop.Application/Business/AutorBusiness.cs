using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;

namespace Bookshop.Application.Business
{
    public class AutorBusiness : BaseBusiness<AutorEntity, AutorRequest, AutorResponse>, IAutorBusiness
    {
        private readonly IAutorRepository _autorRepository;

        public AutorBusiness(IAutorRepository repository, IMapper mapper, IValidator<AutorRequest> validator)
            : base(repository, mapper, validator)
        {
            _autorRepository = repository;
        }

        public async Task<AutorResponse> GetByNomeAsync(string nome, string sobrenome)
        {
            var autor = await _autorRepository.GetByNomeAndSobrenomeAsync(nome, sobrenome);
            return _mapper.Map<AutorResponse>(autor);
        }

        public override async Task<AutorResponse> AddAsync(AutorRequest request)
        {
            var autorExistente = await GetByNomeAsync(request.Nome, request.Sobrenome);
            if (autorExistente != null)
            {
                return autorExistente;
            }
            return await base.AddAsync(request);
        }
    }
}
