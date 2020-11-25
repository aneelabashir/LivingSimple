using System;
using System.Collections.Generic;
using LivingSimple.Model;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivingSimple.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    postPreviewContent = table.Column<string>(nullable: true),
                    postContent = table.Column<string>(nullable: true),
                    numberOfComments = table.Column<int>(nullable: false),
                    imgURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                });

            List<Post> posts = new List<Post>() {
                

            };
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
