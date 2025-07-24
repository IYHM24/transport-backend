using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_transport.Migrations
{
    /// <inheritdoc />
    public partial class Empresasasociadas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpresasAsociadas",
                columns: table => new
                {
                    codEmpresaAsociada = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NIT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    foto_del_logo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    codEmpresa = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasAsociadas", x => x.codEmpresaAsociada);
                    table.ForeignKey(
                        name: "FK_EmpresasAsociadas_Empresas_codEmpresa",
                        column: x => x.codEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "codEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpresasAsociadas_codEmpresa",
                table: "EmpresasAsociadas",
                column: "codEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpresasAsociadas");
        }
    }
}
