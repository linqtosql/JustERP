using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class InitLhzx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpertAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertAccounts", x => x.Id);
                    table.UniqueConstraint("AK_ExpertAccounts_UserName", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "ExpertClasses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Background = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 16, nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    Turn = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertClasses_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertClasses_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertClasses_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertClasses_ExpertClasses_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ExpertClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AlipayAccount = table.Column<string>(maxLength: 32, nullable: true),
                    Avatar = table.Column<string>(maxLength: 128, nullable: true),
                    AvgTime = table.Column<double>(nullable: true),
                    BackgroundImage = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DurationPerTime = table.Column<int>(nullable: true),
                    ExpertAccountId = table.Column<long>(nullable: false),
                    ExpertClassId = table.Column<long>(nullable: true),
                    ExpertFirstClassId = table.Column<long>(nullable: true),
                    ExtensionData = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(maxLength: 512, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsExpert = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 16, nullable: false),
                    NickName = table.Column<string>(maxLength: 16, nullable: true),
                    OnlineStatus = table.Column<int>(nullable: true),
                    Organization = table.Column<string>(maxLength: 32, nullable: true),
                    Phone = table.Column<string>(maxLength: 16, nullable: false),
                    Post = table.Column<string>(maxLength: 32, nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    RegisterIpAddress = table.Column<string>(maxLength: 16, nullable: true),
                    Score = table.Column<double>(nullable: true),
                    ServicesCount = table.Column<int>(nullable: true),
                    Speciality = table.Column<string>(maxLength: 512, nullable: true),
                    Tags = table.Column<string>(maxLength: 64, nullable: true),
                    WeixinAccount = table.Column<string>(maxLength: 32, nullable: true),
                    WorkYears = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.UniqueConstraint("AK_Experts_ExpertAccountId", x => x.ExpertAccountId);
                    table.ForeignKey(
                        name: "FK_Experts_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experts_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experts_ExpertAccounts_ExpertAccountId",
                        column: x => x.ExpertAccountId,
                        principalTable: "ExpertAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experts_ExpertClasses_ExpertClassId",
                        column: x => x.ExpertClassId,
                        principalTable: "ExpertClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experts_ExpertClasses_ExpertFirstClassId",
                        column: x => x.ExpertFirstClassId,
                        principalTable: "ExpertClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experts_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertFriendShips",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ExpertFriendId = table.Column<long>(nullable: false),
                    ExpertId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertFriendShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertFriendShips_Experts_ExpertFriendId",
                        column: x => x.ExpertFriendId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    ConfirmDatetime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ExpertId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrderNo = table.Column<string>(maxLength: 16, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    QuestionRemark = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(maxLength: 512, nullable: true),
                    ServerExpertId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TotalDuration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertOrders", x => x.Id);
                    table.UniqueConstraint("AK_ExpertOrders_OrderNo", x => x.OrderNo);
                    table.ForeignKey(
                        name: "FK_ExpertOrders_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertOrders_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertOrders_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertOrders_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertOrders_Experts_ServerExpertId",
                        column: x => x.ServerExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertWorkSettings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ExpertId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Week = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertWorkSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertWorkSettings_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertComments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommenterExpertId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ExpertId = table.Column<long>(nullable: false),
                    ExpertOrderId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertComments_Experts_CommenterExpertId",
                        column: x => x.CommenterExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertComments_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertComments_ExpertOrders_ExpertOrderId",
                        column: x => x.ExpertOrderId,
                        principalTable: "ExpertOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertComments_ExpertComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ExpertComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertOrderCharts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ExperReceiverId = table.Column<long>(nullable: false),
                    ExpertId = table.Column<long>(nullable: false),
                    ExpertOrderId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ReceiverExpertId = table.Column<long>(nullable: true),
                    SenderExpertId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertOrderCharts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertOrderCharts_ExpertOrders_ExpertOrderId",
                        column: x => x.ExpertOrderId,
                        principalTable: "ExpertOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertOrderCharts_Experts_ReceiverExpertId",
                        column: x => x.ReceiverExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertOrderCharts_Experts_SenderExpertId",
                        column: x => x.SenderExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertOrderLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ExpertOrderId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertOrderLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertOrderLogs_ExpertOrders_ExpertOrderId",
                        column: x => x.ExpertOrderId,
                        principalTable: "ExpertOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertOrderPayments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ExpertId = table.Column<long>(nullable: true),
                    ExpertOrderId = table.Column<long>(nullable: true),
                    ExtensionData = table.Column<string>(nullable: true),
                    PaymentTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertOrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertOrderPayments_ExpertOrders_ExpertOrderId",
                        column: x => x.ExpertOrderId,
                        principalTable: "ExpertOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertOrderRefunds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ExpertId = table.Column<long>(nullable: true),
                    ExpertOrderId = table.Column<long>(nullable: true),
                    ExtensionData = table.Column<string>(nullable: true),
                    RefundTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertOrderRefunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertOrderRefunds_ExpertOrders_ExpertOrderId",
                        column: x => x.ExpertOrderId,
                        principalTable: "ExpertOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpertClasses_CreatorUserId",
                table: "ExpertClasses",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertClasses_DeleterUserId",
                table: "ExpertClasses",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertClasses_LastModifierUserId",
                table: "ExpertClasses",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertClasses_ParentId",
                table: "ExpertClasses",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertComments_CommenterExpertId",
                table: "ExpertComments",
                column: "CommenterExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertComments_ExpertId",
                table: "ExpertComments",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertComments_ExpertOrderId",
                table: "ExpertComments",
                column: "ExpertOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertComments_ParentId",
                table: "ExpertComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertFriendShips_ExpertFriendId",
                table: "ExpertFriendShips",
                column: "ExpertFriendId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderCharts_ExpertOrderId",
                table: "ExpertOrderCharts",
                column: "ExpertOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderCharts_ReceiverExpertId",
                table: "ExpertOrderCharts",
                column: "ReceiverExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderCharts_SenderExpertId",
                table: "ExpertOrderCharts",
                column: "SenderExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderLogs_ExpertOrderId",
                table: "ExpertOrderLogs",
                column: "ExpertOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderPayments_ExpertOrderId",
                table: "ExpertOrderPayments",
                column: "ExpertOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrderRefunds_ExpertOrderId",
                table: "ExpertOrderRefunds",
                column: "ExpertOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_CreatorUserId",
                table: "ExpertOrders",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_DeleterUserId",
                table: "ExpertOrders",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_ExpertId",
                table: "ExpertOrders",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_LastModifierUserId",
                table: "ExpertOrders",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertOrders_ServerExpertId",
                table: "ExpertOrders",
                column: "ServerExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_CreatorUserId",
                table: "Experts",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_DeleterUserId",
                table: "Experts",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_ExpertClassId",
                table: "Experts",
                column: "ExpertClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_ExpertFirstClassId",
                table: "Experts",
                column: "ExpertFirstClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_LastModifierUserId",
                table: "Experts",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertWorkSettings_ExpertId",
                table: "ExpertWorkSettings",
                column: "ExpertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertComments");

            migrationBuilder.DropTable(
                name: "ExpertFriendShips");

            migrationBuilder.DropTable(
                name: "ExpertOrderCharts");

            migrationBuilder.DropTable(
                name: "ExpertOrderLogs");

            migrationBuilder.DropTable(
                name: "ExpertOrderPayments");

            migrationBuilder.DropTable(
                name: "ExpertOrderRefunds");

            migrationBuilder.DropTable(
                name: "ExpertWorkSettings");

            migrationBuilder.DropTable(
                name: "ExpertOrders");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "ExpertAccounts");

            migrationBuilder.DropTable(
                name: "ExpertClasses");
        }
    }
}
