using System.Data;

namespace Bookshop.Domain.Interfaces.Business
{
    public interface IRelatorioBusiness
    {
        Task<DataTable> GetRelatorioDataAsync();
        Task<Stream> GenerateCsvReportAsync();
        Task<Stream> GeneratePdfReportAsync();
        Task<byte[]> GenerateExcelReportAsync();
    }

}
