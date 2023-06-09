﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountPad.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Distributors",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					CompanyPhone = table.Column<string>(type: "text", nullable: true),
					DelivererPhone = table.Column<string>(type: "text", nullable: true),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Distributors", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Permissions",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					PermissionName = table.Column<string>(type: "text", nullable: true),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Permissions", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ProductCategory",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					Description = table.Column<string>(type: "text", nullable: true),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProductCategory", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "RefreshTokens",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Phone = table.Column<string>(type: "text", nullable: true),
					RefreshToken = table.Column<string>(type: "text", nullable: true),
					AccessTokenExpiredDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					RefreshTokenExpiredDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RefreshTokens", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Roles",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					RoleName = table.Column<string>(type: "text", nullable: true),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Roles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					Phone = table.Column<string>(type: "text", nullable: true),
					Password = table.Column<string>(type: "text", nullable: true),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					ProductCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
					Description = table.Column<string>(type: "text", nullable: true),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Products", x => x.Id);
					table.ForeignKey(
						name: "FK_Products_ProductCategory_ProductCategoryId",
						column: x => x.ProductCategoryId,
						principalTable: "ProductCategory",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "PermissionRole",
				columns: table => new
				{
					PermissionsId = table.Column<Guid>(type: "uuid", nullable: false),
					RolesId = table.Column<Guid>(type: "uuid", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsId, x.RolesId });
					table.ForeignKey(
						name: "FK_PermissionRole_Permissions_PermissionsId",
						column: x => x.PermissionsId,
						principalTable: "Permissions",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_PermissionRole_Roles_RolesId",
						column: x => x.RolesId,
						principalTable: "Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "RoleUser",
				columns: table => new
				{
					RolesId = table.Column<Guid>(type: "uuid", nullable: false),
					UsersId = table.Column<Guid>(type: "uuid", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
					table.ForeignKey(
						name: "FK_RoleUser_Roles_RolesId",
						column: x => x.RolesId,
						principalTable: "Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_RoleUser_Users_UsersId",
						column: x => x.UsersId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Solds",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					SoldDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Solds", x => x.Id);
					table.ForeignKey(
						name: "FK_Solds_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Packages",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					Count = table.Column<double>(type: "double precision", nullable: false),
					IncomingPrice = table.Column<decimal>(type: "numeric", nullable: false),
					SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
					IncomingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
					ProductId = table.Column<Guid>(type: "uuid", nullable: false),
					DistributorId = table.Column<Guid>(type: "uuid", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Packages", x => x.Id);
					table.ForeignKey(
						name: "FK_Packages_Distributors_DistributorId",
						column: x => x.DistributorId,
						principalTable: "Distributors",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Packages_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Orders",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					PackageId = table.Column<Guid>(type: "uuid", nullable: false),
					SoldId = table.Column<Guid>(type: "uuid", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					UpdatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Orders", x => x.Id);
					table.ForeignKey(
						name: "FK_Orders_Packages_PackageId",
						column: x => x.PackageId,
						principalTable: "Packages",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Orders_Solds_SoldId",
						column: x => x.SoldId,
						principalTable: "Solds",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Orders_PackageId",
				table: "Orders",
				column: "PackageId");

			migrationBuilder.CreateIndex(
				name: "IX_Orders_SoldId",
				table: "Orders",
				column: "SoldId");

			migrationBuilder.CreateIndex(
				name: "IX_Packages_DistributorId",
				table: "Packages",
				column: "DistributorId");

			migrationBuilder.CreateIndex(
				name: "IX_Packages_ProductId",
				table: "Packages",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_PermissionRole_RolesId",
				table: "PermissionRole",
				column: "RolesId");

			migrationBuilder.CreateIndex(
				name: "IX_Permissions_PermissionName",
				table: "Permissions",
				column: "PermissionName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Products_ProductCategoryId",
				table: "Products",
				column: "ProductCategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Roles_RoleName",
				table: "Roles",
				column: "RoleName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_RoleUser_UsersId",
				table: "RoleUser",
				column: "UsersId");

			migrationBuilder.CreateIndex(
				name: "IX_Solds_UserId",
				table: "Solds",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_Phone",
				table: "Users",
				column: "Phone",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Orders");

			migrationBuilder.DropTable(
				name: "PermissionRole");

			migrationBuilder.DropTable(
				name: "RefreshTokens");

			migrationBuilder.DropTable(
				name: "RoleUser");

			migrationBuilder.DropTable(
				name: "Packages");

			migrationBuilder.DropTable(
				name: "Solds");

			migrationBuilder.DropTable(
				name: "Permissions");

			migrationBuilder.DropTable(
				name: "Roles");

			migrationBuilder.DropTable(
				name: "Distributors");

			migrationBuilder.DropTable(
				name: "Products");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "ProductCategory");
		}
	}
}
