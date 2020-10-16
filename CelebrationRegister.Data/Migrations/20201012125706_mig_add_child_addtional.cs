using Microsoft.EntityFrameworkCore.Migrations;

namespace CelebrationRegister.Data.Migrations
{
    public partial class mig_add_child_addtional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalOptions_Children_ChildId",
                table: "AdditionalOptions");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalOptions_ChildId",
                table: "AdditionalOptions");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "AdditionalOptions");

            migrationBuilder.CreateTable(
                name: "ChildAdditionalOptions",
                columns: table => new
                {
                    CH_AOId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildId = table.Column<int>(nullable: false),
                    OptionId = table.Column<int>(nullable: false),
                    AdditionalOptionsOptionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildAdditionalOptions", x => x.CH_AOId);
                    table.ForeignKey(
                        name: "FK_ChildAdditionalOptions_AdditionalOptions_AdditionalOptionsOptionId",
                        column: x => x.AdditionalOptionsOptionId,
                        principalTable: "AdditionalOptions",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildAdditionalOptions_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "ChildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildAdditionalOptions_AdditionalOptionsOptionId",
                table: "ChildAdditionalOptions",
                column: "AdditionalOptionsOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildAdditionalOptions_ChildId",
                table: "ChildAdditionalOptions",
                column: "ChildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildAdditionalOptions");

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "AdditionalOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalOptions_ChildId",
                table: "AdditionalOptions",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalOptions_Children_ChildId",
                table: "AdditionalOptions",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "ChildId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
