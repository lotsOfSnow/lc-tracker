using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LcTracker.Core.Storage.Migrations
{
    /// <inheritdoc />
    public partial class Locking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Problems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Attempts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Attempts");
        }
    }
}
