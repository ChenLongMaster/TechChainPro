using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDAL.Migrations
{
    public partial class Add_Abstract_And_RepresentImage_For_Article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "User",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Article",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Article",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "representImageUrl",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "representImageUrl",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "UserName");
        }
    }
}
