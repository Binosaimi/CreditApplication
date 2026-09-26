using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace credit.identity.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "users");

            migrationBuilder.AlterColumn<Guid>(
                name: "username",
                table: "users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
