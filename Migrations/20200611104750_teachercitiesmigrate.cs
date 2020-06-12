using Microsoft.EntityFrameworkCore.Migrations;

namespace TopProjectITI_int40.Migrations
{
    public partial class teachercitiesmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CityId",
                table: "Teachers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Cities_CityId",
                table: "Teachers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Cities_CityId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_CityId",
                table: "Teachers");
        }
    }
}
