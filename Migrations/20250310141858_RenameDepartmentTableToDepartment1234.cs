using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolesGrade.Migrations
{
    /// <inheritdoc />
    public partial class RenameDepartmentTableToDepartment1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee121_DEPARTMENTDEPT1_DepartmentId",
                table: "Employee121");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DEPARTMENTDEPT1",
                table: "DEPARTMENTDEPT1");

            migrationBuilder.RenameTable(
                name: "DEPARTMENTDEPT1",
                newName: "DEPARTMENT1234");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DEPARTMENT1234",
                table: "DEPARTMENT1234",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee121_DEPARTMENT1234_DepartmentId",
                table: "Employee121",
                column: "DepartmentId",
                principalTable: "DEPARTMENT1234",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee121_DEPARTMENT1234_DepartmentId",
                table: "Employee121");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DEPARTMENT1234",
                table: "DEPARTMENT1234");

            migrationBuilder.RenameTable(
                name: "DEPARTMENT1234",
                newName: "DEPARTMENTDEPT1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DEPARTMENTDEPT1",
                table: "DEPARTMENTDEPT1",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee121_DEPARTMENTDEPT1_DepartmentId",
                table: "Employee121",
                column: "DepartmentId",
                principalTable: "DEPARTMENTDEPT1",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
