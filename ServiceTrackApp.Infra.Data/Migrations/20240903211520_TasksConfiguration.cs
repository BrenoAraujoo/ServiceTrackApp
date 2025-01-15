using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TasksConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tasks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "tasks");
        }
    }
}
