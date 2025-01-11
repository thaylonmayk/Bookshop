using System.Text;
using Bookshop.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioBusiness _relatorioBusiness;

    public RelatoriosController(IRelatorioBusiness relatorioBusiness)
    {
        _relatorioBusiness = relatorioBusiness;
    }

    [HttpGet("gerar-relatorio")]
    public async Task<IActionResult> GerarRelatorio2()
    {
        var dataTable = await _relatorioBusiness.GetRelatorioDataAsync();

        var localReport = new LocalReport
        {
            ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "LivroAutorReport.rdl")
        };

        localReport.DataSources.Add(new ReportDataSource("RelatorioDataSet", dataTable));

        string reportType = "PDF";
        string mimeType;
        string encoding;
        string fileNameExtension;
        Warning[] warnings;
        string[] streams;
        var reportBytes = localReport.Render(
            reportType,
            null,
            out mimeType,
            out encoding,
            out fileNameExtension,
            out streams,
            out warnings
        );

        return File(reportBytes, mimeType, "Relatorio.pdf");
    }
    [HttpGet("gerar-relatorio-csv")]
    public async Task<IActionResult> GenerateCsvReportAsync()
    {
        var stream = await _relatorioBusiness.GenerateCsvReportAsync();
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        var csvBytes = memoryStream.ToArray();
        var fileName = $"Relatorio_{DateTime.Now:yyyyMMdd}.csv";
        return File(csvBytes, "text/csv", fileName);
    }
    [HttpGet("gerar-relatorio-excel")]
    public async Task<IActionResult> GenerateExcelReportAsync()
    {
        var excelBytes = await _relatorioBusiness.GenerateExcelReportAsync();
        var fileName = $"Relatorio_{DateTime.Now:yyyyMMdd}.xlsx";
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
    [HttpGet("gerar-relatorio-pdf")]
    public async Task<IActionResult> GeneratePdfReportAsync()
    {
        var reportBytes = await _relatorioBusiness.GeneratePdfReportAsync();
        var fileName = $"Relatorio_{DateTime.Now:yyyyMMdd}.pdf";
        return File(reportBytes, "application/pdf", fileName);
    }
}
