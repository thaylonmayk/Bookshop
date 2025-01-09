using AutoMapper;
using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Application.Business
{
    public class BaseBusiness<TEntity, TRequest, TResponse> : IBusiness<TEntity, TRequest, TResponse>
     where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected readonly IValidator<TRequest> _validator;

        public BaseBusiness(IBaseRepository<TEntity> repository, IMapper mapper, IValidator<TRequest> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public virtual async Task<TResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return default;

            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TResponse> AddAsync(TRequest request)
        {
            // Validação do request
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

            var entity = _mapper.Map<TEntity>(request);
            var addedEntity = await _repository.AddAsync(entity);
            return _mapper.Map<TResponse>(addedEntity);
        }

        public virtual async Task<TResponse> UpdateAsync(TRequest request)
        {
            // Validação do request
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

            var entity = _mapper.Map<TEntity>(request);
            var updatedEntity = await _repository.UpdateAsync(entity);
            return _mapper.Map<TResponse>(updatedEntity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
