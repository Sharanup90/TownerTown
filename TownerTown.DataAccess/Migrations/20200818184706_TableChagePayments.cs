using Microsoft.EntityFrameworkCore.Migrations;

namespace TownerTown.DataAccess.Migrations
{
    public partial class TableChagePayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contactNumber",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "orderId",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "razorpayKey",
                table: "Payment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "contactNumber",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "razorpayKey",
                table: "Payment");
        }
    }
}
