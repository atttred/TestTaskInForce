using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Server.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutContent_AspNetUsers_ApplicationUserId",
                table: "AboutContent");

            migrationBuilder.DropIndex(
                name: "IX_AboutContent_ApplicationUserId",
                table: "AboutContent");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AboutContent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "AboutContent",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AboutContent_ApplicationUserId",
                table: "AboutContent",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutContent_AspNetUsers_ApplicationUserId",
                table: "AboutContent",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
