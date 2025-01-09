using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Application.Business
{
    public class RelatorioBusiness : IRelatorioBusiness
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioBusiness(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<DataTable> GetRelatorioDataAsync()
        {
            return await _relatorioRepository.GetRelatorioDataAsync();
        }
    }

}
