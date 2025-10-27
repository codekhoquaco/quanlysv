using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quanlysv.Migrations
{
    /// <inheritdoc />
    public partial class AddClassNameToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_roles_RoleID",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_accounts_students_StudentID",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_accounts_teachers_TeacherID",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_classes_faculties_FacultyID",
                table: "classes");

            migrationBuilder.DropForeignKey(
                name: "FK_classes_faculties_FacultyID1",
                table: "classes");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_semesters_SemesterID",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_students_StudentID",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_students_StudentID1",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_subjects_SubjectID",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_subjects_SubjectID1",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_grades_enrollments_EnrollmentID",
                table: "grades");

            migrationBuilder.DropForeignKey(
                name: "FK_grades_enrollments_EnrollmentID1",
                table: "grades");

            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassID1",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_subjects_faculties_FacultyID",
                table: "subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_faculties_FacultyID",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_FacultyID",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_subjects_FacultyID",
                table: "subjects");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassID",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassID1",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_grades_EnrollmentID",
                table: "grades");

            migrationBuilder.DropIndex(
                name: "IX_grades_EnrollmentID1",
                table: "grades");

            migrationBuilder.DropIndex(
                name: "IX_enrollments_SemesterID",
                table: "enrollments");

            migrationBuilder.DropIndex(
                name: "IX_enrollments_StudentID",
                table: "enrollments");

            migrationBuilder.DropIndex(
                name: "IX_enrollments_StudentID1",
                table: "enrollments");

            migrationBuilder.DropIndex(
                name: "IX_enrollments_SubjectID",
                table: "enrollments");

            migrationBuilder.DropIndex(
                name: "IX_enrollments_SubjectID1",
                table: "enrollments");

            migrationBuilder.DropIndex(
                name: "IX_classes_FacultyID",
                table: "classes");

            migrationBuilder.DropIndex(
                name: "IX_classes_FacultyID1",
                table: "classes");

            migrationBuilder.DropIndex(
                name: "IX_accounts_RoleID",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_StudentID",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_TeacherID",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "ClassID1",
                table: "students");

            migrationBuilder.DropColumn(
                name: "EnrollmentID1",
                table: "grades");

            migrationBuilder.DropColumn(
                name: "StudentID1",
                table: "enrollments");

            migrationBuilder.DropColumn(
                name: "SubjectID1",
                table: "enrollments");

            migrationBuilder.DropColumn(
                name: "FacultyID1",
                table: "classes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "teachers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "students",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "students",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "GradeLetter",
                table: "grades",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "faculties",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "accounts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "teachers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "students",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ClassID1",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GradeLetter",
                table: "grades",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentID1",
                table: "grades",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "faculties",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StudentID1",
                table: "enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectID1",
                table: "enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyID1",
                table: "classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_FacultyID",
                table: "teachers",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_FacultyID",
                table: "subjects",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassID",
                table: "students",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassID1",
                table: "students",
                column: "ClassID1");

            migrationBuilder.CreateIndex(
                name: "IX_grades_EnrollmentID",
                table: "grades",
                column: "EnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_grades_EnrollmentID1",
                table: "grades",
                column: "EnrollmentID1");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_SemesterID",
                table: "enrollments",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_StudentID",
                table: "enrollments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_StudentID1",
                table: "enrollments",
                column: "StudentID1");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_SubjectID",
                table: "enrollments",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_SubjectID1",
                table: "enrollments",
                column: "SubjectID1");

            migrationBuilder.CreateIndex(
                name: "IX_classes_FacultyID",
                table: "classes",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_classes_FacultyID1",
                table: "classes",
                column: "FacultyID1");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_RoleID",
                table: "accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_StudentID",
                table: "accounts",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_TeacherID",
                table: "accounts",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_roles_RoleID",
                table: "accounts",
                column: "RoleID",
                principalTable: "roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_students_StudentID",
                table: "accounts",
                column: "StudentID",
                principalTable: "students",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_teachers_TeacherID",
                table: "accounts",
                column: "TeacherID",
                principalTable: "teachers",
                principalColumn: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_classes_faculties_FacultyID",
                table: "classes",
                column: "FacultyID",
                principalTable: "faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_classes_faculties_FacultyID1",
                table: "classes",
                column: "FacultyID1",
                principalTable: "faculties",
                principalColumn: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_semesters_SemesterID",
                table: "enrollments",
                column: "SemesterID",
                principalTable: "semesters",
                principalColumn: "SemesterID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_students_StudentID",
                table: "enrollments",
                column: "StudentID",
                principalTable: "students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_students_StudentID1",
                table: "enrollments",
                column: "StudentID1",
                principalTable: "students",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_subjects_SubjectID",
                table: "enrollments",
                column: "SubjectID",
                principalTable: "subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_subjects_SubjectID1",
                table: "enrollments",
                column: "SubjectID1",
                principalTable: "subjects",
                principalColumn: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_grades_enrollments_EnrollmentID",
                table: "grades",
                column: "EnrollmentID",
                principalTable: "enrollments",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_grades_enrollments_EnrollmentID1",
                table: "grades",
                column: "EnrollmentID1",
                principalTable: "enrollments",
                principalColumn: "EnrollmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students",
                column: "ClassID",
                principalTable: "classes",
                principalColumn: "ClassID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassID1",
                table: "students",
                column: "ClassID1",
                principalTable: "classes",
                principalColumn: "ClassID");

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_faculties_FacultyID",
                table: "subjects",
                column: "FacultyID",
                principalTable: "faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_faculties_FacultyID",
                table: "teachers",
                column: "FacultyID",
                principalTable: "faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
