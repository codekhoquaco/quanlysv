using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quanlysv.Migrations
{
    /// <inheritdoc />
    public partial class Init_NoForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Students_StudentID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Teachers_TeacherID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Faculties_FacultyID",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Semesters_SemesterID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Subjects_SubjectID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Faculties_FacultyID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Faculties_FacultyID",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grades",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classes",
                table: "Classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "teachers");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "subjects");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "students");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "semesters");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "Grades",
                newName: "grades");

            migrationBuilder.RenameTable(
                name: "Faculties",
                newName: "faculties");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "enrollments");

            migrationBuilder.RenameTable(
                name: "Classes",
                newName: "classes");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_FacultyID",
                table: "teachers",
                newName: "IX_teachers_FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_FacultyID",
                table: "subjects",
                newName: "IX_subjects_FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassID",
                table: "students",
                newName: "IX_students_ClassID");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_EnrollmentID",
                table: "grades",
                newName: "IX_grades_EnrollmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_SubjectID",
                table: "enrollments",
                newName: "IX_enrollments_SubjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentID",
                table: "enrollments",
                newName: "IX_enrollments_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_SemesterID",
                table: "enrollments",
                newName: "IX_enrollments_SemesterID");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_FacultyID",
                table: "classes",
                newName: "IX_classes_FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_TeacherID",
                table: "accounts",
                newName: "IX_accounts_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_StudentID",
                table: "accounts",
                newName: "IX_accounts_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_RoleID",
                table: "accounts",
                newName: "IX_accounts_RoleID");

            migrationBuilder.AddColumn<int>(
                name: "ClassID1",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentID1",
                table: "grades",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers",
                table: "teachers",
                column: "TeacherID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjects",
                table: "subjects",
                column: "SubjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_semesters",
                table: "semesters",
                column: "SemesterID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_grades",
                table: "grades",
                column: "GradeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_faculties",
                table: "faculties",
                column: "FacultyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments",
                column: "EnrollmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_classes",
                table: "classes",
                column: "ClassID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassID1",
                table: "students",
                column: "ClassID1");

            migrationBuilder.CreateIndex(
                name: "IX_grades_EnrollmentID1",
                table: "grades",
                column: "EnrollmentID1");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_StudentID1",
                table: "enrollments",
                column: "StudentID1");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_SubjectID1",
                table: "enrollments",
                column: "SubjectID1");

            migrationBuilder.CreateIndex(
                name: "IX_classes_FacultyID1",
                table: "classes",
                column: "FacultyID1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers",
                table: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subjects",
                table: "subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassID1",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_semesters",
                table: "semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grades",
                table: "grades");

            migrationBuilder.DropIndex(
                name: "IX_grades_EnrollmentID1",
                table: "grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_faculties",
                table: "faculties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments");

            migrationBuilder.DropIndex(
                name: "IX_enrollments_StudentID1",
                table: "enrollments");

            migrationBuilder.DropIndex(
                name: "IX_enrollments_SubjectID1",
                table: "enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_classes",
                table: "classes");

            migrationBuilder.DropIndex(
                name: "IX_classes_FacultyID1",
                table: "classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
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

            migrationBuilder.RenameTable(
                name: "teachers",
                newName: "Teachers");

            migrationBuilder.RenameTable(
                name: "subjects",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "students",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "semesters",
                newName: "Semesters");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "grades",
                newName: "Grades");

            migrationBuilder.RenameTable(
                name: "faculties",
                newName: "Faculties");

            migrationBuilder.RenameTable(
                name: "enrollments",
                newName: "Enrollments");

            migrationBuilder.RenameTable(
                name: "classes",
                newName: "Classes");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_teachers_FacultyID",
                table: "Teachers",
                newName: "IX_Teachers_FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_subjects_FacultyID",
                table: "Subjects",
                newName: "IX_Subjects_FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_students_ClassID",
                table: "Students",
                newName: "IX_Students_ClassID");

            migrationBuilder.RenameIndex(
                name: "IX_grades_EnrollmentID",
                table: "Grades",
                newName: "IX_Grades_EnrollmentID");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_SubjectID",
                table: "Enrollments",
                newName: "IX_Enrollments_SubjectID");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_StudentID",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_SemesterID",
                table: "Enrollments",
                newName: "IX_Enrollments_SemesterID");

            migrationBuilder.RenameIndex(
                name: "IX_classes_FacultyID",
                table: "Classes",
                newName: "IX_Classes_FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_TeacherID",
                table: "Accounts",
                newName: "IX_Accounts_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_StudentID",
                table: "Accounts",
                newName: "IX_Accounts_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_RoleID",
                table: "Accounts",
                newName: "IX_Accounts_RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "TeacherID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "SubjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "SemesterID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grades",
                table: "Grades",
                column: "GradeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "FacultyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "EnrollmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classes",
                table: "Classes",
                column: "ClassID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Students_StudentID",
                table: "Accounts",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Teachers_TeacherID",
                table: "Accounts",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Faculties_FacultyID",
                table: "Classes",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Semesters_SemesterID",
                table: "Enrollments",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "SemesterID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Subjects_SubjectID",
                table: "Enrollments",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentID",
                table: "Grades",
                column: "EnrollmentID",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassID",
                table: "Students",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "ClassID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Faculties_FacultyID",
                table: "Subjects",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Faculties_FacultyID",
                table: "Teachers",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
