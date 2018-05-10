using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class AddMyTimeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_PeopleWechatInfos_Openid",
                table: "PeopleWechatInfos",
                column: "Openid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Peoples_Openid",
                table: "Peoples",
                column: "Openid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_PeopleWechatInfos_Openid",
                table: "PeopleWechatInfos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Peoples_Openid",
                table: "Peoples");
        }
    }
}
