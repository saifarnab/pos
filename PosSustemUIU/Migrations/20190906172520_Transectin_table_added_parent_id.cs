using Microsoft.EntityFrameworkCore.Migrations;

namespace PosSustemUIU.Migrations
{
    public partial class Transectin_table_added_parent_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Transections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Transections");
        }
    }
}
