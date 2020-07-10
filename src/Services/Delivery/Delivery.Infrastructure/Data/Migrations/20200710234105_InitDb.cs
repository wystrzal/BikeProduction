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
                    Quantity = table.Column<int>(nullable: false),
                    LoadingPlaceStatus = table.Column<int>(nullable: false),
                    LoadingPlaceNumber = table.Column<int>(nullable: false)
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
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadingPlaceId = table.Column<int>(nullable: true)
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
