using Microsoft.EntityFrameworkCore.Migrations;

namespace TopProjectITI_int40.Migrations
{
    public partial class studentgroupIsJoined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsJoined",
                table: "StudentsGroups",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsJoined",
                table: "StudentsGroups");
        }
    }
}
