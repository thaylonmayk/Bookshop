using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;

namespace Bookshop.Application.Business
{

    public class AssuntoBusiness : BaseBusiness<AssuntoEntity, AssuntoRequest, AssuntoResponse>, IAssuntoBusiness
    {
        private readonly IAssuntoRepository _assuntoRepository;

        public AssuntoBusiness(IAssuntoRepository repository, IMapper mapper, IValidator<AssuntoRequest> validator)
            : base(repository, mapper, validator)
        {
            _assuntoRepository = repository;
        }

        public async Task<AssuntoResponse> GetByDescricaoAsync(string descricao)
        {
            var assunto = await _assuntoRepository.GetByDescricaoAsync(descricao);
            return _mapper.Map<AssuntoResponse>(assunto);
        }

        public override async Task<AssuntoResponse> AddAsync(AssuntoRequest request)
        {
            var assuntoExistente = await GetByDescricaoAsync(request.Descricao);
            if (assuntoExistente != null)
            {
                return assuntoExistente;
            }
            return await base.AddAsync(request);
        }
    }

}
