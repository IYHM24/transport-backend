using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_transport.Migrations
{
    /// <inheritdoc />
    public partial class Empresastable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    codEmpresa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NIT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    colorPrimario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    colorSecundario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    foto_del_logo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.codEmpresa);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
