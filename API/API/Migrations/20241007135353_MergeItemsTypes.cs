using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MergeItemsTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlcoholItem");

            migrationBuilder.DropTable(
                name: "CommonItem");

            migrationBuilder.AddColumn<Guid>(
                name: "AlcoholFamilyId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AlcoholVolume",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Capacity",
                table: "Items",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Items_AlcoholFamilyId",
                table: "Items",
                column: "AlcoholFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AlcoholFamilies_AlcoholFamilyId",
                table: "Items",
                column: "AlcoholFamilyId",
                principalTable: "AlcoholFamilies",
                principalColumn: "AlcoholFamilyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AlcoholFamilies_AlcoholFamilyId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_AlcoholFamilyId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AlcoholFamilyId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AlcoholVolume",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "AlcoholItem",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlcoholFamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlcoholVolume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<float>(type: "real", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlcoholItem", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_AlcoholItem_AlcoholFamilies_AlcoholFamilyId",
                        column: x => x.AlcoholFamilyId,
                        principalTable: "AlcoholFamilies",
                        principalColumn: "AlcoholFamilyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlcoholItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonItem",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonItem", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_CommonItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlcoholItem_AlcoholFamilyId",
                table: "AlcoholItem",
                column: "AlcoholFamilyId");
        }
    }
}
