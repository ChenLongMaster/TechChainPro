using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 847, DateTimeKind.Local).AddTicks(6489));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 852, DateTimeKind.Local).AddTicks(5601));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 852, DateTimeKind.Local).AddTicks(5652));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 852, DateTimeKind.Local).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 852, DateTimeKind.Local).AddTicks(5659));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("924fecba-2c1b-451c-92cd-83b92d8af6c3"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 857, DateTimeKind.Local).AddTicks(1541));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d1823e23-02e3-443c-b0dc-85e46c97b10e"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 857, DateTimeKind.Local).AddTicks(70));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d4314259-9d36-4af7-b75a-77f24e15600a"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 18, 59, 28, 857, DateTimeKind.Local).AddTicks(1515));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 880, DateTimeKind.Local).AddTicks(8246));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 882, DateTimeKind.Local).AddTicks(2089));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 882, DateTimeKind.Local).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 882, DateTimeKind.Local).AddTicks(2112));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 882, DateTimeKind.Local).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("924fecba-2c1b-451c-92cd-83b92d8af6c3"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 883, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d1823e23-02e3-443c-b0dc-85e46c97b10e"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 883, DateTimeKind.Local).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d4314259-9d36-4af7-b75a-77f24e15600a"),
                column: "CreatedOn",
                value: new DateTime(2021, 8, 7, 5, 51, 36, 883, DateTimeKind.Local).AddTicks(4521));
        }
    }
}
