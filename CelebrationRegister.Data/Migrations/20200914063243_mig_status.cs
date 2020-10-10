using Microsoft.EntityFrameworkCore.Migrations;

namespace CelebrationRegister.Data.Migrations
{
    public partial class mig_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ReportCards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "ReportCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportCards_StatusId",
                table: "ReportCards",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportCards_Statuses_StatusId",
                table: "ReportCards",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportCards_Statuses_StatusId",
                table: "ReportCards");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_ReportCards_StatusId",
                table: "ReportCards");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ReportCards");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "ReportCards");
        }
    }
}
