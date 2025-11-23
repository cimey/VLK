using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class stockhistoryadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StockHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDateStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EffectiveDateEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockHistories_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientStockPositions_StockId",
                table: "ClientStockPositions",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_StockId",
                table: "StockHistories",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientStockPositions_Stocks_StockId",
                table: "ClientStockPositions",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientStockPositions_Stocks_StockId",
                table: "ClientStockPositions");

            migrationBuilder.DropTable(
                name: "StockHistories");

            migrationBuilder.DropIndex(
                name: "IX_ClientStockPositions_StockId",
                table: "ClientStockPositions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");
        }
    }
}
