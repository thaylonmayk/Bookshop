using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;

namespace Bookshop.Application.Business
{
    public class CanalPrecoBusiness : BaseBusiness<CanalPrecoEntity, CanalPrecoRequest, CanalPrecoResponse>, ICanalPrecoBusiness
    {
        public CanalPrecoBusiness(IBaseRepository<CanalPrecoEntity> repository, IMapper mapper, IValidator<CanalPrecoRequest> validator) : base(repository, mapper, validator)
        {
        }
    }
}
