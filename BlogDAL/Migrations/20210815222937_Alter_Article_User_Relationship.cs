using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDAL.Migrations
{
    public partial class Alter_Article_User_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_User_CreatedByNameId",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "CreatedByNameId",
                table: "Article",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_CreatedByNameId",
                table: "Article",
                newName: "IX_Article_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_User_AuthorId",
                table: "Article",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
