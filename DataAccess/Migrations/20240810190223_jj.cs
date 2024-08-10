using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class jj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectId",
                table: "ClassSchedules");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "ClassSchedules",
                newName: "SubjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchedules_SubjectId",
                table: "ClassSchedules",
                newName: "IX_ClassSchedules_SubjectsId");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Timetable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_SubjectId",
                table: "Timetable",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectsId",
                table: "ClassSchedules",
                column: "SubjectsId",
                principalTable: "subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timetable_subjects_SubjectId",
                table: "Timetable",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectsId",
                table: "ClassSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Timetable_subjects_SubjectId",
                table: "Timetable");

            migrationBuilder.DropIndex(
                name: "IX_Timetable_SubjectId",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Timetable");

            migrationBuilder.RenameColumn(
                name: "SubjectsId",
                table: "ClassSchedules",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSchedules_SubjectsId",
                table: "ClassSchedules",
                newName: "IX_ClassSchedules_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id");
        }
    }
}
