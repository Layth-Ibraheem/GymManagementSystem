using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeRequiredTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionType",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "0: Free, 1: Starter, 2: Pro",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "0: Free, 1: Starter, 2: Pro");

            migrationBuilder.AlterColumn<int>(
                name: "RoomType",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "0: Boxing, 1: Kickboxing, 2: Zomba, 3: Dancing ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Boxing: 0 Kickboxing: 1Zomba: 2Dancing: 3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionType",
                table: "Subscriptions",
                type: "int",
                nullable: true,
                comment: "0: Free, 1: Starter, 2: Pro",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "0: Free, 1: Starter, 2: Pro");

            migrationBuilder.AlterColumn<int>(
                name: "RoomType",
                table: "Rooms",
                type: "int",
                nullable: true,
                comment: "Boxing: 0 Kickboxing: 1Zomba: 2Dancing: 3",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "0: Boxing, 1: Kickboxing, 2: Zomba, 3: Dancing ");
        }
    }
}
