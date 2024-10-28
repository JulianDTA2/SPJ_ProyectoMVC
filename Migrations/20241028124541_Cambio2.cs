using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPJ_ProyectoMVC.Migrations
{
    /// <inheritdoc />
    public partial class Cambio2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Catalogo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Catalogo");
        }
    }
}
