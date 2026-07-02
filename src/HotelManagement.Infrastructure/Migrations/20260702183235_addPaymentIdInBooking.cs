using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPaymentIdInBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PaymentId",
                table: "Bookings",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Payments_PaymentId",
                table: "Bookings",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Payments_PaymentId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PaymentId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
