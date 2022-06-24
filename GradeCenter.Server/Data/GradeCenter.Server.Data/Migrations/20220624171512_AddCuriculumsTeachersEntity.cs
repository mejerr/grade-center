using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeCenter.Server.Data.Migrations
{
    public partial class AddCuriculumsTeachersEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurriculumsTeachers",
                columns: table => new
                {
                    CurriculumId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumsTeachers", x => new { x.CurriculumId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_CurriculumsTeachers_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurriculumsTeachers_Curriculums_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumsTeachers_IsDeleted",
                table: "CurriculumsTeachers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumsTeachers_TeacherId",
                table: "CurriculumsTeachers",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculumsTeachers");
        }
    }
}
