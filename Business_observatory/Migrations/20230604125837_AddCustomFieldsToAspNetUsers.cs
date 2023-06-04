using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_observatory.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomFieldsToAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Compania",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Compania",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "AspNetUsers");
        }
    }
}
