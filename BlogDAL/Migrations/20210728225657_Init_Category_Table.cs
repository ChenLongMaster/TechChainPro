using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDALOld.Migrations
{
    public partial class Init_Category_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "representImageUrl",
                table: "Article",
                newName: "RepresentImageUrl");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Article",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedOn", "Introduction", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 7, 29, 5, 56, 57, 392, DateTimeKind.Local).AddTicks(5290), "<p>With a variety of topics to discuss,<i><strong> feel free to contribute your articles to my website.</strong></i></span></p>", "All Categories" },
                    { 2, new DateTime(2021, 7, 29, 5, 56, 57, 393, DateTimeKind.Local).AddTicks(7784), "<p><strong>ASP.NET Core</strong> is the open-source version of ASP.NET, that runs on macOS, Linux, and Windows. ASP.NET Core was first released in 2016 and is a re-design of earlier Windows-only versions of ASP.NET.</p>", "ASP.NET Core" },
                    { 3, new DateTime(2021, 7, 29, 5, 56, 57, 393, DateTimeKind.Local).AddTicks(7804), "<p><strong>Angular </strong>is a platform and framework for building single-page client applications using HTML and TypeScript.</p>", "Angular" },
                    { 4, new DateTime(2021, 7, 29, 5, 56, 57, 393, DateTimeKind.Local).AddTicks(7806), "<p><strong>SQL </strong>stands for Structured Query Language. SQL is a standard language designed for managing data in a relational database management system.&nbsp;</p>", "SQL" },
                    { 5, new DateTime(2021, 7, 29, 5, 56, 57, 393, DateTimeKind.Local).AddTicks(7808), "Blockchain is a system of recording information in a way that makes it difficult or impossible to change, hack, or cheat the system.", "Blockchain" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.RenameColumn(
                name: "RepresentImageUrl",
                table: "Article",
                newName: "representImageUrl");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Article",
                newName: "Category");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Article",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
