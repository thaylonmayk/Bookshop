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
            ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "RelatorioLivroAutor.rdlc")
        };

        localReport.DataSources.Add(new ReportDataSource("RelatorioDataSet", dataTable));

        var reportBytes = localReport.Render("PDF");

        return File(reportBytes, "application/pdf", "Relatorio.pdf");
    }
}
