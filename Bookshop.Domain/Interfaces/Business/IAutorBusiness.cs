using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.Interfaces.Business
{
    public interface IAutorBusiness : IBusiness<AutorEntity, AutorRequest, AutorResponse>
    {
        Task<AutorResponse> GetByNomeAsync(string nome, string sobrenome);
    }
}
