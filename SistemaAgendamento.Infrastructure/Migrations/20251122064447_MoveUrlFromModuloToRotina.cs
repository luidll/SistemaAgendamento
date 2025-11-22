using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAgendamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveUrlFromModuloToRotina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Modulos");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Rotinas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Rotinas");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Modulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
