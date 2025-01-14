﻿using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;

namespace Bookshop.Domain.Interfaces.Business
{
    public interface IAutorBusiness : IBusiness<AutorEntity, AutorRequest, AutorResponse>
    {
        Task<AutorResponse> GetByNomeAsync(string nome, string sobrenome);
    }
}
