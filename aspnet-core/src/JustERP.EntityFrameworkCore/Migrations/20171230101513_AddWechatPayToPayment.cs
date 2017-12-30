using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class AddWechatPayToPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrderPayments_ExpertOrders_ExpertOrderId",
                table: "ExpertOrderPayments");

            migrationBuilder.AlterColumn<long>(
                name: "ExpertOrderId",
                table: "ExpertOrderPayments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ExpertId",
                table: "ExpertOrderPayments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PaymentChannel",
                table: "ExpertOrderPayments",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentNo",
                table: "ExpertOrderPayments",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "ExpertOrderPayments",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrderPayments_ExpertOrders_ExpertOrderId",
                table: "ExpertOrderPayments",
                column: "ExpertOrderId",
                principalTable: "ExpertOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertOrderPayments_ExpertOrders_ExpertOrderId",
                table: "ExpertOrderPayments");

            migrationBuilder.DropColumn(
                name: "PaymentChannel",
                table: "ExpertOrderPayments");

            migrationBuilder.DropColumn(
                name: "PaymentNo",
                table: "ExpertOrderPayments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ExpertOrderPayments");

            migrationBuilder.AlterColumn<long>(
                name: "ExpertOrderId",
                table: "ExpertOrderPayments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ExpertId",
                table: "ExpertOrderPayments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertOrderPayments_ExpertOrders_ExpertOrderId",
                table: "ExpertOrderPayments",
                column: "ExpertOrderId",
                principalTable: "ExpertOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
