using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace credit.loans.Migrations
{
    /// <inheritdoc />
    public partial class fixedDataSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delinquencies_loans_LoansLoanId",
                table: "Delinquencies");

            migrationBuilder.DropIndex(
                name: "IX_Delinquencies_LoansLoanId",
                table: "Delinquencies");

            migrationBuilder.DropColumn(
                name: "LoansLoanId",
                table: "Delinquencies");

            migrationBuilder.CreateIndex(
                name: "IX_Delinquencies_loan_id",
                table: "Delinquencies",
                column: "loan_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delinquencies_loans_loan_id",
                table: "Delinquencies",
                column: "loan_id",
                principalTable: "loans",
                principalColumn: "loan_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delinquencies_loans_loan_id",
                table: "Delinquencies");

            migrationBuilder.DropIndex(
                name: "IX_Delinquencies_loan_id",
                table: "Delinquencies");

            migrationBuilder.AddColumn<Guid>(
                name: "LoansLoanId",
                table: "Delinquencies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Delinquencies_LoansLoanId",
                table: "Delinquencies",
                column: "LoansLoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delinquencies_loans_LoansLoanId",
                table: "Delinquencies",
                column: "LoansLoanId",
                principalTable: "loans",
                principalColumn: "loan_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
