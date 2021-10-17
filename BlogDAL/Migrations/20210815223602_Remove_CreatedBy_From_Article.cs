﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDALOld.Migrations
{
    public partial class Remove_CreatedBy_From_Article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Article");
        }
    }
}
