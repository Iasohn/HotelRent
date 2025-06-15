using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentRoom.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "Confirmation",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmation",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "IsConfirmed",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
