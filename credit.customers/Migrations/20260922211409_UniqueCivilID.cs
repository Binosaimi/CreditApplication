using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace credit.customers.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCivilID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_CivilId",
                table: "Customers",
                column: "CivilId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_CivilId",
                table: "Customers");
        }
    }
}
