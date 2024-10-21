using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Weight",
                table: "Trainers",
                type: "smallint",
                nullable: false,
                comment: "The Weight Is In KG",
                oldClrType: typeof(short),
                oldType: "smallint",
                oldComment: "The Width Is In KG");

            migrationBuilder.AlterColumn<short>(
                name: "Weight",
                table: "Players",
                type: "smallint",
                nullable: false,
                comment: "The Weight Is In KG",
                oldClrType: typeof(short),
                oldType: "smallint",
                oldComment: "The Width Is In KG");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Admins");

            migrationBuilder.AlterColumn<short>(
                name: "Weight",
                table: "Trainers",
                type: "smallint",
                nullable: false,
                comment: "The Width Is In KG",
                oldClrType: typeof(short),
                oldType: "smallint",
                oldComment: "The Weight Is In KG");

            migrationBuilder.AlterColumn<short>(
                name: "Weight",
                table: "Players",
                type: "smallint",
                nullable: false,
                comment: "The Width Is In KG",
                oldClrType: typeof(short),
                oldType: "smallint",
                oldComment: "The Weight Is In KG");
        }
    }
}
