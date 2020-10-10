using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CelebrationRegister.Data.Migrations
{
    public partial class mig_optional_detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionalDetailId",
                table: "ReportCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "FirstLogin",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Children",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DetailTitles",
                columns: table => new
                {
                    DetailTitleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailTitles", x => x.DetailTitleId);
                });

            migrationBuilder.CreateTable(
                name: "OptionalDetails",
                columns: table => new
                {
                    ODetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(nullable: false),
                    ChildId = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    DetailTitleId = table.Column<int>(nullable: false),
                    ReportCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionalDetails", x => x.ODetailsId);
                    table.ForeignKey(
                        name: "FK_OptionalDetails_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "ChildId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionalDetails_DetailTitles_DetailTitleId",
                        column: x => x.DetailTitleId,
                        principalTable: "DetailTitles",
                        principalColumn: "DetailTitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OptionalDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionalDetails_ReportCards_ReportCardId",
                        column: x => x.ReportCardId,
                        principalTable: "ReportCards",
                        principalColumn: "ReportCardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionalDetails_ChildId",
                table: "OptionalDetails",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalDetails_DetailTitleId",
                table: "OptionalDetails",
                column: "DetailTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalDetails_EmployeeId",
                table: "OptionalDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalDetails_ReportCardId",
                table: "OptionalDetails",
                column: "ReportCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionalDetails");

            migrationBuilder.DropTable(
                name: "DetailTitles");

            migrationBuilder.DropColumn(
                name: "OptionalDetailId",
                table: "ReportCards");

            migrationBuilder.DropColumn(
                name: "FirstLogin",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Children");
        }
    }
}
