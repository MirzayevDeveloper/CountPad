using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountPad.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class Initial2 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Products_ProductCategory_ProductCategoryId",
				table: "Products");

			migrationBuilder.DropPrimaryKey(
				name: "PK_ProductCategory",
				table: "ProductCategory");

			migrationBuilder.DropColumn(
				name: "Description",
				table: "ProductCategory");

			migrationBuilder.RenameTable(
				name: "ProductCategory",
				newName: "ProductCategories");

			migrationBuilder.AddPrimaryKey(
				name: "PK_ProductCategories",
				table: "ProductCategories",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_ProductCategories_Name",
				table: "ProductCategories",
				column: "Name",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_Products_ProductCategories_ProductCategoryId",
				table: "Products",
				column: "ProductCategoryId",
				principalTable: "ProductCategories",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Products_ProductCategories_ProductCategoryId",
				table: "Products");

			migrationBuilder.DropPrimaryKey(
				name: "PK_ProductCategories",
				table: "ProductCategories");

			migrationBuilder.DropIndex(
				name: "IX_ProductCategories_Name",
				table: "ProductCategories");

			migrationBuilder.RenameTable(
				name: "ProductCategories",
				newName: "ProductCategory");

			migrationBuilder.AddColumn<string>(
				name: "Description",
				table: "ProductCategory",
				type: "text",
				nullable: true);

			migrationBuilder.AddPrimaryKey(
				name: "PK_ProductCategory",
				table: "ProductCategory",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_Products_ProductCategory_ProductCategoryId",
				table: "Products",
				column: "ProductCategoryId",
				principalTable: "ProductCategory",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
