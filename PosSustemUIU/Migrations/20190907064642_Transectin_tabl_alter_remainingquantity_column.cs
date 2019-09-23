using Microsoft.EntityFrameworkCore.Migrations;

namespace PosSustemUIU.Migrations
{
    public partial class Transectin_tabl_alter_remainingquantity_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingQuantity",
                table: "Transections",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingQuantity",
                table: "Transections");
        }
    }
}
