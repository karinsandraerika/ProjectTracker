using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTracker.Migrations
{
    public partial class PersonProjectItems2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_ProjectItem_ProjectItemId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_ProjectItemId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ProjectItemId",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "PersonProjectItem",
                columns: table => new
                {
                    PersonListId = table.Column<int>(type: "int", nullable: false),
                    ProjectItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonProjectItem", x => new { x.PersonListId, x.ProjectItemsId });
                    table.ForeignKey(
                        name: "FK_PersonProjectItem_Person_PersonListId",
                        column: x => x.PersonListId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonProjectItem_ProjectItem_ProjectItemsId",
                        column: x => x.ProjectItemsId,
                        principalTable: "ProjectItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProjectItem_ProjectItemsId",
                table: "PersonProjectItem",
                column: "ProjectItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonProjectItem");

            migrationBuilder.AddColumn<int>(
                name: "ProjectItemId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_ProjectItemId",
                table: "Person",
                column: "ProjectItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_ProjectItem_ProjectItemId",
                table: "Person",
                column: "ProjectItemId",
                principalTable: "ProjectItem",
                principalColumn: "Id");
        }
    }
}
