using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAgendamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdemToModuloRotina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Agendamentos_AgendamentoId",
                table: "Solicitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Usuarios_SolicitadoId",
                table: "Solicitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Usuarios_SolicitanteId",
                table: "Solicitacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Solicitacao",
                table: "Solicitacao");

            migrationBuilder.RenameTable(
                name: "Solicitacao",
                newName: "Solicitacoes");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacao_SolicitanteId",
                table: "Solicitacoes",
                newName: "IX_Solicitacoes_SolicitanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacao_SolicitadoId",
                table: "Solicitacoes",
                newName: "IX_Solicitacoes_SolicitadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacao_AgendamentoId",
                table: "Solicitacoes",
                newName: "IX_Solicitacoes_AgendamentoId");

            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "Rotinas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "Modulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Solicitacoes",
                table: "Solicitacoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Agendamentos_AgendamentoId",
                table: "Solicitacoes",
                column: "AgendamentoId",
                principalTable: "Agendamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Usuarios_SolicitadoId",
                table: "Solicitacoes",
                column: "SolicitadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Usuarios_SolicitanteId",
                table: "Solicitacoes",
                column: "SolicitanteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Agendamentos_AgendamentoId",
                table: "Solicitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Usuarios_SolicitadoId",
                table: "Solicitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Usuarios_SolicitanteId",
                table: "Solicitacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Solicitacoes",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "Rotinas");

            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "Modulos");

            migrationBuilder.RenameTable(
                name: "Solicitacoes",
                newName: "Solicitacao");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacoes_SolicitanteId",
                table: "Solicitacao",
                newName: "IX_Solicitacao_SolicitanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacoes_SolicitadoId",
                table: "Solicitacao",
                newName: "IX_Solicitacao_SolicitadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacoes_AgendamentoId",
                table: "Solicitacao",
                newName: "IX_Solicitacao_AgendamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Solicitacao",
                table: "Solicitacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Agendamentos_AgendamentoId",
                table: "Solicitacao",
                column: "AgendamentoId",
                principalTable: "Agendamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Usuarios_SolicitadoId",
                table: "Solicitacao",
                column: "SolicitadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Usuarios_SolicitanteId",
                table: "Solicitacao",
                column: "SolicitanteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
