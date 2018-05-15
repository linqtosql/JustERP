using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JustERP.Migrations
{
    public partial class MyTimeChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalMillisecond",
                table: "PeopleActivities",
                newName: "TotalSeconds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSeconds",
                table: "PeopleActivities",
                newName: "TotalMillisecond");
        }
    }
}
