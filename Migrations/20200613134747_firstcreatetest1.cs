using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopProjectITI_int40.Migrations
{
    public partial class firstcreatetest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorySubjects",
                columns: table => new
                {
                    CategorySubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySubjects", x => x.CategorySubjectId);
                });

            migrationBuilder.CreateTable(
                name: "EductionalCenters",
                columns: table => new
                {
                    EductionalCenterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    GovernmentId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    AddressDetails = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EductionalCenters", x => x.EductionalCenterId);
                });

            migrationBuilder.CreateTable(
                name: "Governments",
                columns: table => new
                {
                    GovernmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governments", x => x.GovernmentId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CategorySubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_CategorySubjects_CategorySubjectId",
                        column: x => x.CategorySubjectId,
                        principalTable: "CategorySubjects",
                        principalColumn: "CategorySubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EductionalCenterPhones",
                columns: table => new
                {
                    EductionalCenterPhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EductionalCenterPhoneNumber = table.Column<string>(nullable: false),
                    EductionalCenterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EductionalCenterPhones", x => x.EductionalCenterPhoneId);
                    table.ForeignKey(
                        name: "FK_EductionalCenterPhones_EductionalCenters_EductionalCenterId",
                        column: x => x.EductionalCenterId,
                        principalTable: "EductionalCenters",
                        principalColumn: "EductionalCenterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EductionalCenterSoicalLinks",
                columns: table => new
                {
                    EductionalCenterSoicalLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkAddess = table.Column<string>(nullable: false),
                    EductionalCenterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EductionalCenterSoicalLinks", x => x.EductionalCenterSoicalLinkId);
                    table.ForeignKey(
                        name: "FK_EductionalCenterSoicalLinks_EductionalCenters_EductionalCenterId",
                        column: x => x.EductionalCenterId,
                        principalTable: "EductionalCenters",
                        principalColumn: "EductionalCenterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    GovernmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Governments_GovernmentId",
                        column: x => x.GovernmentId,
                        principalTable: "Governments",
                        principalColumn: "GovernmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EductionalCenterSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false),
                    EductionalCenterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EductionalCenterSubjects", x => new { x.SubjectId, x.EductionalCenterId });
                    table.ForeignKey(
                        name: "FK_EductionalCenterSubjects_EductionalCenters_EductionalCenterId",
                        column: x => x.EductionalCenterId,
                        principalTable: "EductionalCenters",
                        principalColumn: "EductionalCenterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EductionalCenterSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    ParentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(maxLength: 11, nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ParentId);
                    table.ForeignKey(
                        name: "FK_Parents_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Picture = table.Column<string>(maxLength: 255, nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    AddressDetails = table.Column<string>(maxLength: 255, nullable: true),
                    Gender = table.Column<bool>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    About = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teachers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEductions",
                columns: table => new
                {
                    TeacherEductionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EductionName = table.Column<string>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEductions", x => x.TeacherEductionId);
                    table.ForeignKey(
                        name: "FK_TeacherEductions_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherExperiences",
                columns: table => new
                {
                    TeacherExperienceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherExperiences", x => x.TeacherExperienceId);
                    table.ForeignKey(
                        name: "FK_TeacherExperiences_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherPhones",
                columns: table => new
                {
                    TeacherPhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherPhoneNumber = table.Column<string>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherPhones", x => x.TeacherPhoneId);
                    table.ForeignKey(
                        name: "FK_TeacherPhones_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSchools",
                columns: table => new
                {
                    TeacherSchoolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolName = table.Column<string>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSchools", x => x.TeacherSchoolId);
                    table.ForeignKey(
                        name: "FK_TeacherSchools_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSocialLinks",
                columns: table => new
                {
                    TeacherSocialLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkAddess = table.Column<string>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSocialLinks", x => x.TeacherSocialLinkId);
                    table.ForeignKey(
                        name: "FK_TeacherSocialLinks_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => new { x.TeacherId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_GovernmentId",
                table: "Cities",
                column: "GovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EductionalCenterPhones_EductionalCenterId",
                table: "EductionalCenterPhones",
                column: "EductionalCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_EductionalCenterPhones_EductionalCenterPhoneNumber",
                table: "EductionalCenterPhones",
                column: "EductionalCenterPhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EductionalCenterSoicalLinks_EductionalCenterId",
                table: "EductionalCenterSoicalLinks",
                column: "EductionalCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_EductionalCenterSubjects_EductionalCenterId",
                table: "EductionalCenterSubjects",
                column: "EductionalCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_CityId",
                table: "Parents",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CategorySubjectId",
                table: "Subjects",
                column: "CategorySubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEductions_TeacherId",
                table: "TeacherEductions",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherExperiences_TeacherId",
                table: "TeacherExperiences",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPhones_TeacherId",
                table: "TeacherPhones",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPhones_TeacherPhoneNumber",
                table: "TeacherPhones",
                column: "TeacherPhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CityId",
                table: "Teachers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSchools_TeacherId",
                table: "TeacherSchools",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSocialLinks_TeacherId",
                table: "TeacherSocialLinks",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EductionalCenterPhones");

            migrationBuilder.DropTable(
                name: "EductionalCenterSoicalLinks");

            migrationBuilder.DropTable(
                name: "EductionalCenterSubjects");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "TeacherEductions");

            migrationBuilder.DropTable(
                name: "TeacherExperiences");

            migrationBuilder.DropTable(
                name: "TeacherPhones");

            migrationBuilder.DropTable(
                name: "TeacherSchools");

            migrationBuilder.DropTable(
                name: "TeacherSocialLinks");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "EductionalCenters");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "CategorySubjects");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Governments");
        }
    }
}
