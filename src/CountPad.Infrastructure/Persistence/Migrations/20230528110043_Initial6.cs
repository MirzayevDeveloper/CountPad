using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountPad.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class Initial6 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Orders_Solds_SoldId",
				table: "Orders");

			migrationBuilder.DropTable(
				name: "Solds");

			migrationBuilder.RenameColumn(
				name: "SoldId",
				table: "Orders",
				newName: "UserId");

			migrationBuilder.RenameIndex(
				name: "IX_Orders_SoldId",
				table: "Orders",
				newName: "IX_Orders_UserId");

			migrationBuilder.AddColumn<double>(
				name: "Count",
				table: "Orders",
				type: "double precision",
				nullable: false,
				defaultValue: 0.0);

			migrationBuilder.AddColumn<DateTimeOffset>(
				name: "SoldDate",
				table: "Orders",
				type: "timestamp with time zone",
				nullable: false,
				defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

			migrationBuilder.AddColumn<double>(
				name: "SoldPrice",
				table: "Orders",
				type: "double precision",
				nullable: false,
				defaultValue: 0.0);

			migrationBuilder.AddForeignKey(
				name: "FK_Orders_Users_UserId",
				table: "Orders",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Orders_Users_UserId",
				table: "Orders");

			migrationBuilder.DropColumn(
				name: "Count",
				table: "Orders");

			migrationBuilder.DropColumn(
				name: "SoldDate",
				table: "Orders");

			migrationBuilder.DropColumn(
				name: "SoldPrice",
				table: "Orders");

			migrationBuilder.RenameColumn(
				name: "UserId",
				table: "Orders",
				newName: "SoldId");

			migrationBuilder.RenameIndex(
				name: "IX_Orders_UserId",
				table: "Orders",
				newName: "IX_Orders_SoldId");

			migrationBuilder.CreateTable(
				name: "Solds",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					CreatedDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
					SoldDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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

			migrationBuilder.CreateIndex(
				name: "IX_Solds_UserId",
				table: "Solds",
				column: "UserId");

			migrationBuilder.AddForeignKey(
				name: "FK_Orders_Solds_SoldId",
				table: "Orders",
				column: "SoldId",
				principalTable: "Solds",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
