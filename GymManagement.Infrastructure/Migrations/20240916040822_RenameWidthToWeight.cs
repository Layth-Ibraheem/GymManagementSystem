using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameWidthToWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Trainers",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Players",
                newName: "Weight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Trainers",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Players",
                newName: "Width");
        }
    }
}
