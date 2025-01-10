using AutoMapper;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;

namespace Bookshop.Application.Business
{
    public class CanalBusiness : BaseBusiness<CanalEntity, CanalRequest, CanalResponse>, ICanalBusiness
    {
        public CanalBusiness(ICanalRepository repository, IMapper mapper, IValidator<CanalRequest> validator)
            : base(repository, mapper, validator)
        {
        }
    }
}
