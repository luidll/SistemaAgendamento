using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agendamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlAndIconModulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Modulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Modulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Modulos");
        }
    }
}
