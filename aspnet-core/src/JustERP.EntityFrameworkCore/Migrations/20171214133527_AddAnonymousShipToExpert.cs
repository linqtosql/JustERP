using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class AddAnonymousShipToExpert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ExpertAccounts",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "ExpertAnonymousShips",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ExpertId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 16, nullable: false),
                    UserName = table.Column<string>(maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertAnonymousShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertAnonymousShips_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAnonymousShips_ExpertId",
                table: "ExpertAnonymousShips",
                column: "ExpertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertAnonymousShips");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ExpertAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16);
        }
    }
}
