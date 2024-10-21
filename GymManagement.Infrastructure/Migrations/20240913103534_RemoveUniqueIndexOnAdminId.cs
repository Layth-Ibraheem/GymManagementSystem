using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexOnAdminId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the unique index on AdminId
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_AdminId",
                table: "Subscriptions");

            // Recreate a non-unique index on AdminId
            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AdminId",
                table: "Subscriptions",
                column: "AdminId"); // No unique: this allows multiple subscriptions for an admin
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the non-unique index (in case you need to roll back)
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_AdminId",
                table: "Subscriptions");

            // Recreate the unique index
            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AdminId",
                table: "Subscriptions",
                column: "AdminId",
                unique: true); // Unique constraint re-applied on AdminId
        }
    }
}
