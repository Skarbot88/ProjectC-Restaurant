using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class final1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUsersId",
                table: "OrderBill",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "OrderBill",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "e9790ac4-08ff-46f6-aa64-cc416c241972");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18bF1b0-aa65-189f-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "1bc3aee5-b224-4e60-b500-fb01081eb295");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18bF1b0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "b6ec1703-abe6-45ed-a2c2-385c3a5b239c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "USB3b1-aa65-189f-14pd-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d6689e98-f2f8-492f-80f5-cfc92af2ecd7", "AQAAAAEAACcQAAAAEDM0PmHUylvc8SZEPB6Vv7dnOV00CY/pXgL9pSNTnRAcUu9UWeNb6pVvDBD/yC90jA==" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderBill_ApplicationUsersId",
                table: "OrderBill",
                column: "ApplicationUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBill_AspNetUsers_ApplicationUsersId",
                table: "OrderBill",
                column: "ApplicationUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBill_AspNetUsers_ApplicationUsersId",
                table: "OrderBill");

            migrationBuilder.DropIndex(
                name: "IX_OrderBill_ApplicationUsersId",
                table: "OrderBill");

            migrationBuilder.DropColumn(
                name: "ApplicationUsersId",
                table: "OrderBill");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderBill");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "4f5185cc-1d28-4833-a8eb-84cdaa40de9c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18bF1b0-aa65-189f-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "02e57377-123a-4784-a4f2-2de9b85cab2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18bF1b0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "20f62478-6aa5-42f8-be54-7ae7b3b5bfe7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "USB3b1-aa65-189f-14pd-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b1ebc9f-1261-4900-b37b-4afd6c6f935c", "AQAAAAEAACcQAAAAEKhKZ6ySW+U1lyZoLrqa2ZenJ7GH9t+ip7pWtxE11EeKgX9P67hZOb1dMzFz/aDBAA==" });
        }
    }
}
