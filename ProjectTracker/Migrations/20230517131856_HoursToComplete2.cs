using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTracker.Migrations
{
    /// <inheritdoc />
    public partial class HoursToComplete2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToComplete",
                table: "ProjectItem");

            migrationBuilder.AddColumn<int>(
                name: "HoursToComplete",
                table: "ProjectItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoursToComplete",
                table: "ProjectItem");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeToComplete",
                table: "ProjectItem",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
