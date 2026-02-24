using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanOriginationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDealReviewFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanTypes",
                columns: table => new
                {
                    loanTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    loanTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTypes", x => x.loanTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LoanDeals",
                columns: table => new
                {
                    dealId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    custId = table.Column<int>(type: "int", nullable: false),
                    scorecardId = table.Column<int>(type: "int", nullable: false),
                    loanTypeId = table.Column<int>(type: "int", nullable: false),
                    eligibleAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    approvedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    riskRating = table.Column<int>(type: "int", nullable: false),
                    cibilScore = table.Column<int>(type: "int", nullable: false),
                    currentStatus = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDeals", x => x.dealId);
                    table.ForeignKey(
                        name: "FK_LoanDeals_LoanTypes_loanTypeId",
                        column: x => x.loanTypeId,
                        principalTable: "LoanTypes",
                        principalColumn: "loanTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DealReviews",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dealId = table.Column<int>(type: "int", nullable: false),
                    officerId = table.Column<int>(type: "int", nullable: false),
                    decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    decisionReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<int>(type: "int", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealReviews", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_DealReviews_LoanDeals_dealId",
                        column: x => x.dealId,
                        principalTable: "LoanDeals",
                        principalColumn: "dealId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DealReviews_dealId",
                table: "DealReviews",
                column: "dealId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDeals_loanTypeId",
                table: "LoanDeals",
                column: "loanTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealReviews");

            migrationBuilder.DropTable(
                name: "LoanDeals");

            migrationBuilder.DropTable(
                name: "LoanTypes");
        }
    }
}
