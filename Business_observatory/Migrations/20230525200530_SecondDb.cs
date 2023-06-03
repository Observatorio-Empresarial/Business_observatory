using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_observatory.Migrations
{
    /// <inheritdoc />
    public partial class SecondDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Contact",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contact");
        }
    }
}
