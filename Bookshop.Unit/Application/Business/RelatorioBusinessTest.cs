using Bookshop.Application.Business;
using Bookshop.Domain.Interfaces.Repositories;
using Moq;
using System.Data;

namespace Bookshop.Unit.Application.Business
{
    public class RelatorioBusinessTest
    {
        private readonly Mock<IRelatorioRepository> _relatorioRepositoryMock;
        private readonly RelatorioBusiness _relatorioBusiness;

        public RelatorioBusinessTest()
        {
            _relatorioRepositoryMock = new Mock<IRelatorioRepository>();
            _relatorioBusiness = new RelatorioBusiness(_relatorioRepositoryMock.Object);
        }

        [Fact]
        public async Task GetRelatorioDataAsync_ShouldReturnDataTable_WhenDataExists()
        {
            // Arrange
            var dataTable = new DataTable();
            dataTable.Columns.Add("Column1");
            dataTable.Rows.Add("Value1");

            _relatorioRepositoryMock.Setup(repo => repo.GetRelatorioDataAsync()).ReturnsAsync(dataTable);

            // Act
            var result = await _relatorioBusiness.GetRelatorioDataAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dataTable, result);
        }

        [Fact]
        public async Task GetRelatorioDataAsync_ShouldReturnEmptyDataTable_WhenNoDataExists()
        {
            // Arrange
            var dataTable = new DataTable();

            _relatorioRepositoryMock.Setup(repo => repo.GetRelatorioDataAsync()).ReturnsAsync(dataTable);

            // Act
            var result = await _relatorioBusiness.GetRelatorioDataAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Rows);
        }
    }
}