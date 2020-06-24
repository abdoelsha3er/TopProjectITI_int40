using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopProjectITI_int40.Migrations
{
    public partial class GenerateReportV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Homework = table.Column<string>(nullable: true),
                    NextLecture = table.Column<string>(nullable: true),
                    reportDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "ReportStudentGroups",
                columns: table => new
                {
                    ReportStudentGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    EductionalCenterGroupId = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false),
                    Attendance = table.Column<bool>(nullable: false),
                    CommunicationSkills = table.Column<string>(nullable: true),
                    NotesExam = table.Column<string>(nullable: true),
                    StatusExam = table.Column<bool>(nullable: false),
                    DegreeExam = table.Column<int>(nullable: false),
                    NotesLastHomework = table.Column<string>(nullable: true),
                    StatusLastHomework = table.Column<bool>(nullable: false),
                    DegreeLastHomework = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStudentGroups", x => x.ReportStudentGroupId);
                    table.ForeignKey(
                        name: "FK_ReportStudentGroups_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportStudentGroups_StudentsGroups_EductionalCenterGroupId_StudentId",
                        columns: x => new { x.EductionalCenterGroupId, x.StudentId },
                        principalTable: "StudentsGroups",
                        principalColumns: new[] { "EductionalCenterGroupId", "StudentId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportStudentGroups_ReportId",
                table: "ReportStudentGroups",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStudentGroups_EductionalCenterGroupId_StudentId",
                table: "ReportStudentGroups",
                columns: new[] { "EductionalCenterGroupId", "StudentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportStudentGroups");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
