using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackHub.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskTypeDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_task_type_TaskTypeId",
                table: "tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_task_type_TaskTypeId",
                table: "tasks",
                column: "TaskTypeId",
                principalTable: "task_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_task_type_TaskTypeId",
                table: "tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_task_type_TaskTypeId",
                table: "tasks",
                column: "TaskTypeId",
                principalTable: "task_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
