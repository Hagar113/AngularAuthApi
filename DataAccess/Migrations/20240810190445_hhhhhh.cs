using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class hhhhhh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectsId",
                table: "ClassSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedules_SubjectsId",
                table: "ClassSchedules");

            migrationBuilder.DropColumn(
                name: "SubjectsId",
                table: "ClassSchedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectsId",
                table: "ClassSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_SubjectsId",
                table: "ClassSchedules",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectsId",
                table: "ClassSchedules",
                column: "SubjectsId",
                principalTable: "subjects",
                principalColumn: "Id");
        }
    }
}
