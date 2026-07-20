using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Unique_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TBMatricula_TurmaId_AlunoId",
                table: "TBMatricula");

            migrationBuilder.DropIndex(
                name: "UQ_TBInstrutor_Nome",
                table: "TBInstrutor");

            migrationBuilder.DropIndex(
                name: "UQ_TBCurso_Nome",
                table: "TBCurso");

            migrationBuilder.DropIndex(
                name: "UQ_TBCategoria_Nome",
                table: "TBCategoria");

            migrationBuilder.DropIndex(
                name: "UQ_TBAula_CursoId_Ordem",
                table: "TBAula");

            migrationBuilder.CreateIndex(
                name: "UQ_TBTurma_UserId_Nome_DataInicio_DataTermino",
                table: "TBTurma",
                columns: new[] { "UserId", "Nome", "DataInicio", "DataTermino" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBMatricula_TurmaId",
                table: "TBMatricula",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "UQ_TBMatricula_UserId_TurmaId_AlunoId",
                table: "TBMatricula",
                columns: new[] { "UserId", "TurmaId", "AlunoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBInstrutor_UserId_Nome",
                table: "TBInstrutor",
                columns: new[] { "UserId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBInstituicao_UserId_Nome",
                table: "TBInstituicao",
                columns: new[] { "UserId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBCurso_UserId_Nome",
                table: "TBCurso",
                columns: new[] { "UserId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBCategoria_UserId_Nome",
                table: "TBCategoria",
                columns: new[] { "UserId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBAula_CursoId",
                table: "TBAula",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "UQ_TBAula_UserId_CursoId_Ordem",
                table: "TBAula",
                columns: new[] { "UserId", "CursoId", "Ordem" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TBTurma_UserId_Nome_DataInicio_DataTermino",
                table: "TBTurma");

            migrationBuilder.DropIndex(
                name: "IX_TBMatricula_TurmaId",
                table: "TBMatricula");

            migrationBuilder.DropIndex(
                name: "UQ_TBMatricula_UserId_TurmaId_AlunoId",
                table: "TBMatricula");

            migrationBuilder.DropIndex(
                name: "UQ_TBInstrutor_UserId_Nome",
                table: "TBInstrutor");

            migrationBuilder.DropIndex(
                name: "UQ_TBInstituicao_UserId_Nome",
                table: "TBInstituicao");

            migrationBuilder.DropIndex(
                name: "UQ_TBCurso_UserId_Nome",
                table: "TBCurso");

            migrationBuilder.DropIndex(
                name: "UQ_TBCategoria_UserId_Nome",
                table: "TBCategoria");

            migrationBuilder.DropIndex(
                name: "IX_TBAula_CursoId",
                table: "TBAula");

            migrationBuilder.DropIndex(
                name: "UQ_TBAula_UserId_CursoId_Ordem",
                table: "TBAula");

            migrationBuilder.CreateIndex(
                name: "UQ_TBMatricula_TurmaId_AlunoId",
                table: "TBMatricula",
                columns: new[] { "TurmaId", "AlunoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBInstrutor_Nome",
                table: "TBInstrutor",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBCurso_Nome",
                table: "TBCurso",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBCategoria_Nome",
                table: "TBCategoria",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBAula_CursoId_Ordem",
                table: "TBAula",
                columns: new[] { "CursoId", "Ordem" },
                unique: true);
        }
    }
}
