using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semester_SemesterId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SemesterId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "SemesterSubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterSubjects_Semester_Semester_id",
                        column: x => x.Semester_id,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemesterSubjects_Subjects_Subject_id",
                        column: x => x.Subject_id,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Semester_id",
                table: "Students",
                column: "Semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubjects_Semester_id",
                table: "SemesterSubjects",
                column: "Semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubjects_Subject_id",
                table: "SemesterSubjects",
                column: "Subject_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semester_Semester_id",
                table: "Students",
                column: "Semester_id",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semester_Semester_id",
                table: "Students");

            migrationBuilder.DropTable(
                name: "SemesterSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Students_Semester_id",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterId",
                table: "Students",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semester_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
