using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_transport.Migrations
{
    /// <inheritdoc />
    public partial class rolescodEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodEmpresa",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodEmpresa",
                table: "AspNetRoles");
        }
    }
}
