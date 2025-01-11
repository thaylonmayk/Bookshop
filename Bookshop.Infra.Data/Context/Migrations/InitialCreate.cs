using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookshop.Infra.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assuntos",
                columns: table => new
                {
                    Cod = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assuntos", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Cod = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "Canais",
                columns: table => new
                {
                    Cod = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canais", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "CanalPrecos",
                columns: table => new
                {
                    Cod = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodLivro = table.Column<int>(nullable: false),
                    CodCanal = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanalPrecos", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Cod = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "LivrosAutores",
                columns: table => new
                {
                    CodLivro = table.Column<int>(nullable: false),
                    CodAutor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrosAutores", x => new { x.CodLivro, x.CodAutor });
                    table.ForeignKey(
                        name: "FK_LivrosAutores_Autores_CodAutor",
                        column: x => x.CodAutor,
                        principalTable: "Autores",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivrosAutores_Livros_CodLivro",
                        column: x => x.CodLivro,
                        principalTable: "Livros",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivrosAssuntos",
                columns: table => new
                {
                    CodLivro = table.Column<int>(nullable: false),
                    CodAssunto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrosAssuntos", x => new { x.CodLivro, x.CodAssunto });
                    table.ForeignKey(
                        name: "FK_LivrosAssuntos_Assuntos_CodAssunto",
                        column: x => x.CodAssunto,
                        principalTable: "Assuntos",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivrosAssuntos_Livros_CodLivro",
                        column: x => x.CodLivro,
                        principalTable: "Livros",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                });

            // Add indexes to CanalPrecos table
              migrationBuilder.CreateIndex(
                name: "IX_Autores_Nome",
                table: "Autores",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Assuntos_Descricao",
                table: "Assuntos",
                column: "Descricao");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_Nome",
                table: "Canais",
                column: "Nome");
                
            migrationBuilder.CreateIndex(
                name: "IX_CanalPrecos_CodLivro",
                table: "CanalPrecos",
                column: "CodLivro");

            migrationBuilder.CreateIndex(
                name: "IX_CanalPrecos_CodCanal",
                table: "CanalPrecos",
                column: "CodCanal");

            // Create the LivrosAutoresAssuntos view
            migrationBuilder.Sql(@"
                CREATE VIEW LivrosAutoresAssuntos AS
                SELECT 
                    a.Cod AS AutorCod,
                    a.Nome AS AutorNome,
                    a.Sobrenome AS AutorSobrenome,
                    l.Cod AS LivroCod,
                    l.Titulo AS LivroTitulo,
                    l.Editora AS LivroEditora,
                    l.AnoPublicacao AS LivroAnoPublicacao,
                    l.Edicao AS LivroEdicao,
                    l.Sinopse AS LivroSinopse,
                    l.Valor AS LivroValor,
                    s.Cod AS AssuntoCod,
                    s.Descricao AS AssuntoDescricao
                FROM Livros l
                JOIN LivrosAutores la ON l.Cod = la.CodLivro
                JOIN Autores a ON la.CodAutor = a.Cod
                JOIN LivrosAssuntos las ON l.Cod = las.CodLivro
                JOIN Assuntos s ON las.CodAssunto = s.Cod
            ");

            // Create indexes on the view
            migrationBuilder.Sql(@"
                CREATE UNIQUE CLUSTERED INDEX IX_LivrosAutoresAssuntos_AutorCod_LivroCod_AssuntoCod
                ON LivrosAutoresAssuntos (AutorCod, LivroCod, AssuntoCod)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "LivrosAutores");
            migrationBuilder.DropTable(name: "LivrosAssuntos");
            migrationBuilder.DropTable(name: "LivrosAutoresAssuntos");
            migrationBuilder.DropTable(name: "CanalPrecos");
            migrationBuilder.DropTable(name: "Autores");
            migrationBuilder.DropTable(name: "Assuntos");
            migrationBuilder.DropTable(name: "Livros");
            migrationBuilder.DropTable(name: "Canais");
            migrationBuilder.Sql("DROP VIEW IF EXISTS LivrosAutoresAssuntos");
        }
    }
}