using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class user_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Role",
                table: "users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");
        }
    }
}
