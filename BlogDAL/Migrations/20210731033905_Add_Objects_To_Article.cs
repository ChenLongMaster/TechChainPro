using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDAL.Migrations
{
    public partial class Add_Objects_To_Article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByNameId",
                table: "Article",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 31, 10, 39, 5, 54, DateTimeKind.Local).AddTicks(919));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 31, 10, 39, 5, 55, DateTimeKind.Local).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 31, 10, 39, 5, 55, DateTimeKind.Local).AddTicks(3834));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 31, 10, 39, 5, 55, DateTimeKind.Local).AddTicks(3836));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 31, 10, 39, 5, 55, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryId",
                table: "Article",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CreatedByNameId",
                table: "Article",
                column: "CreatedByNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_User_CreatedByNameId",
                table: "Article",
                column: "CreatedByNameId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_User_CreatedByNameId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_CategoryId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_CreatedByNameId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "CreatedByNameId",
                table: "Article");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 30, 10, 37, 30, 181, DateTimeKind.Local).AddTicks(1782));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 30, 10, 37, 30, 184, DateTimeKind.Local).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 30, 10, 37, 30, 184, DateTimeKind.Local).AddTicks(3919));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 30, 10, 37, 30, 184, DateTimeKind.Local).AddTicks(3924));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2021, 7, 30, 10, 37, 30, 184, DateTimeKind.Local).AddTicks(3926));
        }
    }
}
