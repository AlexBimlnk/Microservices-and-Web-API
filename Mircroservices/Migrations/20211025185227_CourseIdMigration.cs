using Microsoft.EntityFrameworkCore.Migrations;

namespace Mircroservices.Migrations
{
    public partial class CourseIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Students");
        }
    }
}
