using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBCurso_Add_TBAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCurso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    CargaHoraria = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBCurso_TBCategoria",
                        column: x => x.CategoriaId,
                        principalTable: "TBCategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBAula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DuracaoEmMinutos = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBAula_TBCurso",
                        column: x => x.CursoId,
                        principalTable: "TBCurso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_TBAula_CursoId_Ordem",
                table: "TBAula",
                columns: new[] { "CursoId", "Ordem" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBAula_Nome",
                table: "TBAula",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBCurso_CategoriaId",
                table: "TBCurso",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "UQ_TBCurso_Nome",
                table: "TBCurso",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAula");

            migrationBuilder.DropTable(
                name: "TBCurso");
        }
    }
}
