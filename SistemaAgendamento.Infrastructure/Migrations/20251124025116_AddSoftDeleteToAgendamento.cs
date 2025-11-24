using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAgendamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Agendamentos_AgendamentoId",
                table: "Solicitacoes");

            migrationBuilder.AddColumn<bool>(
                name: "Excluído",
                table: "Agendamentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Agendamentos_AgendamentoId",
                table: "Solicitacoes",
                column: "AgendamentoId",
                principalTable: "Agendamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Agendamentos_AgendamentoId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "Excluído",
                table: "Agendamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Agendamentos_AgendamentoId",
                table: "Solicitacoes",
                column: "AgendamentoId",
                principalTable: "Agendamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
