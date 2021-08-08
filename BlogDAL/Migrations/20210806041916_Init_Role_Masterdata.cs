using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDAL.Migrations
{
    public partial class Init_Role_Masterdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedOn", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("d1823e23-02e3-443c-b0dc-85e46c97b10e"), new DateTime(2021, 8, 6, 11, 19, 15, 869, DateTimeKind.Local).AddTicks(1168), false, "Admin" },
                    { new Guid("d4314259-9d36-4af7-b75a-77f24e15600a"), new DateTime(2021, 8, 6, 11, 19, 15, 869, DateTimeKind.Local).AddTicks(2180), false, "Moderator" },
                    { new Guid("924fecba-2c1b-451c-92cd-83b92d8af6c3"), new DateTime(2021, 8, 6, 11, 19, 15, 869, DateTimeKind.Local).AddTicks(2204), false, "Member" }
                });
        }
    }
}
