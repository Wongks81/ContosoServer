using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoServer.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblStudent_tblCourse_CourseId",
                table: "tblStudent");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "tblStudent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tblStudent_tblCourse_CourseId",
                table: "tblStudent",
                column: "CourseId",
                principalTable: "tblCourse",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblStudent_tblCourse_CourseId",
                table: "tblStudent");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "tblStudent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblStudent_tblCourse_CourseId",
                table: "tblStudent",
                column: "CourseId",
                principalTable: "tblCourse",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
