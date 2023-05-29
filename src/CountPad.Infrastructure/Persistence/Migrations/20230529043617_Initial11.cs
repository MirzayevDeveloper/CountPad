﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountPad.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Packages");

            migrationBuilder.CreateTable(
                name: "PackageHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<double>(type: "double precision", nullable: false),
                    IncomingPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IncomingDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistributorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageHistories_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageHistories_DistributorId",
                table: "PackageHistories",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageHistories_ProductId",
                table: "PackageHistories",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageHistories");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Packages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
