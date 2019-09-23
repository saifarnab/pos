using Microsoft.EntityFrameworkCore.Migrations;

namespace PosSustemUIU.Migrations
{
    public partial class update_purchase_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ProductPurchases",
                newName: "PurchaseNote");

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "ProductPurchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductPurchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryNote",
                table: "ProductPurchases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "ProductPurchases");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductPurchases");

            migrationBuilder.DropColumn(
                name: "DeliveryNote",
                table: "ProductPurchases");

            migrationBuilder.RenameColumn(
                name: "PurchaseNote",
                table: "ProductPurchases",
                newName: "Created");
        }
    }
}
