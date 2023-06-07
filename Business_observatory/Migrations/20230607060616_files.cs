using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_observatory.Migrations
{
    /// <inheritdoc />
    public partial class files : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArchivoId",
                table: "Archivos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Archivos",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Archivos",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_ArchivoId",
                table: "Archivos",
                column: "ArchivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Archivos_ArchivoId",
                table: "Archivos",
                column: "ArchivoId",
                principalTable: "Archivos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Archivos_ArchivoId",
                table: "Archivos");

            migrationBuilder.DropIndex(
                name: "IX_Archivos_ArchivoId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "ArchivoId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Archivos");
        }
    }
}
