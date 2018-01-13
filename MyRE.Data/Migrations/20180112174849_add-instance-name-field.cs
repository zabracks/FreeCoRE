﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRE.Data.Migrations
{
    public partial class addinstancenamefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AppInstances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AppInstances");
        }
    }
}
