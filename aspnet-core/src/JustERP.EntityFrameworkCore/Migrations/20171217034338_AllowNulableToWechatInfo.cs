using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class AllowNulableToWechatInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unionid",
                table: "ExpertWechatInfos",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "ExpertWechatInfos",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "ExpertWechatInfos",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ExpertWechatInfos",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unionid",
                table: "ExpertWechatInfos",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "ExpertWechatInfos",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "ExpertWechatInfos",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ExpertWechatInfos",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);
        }
    }
}
