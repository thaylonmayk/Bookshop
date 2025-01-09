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
            DataTable dataTable = await _relatorioRepository.GetRelatorioDataAsync();

            //ReportDataSource rds = new ReportDataSource("DataSetRelatorio", dataTable);

            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.LocalReport.DataSources.Clear();
            //reportViewer.LocalReport.DataSources.Add(rds);
            //reportViewer.LocalReport.ReportPath = "LivroAutorReport.rdl";
            //reportViewer.RefreshReport();

            return dataTable;
        }
    }

}