﻿using Bookshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.Interfaces.Repositories
{
    public interface IAutorRepository : IBaseRepository<AutorEntity>
    {
        Task<AutorEntity> GetByNomeAndSobrenomeAsync(string nome, string sobrenome);
    }
}