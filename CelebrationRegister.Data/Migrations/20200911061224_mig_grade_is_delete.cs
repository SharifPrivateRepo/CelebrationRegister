using Microsoft.EntityFrameworkCore.Migrations;

namespace CelebrationRegister.Data.Migrations
{
    public partial class mig_grade_is_delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Grades",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Grades");
        }
    }
}
