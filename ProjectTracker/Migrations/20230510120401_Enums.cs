using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTracker.Migrations
{
    public partial class Enums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Project_ProjectId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonProjectItem_Person_PersonListId",
                table: "PersonProjectItem");

            migrationBuilder.DropIndex(
                name: "IX_Person_ProjectId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "PersonListId",
                table: "PersonProjectItem",
                newName: "PersonsId");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeToComplete",
                table: "ProjectItem",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Importance",
                table: "ProjectItem",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectItem",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Completed",
                table: "ProjectItem",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Person",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Person",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Person",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "PersonProject",
                columns: table => new
                {
                    PersonsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonProject", x => new { x.PersonsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_PersonProject_Person_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonProject_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProject_ProjectsId",
                table: "PersonProject",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonProjectItem_Person_PersonsId",
                table: "PersonProjectItem",
                column: "PersonsId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonProjectItem_Person_PersonsId",
                table: "PersonProjectItem");

            migrationBuilder.DropTable(
                name: "PersonProject");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "PersonsId",
                table: "PersonProjectItem",
                newName: "PersonListId");

            migrationBuilder.AlterColumn<int>(
                name: "TimeToComplete",
                table: "ProjectItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Importance",
                table: "ProjectItem",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectItem",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Completed",
                table: "ProjectItem",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Person",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Person",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_ProjectId",
                table: "Person",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Project_ProjectId",
                table: "Person",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonProjectItem_Person_PersonListId",
                table: "PersonProjectItem",
                column: "PersonListId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
