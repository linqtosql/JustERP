using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class MyTimeInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AbpUserTokens",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AbpUserTokens",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AbpUserTokens",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecurityStamp",
                table: "AbpUsers",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AbpUsers",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpUsers",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpRoles",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LabelCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExtensionData = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeopleWechatInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(maxLength: 16, nullable: true),
                    Country = table.Column<string>(maxLength: 16, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Headimgurl = table.Column<string>(maxLength: 512, nullable: false),
                    Nickname = table.Column<string>(maxLength: 32, nullable: false),
                    Openid = table.Column<string>(maxLength: 128, nullable: false),
                    Province = table.Column<string>(maxLength: 16, nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Unionid = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleWechatInfos", x => x.Id);
                    table.UniqueConstraint("AK_PeopleWechatInfos_Openid", x => x.Openid);
                });

            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AvatarImg = table.Column<string>(maxLength: 128, nullable: true),
                    BackgroundColor = table.Column<string>(maxLength: 8, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NickName = table.Column<string>(maxLength: 32, nullable: true),
                    Openid = table.Column<string>(maxLength: 128, nullable: false),
                    PeopleWechatInfoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                    table.UniqueConstraint("AK_Peoples_Openid", x => x.Openid);
                    table.ForeignKey(
                        name: "FK_Peoples_PeopleWechatInfos_PeopleWechatInfoId",
                        column: x => x.PeopleWechatInfoId,
                        principalTable: "PeopleWechatInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Icon = table.Column<string>(maxLength: 64, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsSystem = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    PeopleId = table.Column<long>(nullable: true),
                    Turn = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LabelCategoryId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    PeopleId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Labels_LabelCategories_LabelCategoryId",
                        column: x => x.LabelCategoryId,
                        principalTable: "LabelCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Labels_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleActivities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivityIcon = table.Column<string>(maxLength: 64, nullable: false),
                    ActivityId = table.Column<long>(nullable: false),
                    ActivityName = table.Column<string>(maxLength: 32, nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    PeopleId = table.Column<long>(nullable: false),
                    TotalSeconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleActivities_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleActivityLabels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LabelCategoryId = table.Column<long>(nullable: false),
                    LabelName = table.Column<long>(maxLength: 64, nullable: false),
                    PeopleActivityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleActivityLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleActivityLabels_LabelCategories_LabelCategoryId",
                        column: x => x.LabelCategoryId,
                        principalTable: "LabelCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleActivityLabels_PeopleActivities_PeopleActivityId",
                        column: x => x.PeopleActivityId,
                        principalTable: "PeopleActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_PeopleId",
                table: "Activities",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_LabelCategoryId",
                table: "Labels",
                column: "LabelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_PeopleId",
                table: "Labels",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleActivities_ActivityId",
                table: "PeopleActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleActivities_PeopleId",
                table: "PeopleActivities",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleActivityLabels_LabelCategoryId",
                table: "PeopleActivityLabels",
                column: "LabelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleActivityLabels_PeopleActivityId",
                table: "PeopleActivityLabels",
                column: "PeopleActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_PeopleWechatInfoId",
                table: "Peoples",
                column: "PeopleWechatInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "PeopleActivityLabels");

            migrationBuilder.DropTable(
                name: "LabelCategories");

            migrationBuilder.DropTable(
                name: "PeopleActivities");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Peoples");

            migrationBuilder.DropTable(
                name: "PeopleWechatInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AbpUserTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AbpUserTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AbpUserTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecurityStamp",
                table: "AbpUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AbpUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyStamp",
                table: "AbpRoles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
