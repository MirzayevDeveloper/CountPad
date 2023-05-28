using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountPad.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class Initial7 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Orders_Packages_PackageId",
				table: "Orders");

			migrationBuilder.DropForeignKey(
				name: "FK_Packages_Distributors_DistributorId",
				table: "Packages");

			migrationBuilder.DropForeignKey(
				name: "FK_Packages_Products_ProductId",
				table: "Packages");

			migrationBuilder.DropTable(
				name: "PackageHistories");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Packages",
				table: "Packages");

			migrationBuilder.RenameTable(
				name: "Packages",
				newName: "Package");

			migrationBuilder.RenameIndex(
				name: "IX_Packages_ProductId",
				table: "Package",
				newName: "IX_Package_ProductId");

			migrationBuilder.RenameIndex(
				name: "IX_Packages_DistributorId",
				table: "Package",
				newName: "IX_Package_DistributorId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Package",
				table: "Package",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_Orders_Package_PackageId",
				table: "Orders",
				column: "PackageId",
				principalTable: "Package",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Package_Distributors_DistributorId",
				table: "Package",
				column: "DistributorId",
				principalTable: "Distributors",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Package_Products_ProductId",
				table: "Package",
				column: "ProductId",
				principalTable: "Products",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Orders_Package_PackageId",
				table: "Orders");

			migrationBuilder.DropForeignKey(
				name: "FK_Package_Distributors_DistributorId",
				table: "Package");

			migrationBuilder.DropForeignKey(
				name: "FK_Package_Products_ProductId",
				table: "Package");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Package",
				table: "Package");

			migrationBuilder.RenameTable(
				name: "Package",
				newName: "Packages");

			migrationBuilder.RenameIndex(
				name: "IX_Package_ProductId",
				table: "Packages",
				newName: "IX_Packages_ProductId");

			migrationBuilder.RenameIndex(
				name: "IX_Package_DistributorId",
				table: "Packages",
				newName: "IX_Packages_DistributorId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Packages",
				table: "Packages",
				column: "Id");

			migrationBuilder.CreateTable(
				name: "PackageHistories",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					DistributorId = table.Column<Guid>(type: "uuid", nullable: true),
					ProductId = table.Column<Guid>(type: "uuid", nullable: true),
					Count = table.Column<double>(type: "double precision", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					IncomingDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					IncomingPrice = table.Column<decimal>(type: "numeric", nullable: false),
					SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PackageHistories", x => x.Id);
					table.ForeignKey(
						name: "FK_PackageHistories_Distributors_DistributorId",
						column: x => x.DistributorId,
						principalTable: "Distributors",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_PackageHistories_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id");
				});

			migrationBuilder.CreateIndex(
				name: "IX_PackageHistories_DistributorId",
				table: "PackageHistories",
				column: "DistributorId");

			migrationBuilder.CreateIndex(
				name: "IX_PackageHistories_ProductId",
				table: "PackageHistories",
				column: "ProductId");

			migrationBuilder.AddForeignKey(
				name: "FK_Orders_Packages_PackageId",
				table: "Orders",
				column: "PackageId",
				principalTable: "Packages",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Packages_Distributors_DistributorId",
				table: "Packages",
				column: "DistributorId",
				principalTable: "Distributors",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Packages_Products_ProductId",
				table: "Packages",
				column: "ProductId",
				principalTable: "Products",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
