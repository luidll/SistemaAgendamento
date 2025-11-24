using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAgendamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SolicitacaoIntervalos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraFimSolicitada",
                table: "Solicitacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraInicioSolicitada",
                table: "Solicitacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraFimSolicitada",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "DataHoraInicioSolicitada",
                table: "Solicitacoes");
        }
    }
}
