using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxGymsToSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxGyms",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxGyms",
                table: "Subscriptions");
        }
    }
}
