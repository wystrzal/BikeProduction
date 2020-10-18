using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Delivery.Infrastructure.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoadingPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadingPlaceName = table.Column<string>(nullable: true),
                    LoadedQuantity = table.Column<int>(nullable: false),
                    AmountOfSpace = table.Column<int>(nullable: false),
                    LoadingPlaceStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadingPlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PacksToDelivery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductsQuantity = table.Column<int>(nullable: false),
                    PackStatus = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    LoadingPlaceId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacksToDelivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacksToDelivery_LoadingPlaces_LoadingPlaceId",
                        column: x => x.LoadingPlaceId,
                        principalTable: "LoadingPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacksToDelivery_LoadingPlaceId",
                table: "PacksToDelivery",
                column: "LoadingPlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacksToDelivery");

            migrationBuilder.DropTable(
                name: "LoadingPlaces");
        }
    }
}
