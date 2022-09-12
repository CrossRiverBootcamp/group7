using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerAccount.Storage.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationsHistory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    TransactionID = table.Column<int>(type: "int", nullable: false),
                    IsDebit = table.Column<bool>(type: "bit", nullable: false),
                    TransactionAmount = table.Column<float>(type: "real", nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OperationsHistory_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationsHistory_AccountId",
                table: "OperationsHistory",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsHistory");
        }
    }
}
