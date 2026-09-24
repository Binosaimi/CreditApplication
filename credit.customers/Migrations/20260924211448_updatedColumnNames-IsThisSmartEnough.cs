using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace credit.customers.Migrations
{
    /// <inheritdoc />
    public partial class updatedColumnNamesIsThisSmartEnough : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "customers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Dob",
                table: "customers",
                newName: "dob");

            migrationBuilder.RenameColumn(
                name: "IsEligible",
                table: "customers",
                newName: "is_eligible");

            migrationBuilder.RenameColumn(
                name: "CivilId",
                table: "customers",
                newName: "civil_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "customers",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CivilId",
                table: "customers",
                newName: "IX_customers_civil_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "civil_id",
                table: "customers",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "customer_id");

            migrationBuilder.CreateTable(
                name: "litigation",
                columns: table => new
                {
                    litigation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    institution_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    date_of_verdict = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_litigation", x => x.litigation_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "litigation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "dob",
                table: "Customers",
                newName: "Dob");

            migrationBuilder.RenameColumn(
                name: "is_eligible",
                table: "Customers",
                newName: "IsEligible");

            migrationBuilder.RenameColumn(
                name: "civil_id",
                table: "Customers",
                newName: "CivilId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_customers_civil_id",
                table: "Customers",
                newName: "IX_Customers_CivilId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CivilId",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }
    }
}
