using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAgendamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBoolFinalizadoSolicitacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "Solicitacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "Solicitacoes");
        }
    }
}
