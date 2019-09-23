using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PosSustemUIU.Migrations
{
    public partial class Transectin_table_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transections",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TransectionTypeId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Vat = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ExpireDate = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transections_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transections_TransectionType_TransectionTypeId",
                        column: x => x.TransectionTypeId,
                        principalTable: "TransectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transections_ProductId",
                table: "Transections",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transections_TransectionTypeId",
                table: "Transections",
                column: "TransectionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transections");
        }
    }
}
