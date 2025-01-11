using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf;
using System.Data;
using System.Text;

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
        public async Task<Stream> GenerateCsvReportAsync()
        {
            DataTable dataTable = await GetRelatorioDataAsync();

            StringBuilder csvContent = new StringBuilder();

            IEnumerable<string> columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            csvContent.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dataTable.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                csvContent.AppendLine(string.Join(",", fields));
            }

            var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true))
            {
                await streamWriter.WriteAsync(csvContent.ToString());
                await streamWriter.FlushAsync();
            }
            memoryStream.Position = 0;

            return memoryStream;
        }

        public async Task<byte[]> GenerateExcelReportAsync()
    {
        DataTable dataTable = await GetRelatorioDataAsync();

        using (var memoryStream = new MemoryStream())
        {
            using (var spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Relatório"
                };
                sheets.Append(sheet);

                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = new Stylesheet();

                var fills = new Fills();
                var fill = new Fill();
                var patternFill = new PatternFill { PatternType = PatternValues.Solid };
                patternFill.ForegroundColor = new ForegroundColor { Rgb = new HexBinaryValue { Value = "ADD8E6" } }; 
                patternFill.BackgroundColor = new BackgroundColor { Indexed = 64 };
                fill.Append(patternFill);
                fills.Append(fill);

                var cellFormats = new CellFormats();
                var cellFormat = new CellFormat { FillId = 0, ApplyFill = true };
                cellFormats.Append(cellFormat);

                stylesPart.Stylesheet.Append(fills);
                stylesPart.Stylesheet.Append(cellFormats);
                stylesPart.Stylesheet.Save();

                var headerRow = new Row();
                foreach (DataColumn column in dataTable.Columns)
                {
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(column.ColumnName),
                        StyleIndex = 0
                    };

                    // Adjust column width for specific columns
                    if (column.ColumnName == "LivroTitulo" || column.ColumnName == "LivroEditora" || column.ColumnName == "LivroSinopse" || column.ColumnName == "AssuntoDescricao")
                    {
                        var columns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                        if (columns == null)
                        {
                            columns = new Columns();
                            worksheetPart.Worksheet.InsertAt(columns, 0);
                        }

                        var columnIndex = dataTable.Columns.IndexOf(column) + 1;
                        var columnWidth = new Column
                        {
                            Min = (uint)columnIndex,
                            Max = (uint)columnIndex,
                            Width = 30,
                            CustomWidth = true
                        };
                        columns.Append(columnWidth);
                    }
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                // Add data rows
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var row = new Row();
                    foreach (var item in dataRow.ItemArray)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.ToString())
                        };
                        row.AppendChild(cell);
                    }
                    sheetData.AppendChild(row);
                }

                workbookPart.Workbook.Save();
            }

            return memoryStream.ToArray();
        }
    }        public async Task<Stream> GeneratePdfReportAsync()
        {
            DataTable dataTable = await GetRelatorioDataAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (var document = new iTextSharp.text.Document())
                {
                    PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Add title
                    document.Add(new iTextSharp.text.Paragraph("Relatório"));

                    // Add table
                    var pdfTable = new iTextSharp.text.pdf.PdfPTable(dataTable.Columns.Count);

                    // Add header
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        pdfTable.AddCell(new iTextSharp.text.Phrase(column.ColumnName));
                    }

                    // Add rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var cell in row.ItemArray)
                        {
                            pdfTable.AddCell(new iTextSharp.text.Phrase(cell.ToString()));
                        }
                    }

                    document.Add(pdfTable);
                    document.Close();
                }

                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }

}