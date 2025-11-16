using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agendamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    SistemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modulos_Sistemas_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rotinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rotinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rotinas_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRotinaPermissao",
                columns: table => new
                {
                    RotinasPermitidasId = table.Column<int>(type: "int", nullable: false),
                    UsuariosPermitidosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRotinaPermissao", x => new { x.RotinasPermitidasId, x.UsuariosPermitidosId });
                    table.ForeignKey(
                        name: "FK_UsuarioRotinaPermissao_Rotinas_RotinasPermitidasId",
                        column: x => x.RotinasPermitidasId,
                        principalTable: "Rotinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRotinaPermissao_Usuarios_UsuariosPermitidosId",
                        column: x => x.UsuariosPermitidosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_SistemaId",
                table: "Modulos",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rotinas_ModuloId",
                table: "Rotinas",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRotinaPermissao_UsuariosPermitidosId",
                table: "UsuarioRotinaPermissao",
                column: "UsuariosPermitidosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioRotinaPermissao");

            migrationBuilder.DropTable(
                name: "Rotinas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Sistemas");
        }
    }
}
