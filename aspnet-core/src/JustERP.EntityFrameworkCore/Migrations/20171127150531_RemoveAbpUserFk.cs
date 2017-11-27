using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class RemoveAbpUserFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrders_AbpUsers_CreatorUserId",
                table: "ExpertOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrders_AbpUsers_DeleterUserId",
                table: "ExpertOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrders_AbpUsers_LastModifierUserId",
                table: "ExpertOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Experts_AbpUsers_CreatorUserId",
                table: "Experts");

            migrationBuilder.DropForeignKey(
                name: "FK_Experts_AbpUsers_DeleterUserId",
                table: "Experts");

            migrationBuilder.DropForeignKey(
                name: "FK_Experts_AbpUsers_LastModifierUserId",
                table: "Experts");

            migrationBuilder.DropIndex(
                name: "IX_Experts_CreatorUserId",
                table: "Experts");

            migrationBuilder.DropIndex(
                name: "IX_Experts_DeleterUserId",
                table: "Experts");

            migrationBuilder.DropIndex(
                name: "IX_Experts_LastModifierUserId",
                table: "Experts");

            migrationBuilder.DropIndex(
                name: "IX_ExpertOrders_CreatorUserId",
                table: "ExpertOrders");

            migrationBuilder.DropIndex(
                name: "IX_ExpertOrders_DeleterUserId",
                table: "ExpertOrders");

            migrationBuilder.DropIndex(
                name: "IX_ExpertOrders_LastModifierUserId",
                table: "ExpertOrders");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ExpertWorkSettings");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ExpertWorkSettings");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "ExpertWorkSettings");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ExpertWorkSettings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ExpertWorkSettings");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ExpertWorkSettings");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ExpertWorkSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ExpertWorkSettings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "ExpertWorkSettings",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "ExpertWorkSettings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ExpertWorkSettings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ExpertWorkSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ExpertWorkSettings",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "ExpertWorkSettings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experts_CreatorUserId",
                table: "Experts",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_DeleterUserId",
                table: "Experts",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_LastModifierUserId",
                table: "Experts",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_CreatorUserId",
                table: "ExpertOrders",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_DeleterUserId",
                table: "ExpertOrders",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_LastModifierUserId",
                table: "ExpertOrders",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrders_AbpUsers_CreatorUserId",
                table: "ExpertOrders",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrders_AbpUsers_DeleterUserId",
                table: "ExpertOrders",
                column: "DeleterUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrders_AbpUsers_LastModifierUserId",
                table: "ExpertOrders",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experts_AbpUsers_CreatorUserId",
                table: "Experts",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experts_AbpUsers_DeleterUserId",
                table: "Experts",
                column: "DeleterUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experts_AbpUsers_LastModifierUserId",
                table: "Experts",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
