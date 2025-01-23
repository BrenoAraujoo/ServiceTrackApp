using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class customer_rename_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SmartPhoneNumber_Value",
                table: "customer",
                newName: "SmartPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email_Value",
                table: "customer",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "customer",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address_State",
                table: "customer",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "customer",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "customer",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "customer",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "customer",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "customer",
                newName: "Address_State");

            migrationBuilder.RenameColumn(
                name: "SmartPhoneNumber",
                table: "customer",
                newName: "SmartPhoneNumber_Value");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "customer",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "customer",
                newName: "Email_Value");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "customer",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "customer",
                newName: "Address_City");
        }
    }
}
