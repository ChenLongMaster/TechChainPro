using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDAL.Migrations
{
    public partial class Update_User_Role_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_User_UsersId",
                table: "RoleUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "RoleUser",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                newName: "IX_RoleUser_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_User_UserId",
                table: "RoleUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_User_UserId",
                table: "RoleUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RoleUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UserId",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersId");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 863, DateTimeKind.Local).AddTicks(6764));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 865, DateTimeKind.Local).AddTicks(8894));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 865, DateTimeKind.Local).AddTicks(8928));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 865, DateTimeKind.Local).AddTicks(8932));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 865, DateTimeKind.Local).AddTicks(8935));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("924fecba-2c1b-451c-92cd-83b92d8af6c3"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 869, DateTimeKind.Local).AddTicks(2204));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d1823e23-02e3-443c-b0dc-85e46c97b10e"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 869, DateTimeKind.Local).AddTicks(1168));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d4314259-9d36-4af7-b75a-77f24e15600a"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 6, 11, 19, 15, 869, DateTimeKind.Local).AddTicks(2180));

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_User_UsersId",
                table: "RoleUser",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
