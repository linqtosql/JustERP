using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class AddWechatinfoToExpert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OpenId",
                table: "Experts",
                maxLength: 128,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExpertWechatInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(maxLength: 16, nullable: false),
                    Country = table.Column<string>(maxLength: 16, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Headimgurl = table.Column<string>(maxLength: 128, nullable: false),
                    Nickname = table.Column<string>(maxLength: 32, nullable: false),
                    Openid = table.Column<string>(maxLength: 128, nullable: false),
                    Province = table.Column<string>(maxLength: 16, nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Unionid = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertWechatInfos", x => x.Id);
                    table.UniqueConstraint("AK_ExpertWechatInfos_Openid", x => x.Openid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertWechatInfos");

            migrationBuilder.DropColumn(
                name: "OpenId",
                table: "Experts");
        }
    }
}
