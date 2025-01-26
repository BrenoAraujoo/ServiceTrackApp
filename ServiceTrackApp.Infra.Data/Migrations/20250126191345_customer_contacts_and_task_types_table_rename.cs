using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceTrackApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class customer_contacts_and_task_types_table_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_type_users_CreatorId",
                table: "task_type");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_customer_CustomerId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_task_type_TaskTypeId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_task_type",
                table: "task_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer",
                table: "customer");

            migrationBuilder.RenameTable(
                name: "task_type",
                newName: "task_types");

            migrationBuilder.RenameTable(
                name: "customer",
                newName: "customers");

            migrationBuilder.RenameIndex(
                name: "IX_task_type_CreatorId",
                table: "task_types",
                newName: "IX_task_types_CreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "SmartPhoneNumber",
                table: "customers",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "customers",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_task_types",
                table: "task_types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    JobPosition = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SmartPhoneNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contacts_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacts_CustomerId",
                table: "contacts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_task_types_users_CreatorId",
                table: "task_types",
                column: "CreatorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_customers_CustomerId",
                table: "tasks",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_task_types_TaskTypeId",
                table: "tasks",
                column: "TaskTypeId",
                principalTable: "task_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_types_users_CreatorId",
                table: "task_types");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_customers_CustomerId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_task_types_TaskTypeId",
                table: "tasks");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_task_types",
                table: "task_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "task_types",
                newName: "task_type");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "customer");

            migrationBuilder.RenameIndex(
                name: "IX_task_types_CreatorId",
                table: "task_type",
                newName: "IX_task_type_CreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "SmartPhoneNumber",
                table: "customer",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11);

            migrationBuilder.AddPrimaryKey(
                name: "PK_task_type",
                table: "task_type",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer",
                table: "customer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_task_type_users_CreatorId",
                table: "task_type",
                column: "CreatorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_customer_CustomerId",
                table: "tasks",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_task_type_TaskTypeId",
                table: "tasks",
                column: "TaskTypeId",
                principalTable: "task_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
