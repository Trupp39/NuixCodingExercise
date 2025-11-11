using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentPerformanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateInvesmentListOnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Investments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investments_UserId",
                table: "Investments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_Users_UserId",
                table: "Investments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_Users_UserId",
                table: "Investments");

            migrationBuilder.DropIndex(
                name: "IX_Investments_UserId",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Investments");
        }
    }
}
