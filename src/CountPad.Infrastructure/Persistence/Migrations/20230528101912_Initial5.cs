﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountPad.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class Initial5 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTimeOffset>(
				name: "IncomingDate",
				table: "Packages",
				type: "timestamp with time zone",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldType: "timestamp without time zone");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTime>(
				name: "IncomingDate",
				table: "Packages",
				type: "timestamp without time zone",
				nullable: false,
				oldClrType: typeof(DateTimeOffset),
				oldType: "timestamp with time zone");
		}
	}
}
