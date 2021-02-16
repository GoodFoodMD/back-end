using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApplication1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemLabel = table.Column<string>(type: "text", nullable: false),
                    ItemPrice = table.Column<double>(type: "double precision", nullable: false),
                    ItemCategory = table.Column<int>(type: "integer", nullable: false),
                    ItemDescription = table.Column<string>(type: "text", nullable: true),
                    ItemPictureUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsPickUpOnPoint = table.Column<bool>(type: "boolean", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CustomerEmail = table.Column<string>(type: "text", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    CustomerPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    OrderTotalPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
