using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class MyTimeAllowPeopleNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "Peoples",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarImg",
                table: "Peoples",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "Peoples",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarImg",
                table: "Peoples",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
