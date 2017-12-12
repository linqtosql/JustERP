using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class AddExpertTypeToExpert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrderCharts_Experts_ReceiverExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrderCharts_Experts_SenderExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropIndex(
                name: "IX_ExpertOrderCharts_ReceiverExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropIndex(
                name: "IX_ExpertOrderCharts_SenderExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropColumn(
                name: "ReceiverExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropColumn(
                name: "SenderExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.AddColumn<long>(
                name: "ExpertType",
                table: "Experts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderCharts_ExperReceiverId",
                table: "ExpertOrderCharts",
                column: "ExperReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderCharts_ExpertId",
                table: "ExpertOrderCharts",
                column: "ExpertId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrderCharts_Experts_ExperReceiverId",
                table: "ExpertOrderCharts",
                column: "ExperReceiverId",
                principalTable: "Experts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrderCharts_Experts_ExpertId",
                table: "ExpertOrderCharts",
                column: "ExpertId",
                principalTable: "Experts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrderCharts_Experts_ExperReceiverId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrderCharts_Experts_ExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropIndex(
                name: "IX_ExpertOrderCharts_ExperReceiverId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropIndex(
                name: "IX_ExpertOrderCharts_ExpertId",
                table: "ExpertOrderCharts");

            migrationBuilder.DropColumn(
                name: "ExpertType",
                table: "Experts");

            migrationBuilder.AddColumn<long>(
                name: "ReceiverExpertId",
                table: "ExpertOrderCharts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SenderExpertId",
                table: "ExpertOrderCharts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderCharts_ReceiverExpertId",
                table: "ExpertOrderCharts",
                column: "ReceiverExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderCharts_SenderExpertId",
                table: "ExpertOrderCharts",
                column: "SenderExpertId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrderCharts_Experts_ReceiverExpertId",
                table: "ExpertOrderCharts",
                column: "ReceiverExpertId",
                principalTable: "Experts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrderCharts_Experts_SenderExpertId",
                table: "ExpertOrderCharts",
                column: "SenderExpertId",
                principalTable: "Experts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
