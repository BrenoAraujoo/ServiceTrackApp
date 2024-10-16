using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackHub.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class user_refresh_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenHash",
                table: "users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenHash",
                table: "users");
        }
    }
}
