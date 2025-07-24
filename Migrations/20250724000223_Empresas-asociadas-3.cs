using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_transport.Migrations
{
    /// <inheritdoc />
    public partial class Empresasasociadas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FechaFinMembresia",
                table: "Empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FechaInicioMembresia",
                table: "Empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFinMembresia",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "FechaInicioMembresia",
                table: "Empresas");
        }
    }
}
