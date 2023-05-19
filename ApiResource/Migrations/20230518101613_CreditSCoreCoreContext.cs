using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiResource.Migrations
{
    /// <inheritdoc />
    public partial class CreditSCoreCoreContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankCustomers",
                columns: table => new
                {
                    BankCustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CustomerBalance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCustomers", x => x.BankCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "AccountBalances",
                columns: table => new
                {
                    AccountBalanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankCustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalances", x => x.AccountBalanceId);
                    table.ForeignKey(
                        name: "FK_AccountBalances_BankCustomers_BankCustomerId",
                        column: x => x.BankCustomerId,
                        principalTable: "BankCustomers",
                        principalColumn: "BankCustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    CreditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankCustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditedAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebitedAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceBeforeCredit = table.Column<double>(type: "float", nullable: false),
                    BalanceAccountBalanceId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.CreditId);
                    table.ForeignKey(
                        name: "FK_Credits_AccountBalances_BalanceAccountBalanceId",
                        column: x => x.BalanceAccountBalanceId,
                        principalTable: "AccountBalances",
                        principalColumn: "AccountBalanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Debits",
                columns: table => new
                {
                    DebitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreditedAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebitedAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankCustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceAccountBalanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BalanceBeforeDebit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debits", x => x.DebitId);
                    table.ForeignKey(
                        name: "FK_Debits_AccountBalances_BalanceAccountBalanceId",
                        column: x => x.BalanceAccountBalanceId,
                        principalTable: "AccountBalances",
                        principalColumn: "AccountBalanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalances_BankCustomerId",
                table: "AccountBalances",
                column: "BankCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_BalanceAccountBalanceId",
                table: "Credits",
                column: "BalanceAccountBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Debits_BalanceAccountBalanceId",
                table: "Debits",
                column: "BalanceAccountBalanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Debits");

            migrationBuilder.DropTable(
                name: "AccountBalances");

            migrationBuilder.DropTable(
                name: "BankCustomers");
        }
    }
}
