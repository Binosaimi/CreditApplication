using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace credit.customers.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourtId",
                table: "litigation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourtId",
                table: "litigation");
        }
    }
}
