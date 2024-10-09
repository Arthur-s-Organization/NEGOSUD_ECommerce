using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemImage",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "ItemImagePath",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemImagePath",
                table: "Items");

            migrationBuilder.AddColumn<byte[]>(
                name: "ItemImage",
                table: "Items",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
