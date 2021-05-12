using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class orderCartAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemsId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderDetailId",
                table: "Items",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_OrderDetail_OrderDetailId",
                table: "Items",
                column: "OrderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "OrderDetailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_OrderDetail_OrderDetailId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderDetailId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemsId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "Items");
        }
    }
}
