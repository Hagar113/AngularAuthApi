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

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_subjects_SubjectId",
                table: "teachers");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "teachers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "ClassSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_subjects_SubjectId",
                table: "teachers",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectId",
                table: "ClassSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_subjects_SubjectId",
                table: "teachers");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "ClassSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_subjects_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_subjects_SubjectId",
                table: "teachers",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
