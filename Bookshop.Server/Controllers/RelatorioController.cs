using Bookshop.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;

[ApiController]
[Route("api/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly IRelatorioBusiness _relatorioBusiness;

    public RelatorioController(IRelatorioBusiness relatorioBusiness)
    {
        _relatorioBusiness = relatorioBusiness;
    }

    [HttpGet("gerar-relatorio")]
    public async Task<IActionResult> GerarRelatorio()
    {
        var dataTable = await _relatorioBusiness.GetRelatorioDataAsync();

        var localReport = new LocalReport
        {
            ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "LivroAutorReport.rdl")
        };

        localReport.DataSources.Add(new ReportDataSource("RelatorioDataSet", dataTable));

        var reportBytes = localReport.Render("PDF");

        return File(reportBytes, "application/pdf", "Relatorio.pdf");
    }

    [HttpGet("gerar-relatorio2")]
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
}
