using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourAgency.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageColumnToTours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Tours");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Tours",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
