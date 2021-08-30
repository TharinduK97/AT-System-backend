using Microsoft.EntityFrameworkCore.Migrations;

namespace hp_proj_1_backend.Migrations
{
    public partial class appliedjob6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedJobs_Jobs_JobID",
                table: "AppliedJobs");

            migrationBuilder.DropColumn(
                name: "JobIn",
                table: "AppliedJobs");

            migrationBuilder.AlterColumn<int>(
                name: "JobID",
                table: "AppliedJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedJobs_Jobs_JobID",
                table: "AppliedJobs",
                column: "JobID",
                principalTable: "Jobs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedJobs_Jobs_JobID",
                table: "AppliedJobs");

            migrationBuilder.AlterColumn<int>(
                name: "JobID",
                table: "AppliedJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "JobIn",
                table: "AppliedJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedJobs_Jobs_JobID",
                table: "AppliedJobs",
                column: "JobID",
                principalTable: "Jobs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
