using Bookshop.Domain.Entities.View;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly MainContext _context;

    public RelatorioRepository(MainContext context)
    {
        _context = context;
    }

    public async Task<DataTable> GetRelatorioDataAsync()
    {
        var query = await _context.LivrosAutoresAssuntos.ToListAsync();

        return ToDataTable(query);
    }

    private DataTable ToDataTable(IEnumerable<LivroAutorAssuntoView> items)
    {
        var dataTable = new DataTable();

        // Adicionar colunas
        dataTable.Columns.Add("AutorCod", typeof(int));
        dataTable.Columns.Add("AutorNome", typeof(string));
        dataTable.Columns.Add("AutorSobrenome", typeof(string));
        dataTable.Columns.Add("LivroCod", typeof(int));
        dataTable.Columns.Add("LivroTitulo", typeof(string));
        dataTable.Columns.Add("LivroEditora", typeof(string));
        dataTable.Columns.Add("LivroAnoPublicacao", typeof(string));
        dataTable.Columns.Add("LivroEdicao", typeof(int));
        dataTable.Columns.Add("LivroSinopse", typeof(string));
        dataTable.Columns.Add("LivroValor", typeof(decimal));
        dataTable.Columns.Add("AssuntoCod", typeof(int));
        dataTable.Columns.Add("AssuntoDescricao", typeof(string));
  
  
        var groupedItems = items
            .GroupBy(item => item.LivroTitulo)
            .SelectMany(group => group.OrderBy(item => item.AutorNome));

        // Adicionar linhas
        foreach (var item in groupedItems)
        {
            dataTable.Rows.Add(
                item.AutorCod, item.AutorNome, item.AutorSobrenome,
                item.LivroCod, item.LivroTitulo, item.LivroEditora,
                item.LivroAnoPublicacao, item.LivroEdicao, item.LivroSinopse,
                item.LivroValor, item.AssuntoCod, item.AssuntoDescricao);
        }

        return dataTable;
    }
}
