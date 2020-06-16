using Microsoft.EntityFrameworkCore.Migrations;

namespace TopProjectITI_int40.Migrations
{
    public partial class eductionalcenter1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GovernmentId",
                table: "EductionalCenters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GovernmentId",
                table: "EductionalCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
