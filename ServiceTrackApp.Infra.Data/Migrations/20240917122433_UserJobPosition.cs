using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserJobPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "users",
                newName: "SmartPhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "JobPosition",
                table: "users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobPosition",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "SmartPhoneNumber",
                table: "users",
                newName: "Phone");
        }
    }
}
