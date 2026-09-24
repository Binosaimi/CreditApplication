using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace credit.loans.Migrations
{
    /// <inheritdoc />
    public partial class Day3MigrationsChangedDBNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    loan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    institution_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tenor = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    rate = table.Column<double>(type: "double precision", nullable: false),
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loans", x => x.loan_id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentLedger",
                columns: table => new
                {
                    payment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    loan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    institution_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLedger", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "Delinquencies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    delinquency_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LoansLoanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delinquencies", x => x.id);
                    table.ForeignKey(
                        name: "FK_Delinquencies_loans_LoansLoanId",
                        column: x => x.LoansLoanId,
                        principalTable: "loans",
                        principalColumn: "loan_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delinquencies_LoansLoanId",
                table: "Delinquencies",
                column: "LoansLoanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delinquencies");

            migrationBuilder.DropTable(
                name: "PaymentLedger");

            migrationBuilder.DropTable(
                name: "loans");
        }
    }
}
