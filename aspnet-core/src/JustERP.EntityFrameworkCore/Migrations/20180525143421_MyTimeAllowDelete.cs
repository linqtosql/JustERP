using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class MyTimeAllowDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Peoples_PeopleId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleActivities_Activities_ActivityId",
                table: "PeopleActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleActivities_Peoples_PeopleId",
                table: "PeopleActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleActivityLabels_PeopleActivities_PeopleActivityId",
                table: "PeopleActivityLabels");

            migrationBuilder.AlterColumn<long>(
                name: "PeopleId",
                table: "PeopleActivities",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ActivityId",
                table: "PeopleActivities",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Peoples_PeopleId",
                table: "Activities",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleActivities_Activities_ActivityId",
                table: "PeopleActivities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleActivities_Peoples_PeopleId",
                table: "PeopleActivities",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleActivityLabels_PeopleActivities_PeopleActivityId",
                table: "PeopleActivityLabels",
                column: "PeopleActivityId",
                principalTable: "PeopleActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Peoples_PeopleId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleActivities_Activities_ActivityId",
                table: "PeopleActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleActivities_Peoples_PeopleId",
                table: "PeopleActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleActivityLabels_PeopleActivities_PeopleActivityId",
                table: "PeopleActivityLabels");

            migrationBuilder.AlterColumn<long>(
                name: "PeopleId",
                table: "PeopleActivities",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ActivityId",
                table: "PeopleActivities",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Peoples_PeopleId",
                table: "Activities",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleActivities_Activities_ActivityId",
                table: "PeopleActivities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleActivities_Peoples_PeopleId",
                table: "PeopleActivities",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleActivityLabels_PeopleActivities_PeopleActivityId",
                table: "PeopleActivityLabels",
                column: "PeopleActivityId",
                principalTable: "PeopleActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
