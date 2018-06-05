using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class MyTimeAddTimezone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimezoneInfo",
                table: "Peoples",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimezoneOffset",
                table: "Peoples",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimezoneInfo",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "TimezoneOffset",
                table: "Peoples");
        }
    }
}
