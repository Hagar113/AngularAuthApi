using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class h : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "DaysOfWeek",
                columns: table => new
                {
                    DayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeek", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    isEnabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    isEnabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    isEnabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timetable",
                columns: table => new
                {
                    TimetableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetable", x => x.TimetableId);
                    table.ForeignKey(
                        name: "FK_Timetable_DaysOfWeek_DayId",
                        column: x => x.DayId,
                        principalTable: "DaysOfWeek",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rolepage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolepage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rolepage_pages_PageId",
                        column: x => x.PageId,
                        principalTable: "pages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolepage_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    schoolYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    isEnabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_roleId",
                        column: x => x.roleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    TimetableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Timetable_TimetableId",
                        column: x => x.TimetableId,
                        principalTable: "Timetable",
                        principalColumn: "TimetableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    classId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    isEnabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_Classes_classId",
                        column: x => x.classId,
                        principalTable: "Classes",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK_students_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modifiedBy = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true),
                    isEnabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teachers_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_teachers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_ClassId",
                table: "ClassSchedules",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_TimetableId",
                table: "ClassSchedules",
                column: "TimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_rolepage_PageId",
                table: "rolepage",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_rolepage_RoleId",
                table: "rolepage",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_students_classId",
                table: "students",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "IX_students_UserId",
                table: "students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_SubjectId",
                table: "teachers",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_UserId",
                table: "teachers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_DayId",
                table: "Timetable",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_phone",
                table: "users",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_roleId",
                table: "users",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassSchedules");

            migrationBuilder.DropTable(
                name: "rolepage");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "Timetable");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "DaysOfWeek");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
