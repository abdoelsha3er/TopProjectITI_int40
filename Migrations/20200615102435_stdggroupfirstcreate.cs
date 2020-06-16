using Microsoft.EntityFrameworkCore.Migrations;

namespace TopProjectITI_int40.Migrations
{
    public partial class stdggroupfirstcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsGroups",
                columns: table => new
                {
                    EductionalCenterGroupId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsGroups", x => new { x.EductionalCenterGroupId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentsGroups_EductionalCenterGroups_EductionalCenterGroupId",
                        column: x => x.EductionalCenterGroupId,
                        principalTable: "EductionalCenterGroups",
                        principalColumn: "EductionalCenterGroupId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentsGroups_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsGroups_StudentId",
                table: "StudentsGroups",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsGroups");
        }
    }
}
