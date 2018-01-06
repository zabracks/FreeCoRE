﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyRE.Web.Data.Migrations
{
    public partial class moreexpressiontypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FunctionName",
                table: "Expressions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VariableName",
                table: "Expressions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FunctionParameter",
                columns: table => new
                {
                    FunctionParameterId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvocationExpressionExpressionId = table.Column<long>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    ValueExpressionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionParameter", x => x.FunctionParameterId);
                    table.ForeignKey(
                        name: "FK_FunctionParameter_Expressions_InvocationExpressionExpressionId",
                        column: x => x.InvocationExpressionExpressionId,
                        principalTable: "Expressions",
                        principalColumn: "ExpressionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionParameter_Expressions_ValueExpressionId",
                        column: x => x.ValueExpressionId,
                        principalTable: "Expressions",
                        principalColumn: "ExpressionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionParameter_InvocationExpressionExpressionId",
                table: "FunctionParameter",
                column: "InvocationExpressionExpressionId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionParameter_ValueExpressionId",
                table: "FunctionParameter",
                column: "ValueExpressionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionParameter");

            migrationBuilder.DropColumn(
                name: "FunctionName",
                table: "Expressions");

            migrationBuilder.DropColumn(
                name: "VariableName",
                table: "Expressions");
        }
    }
}
