using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDALOld.Migrations
{
    public partial class AddCreatedBy_For_Article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Article",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
