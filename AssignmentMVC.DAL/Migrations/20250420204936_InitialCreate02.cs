using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentMVC.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "createdOn",
                table: "Departments",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Departments",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiBy",
                table: "Departments",
                newName: "LastModifiedBy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Departments",
                newName: "createdOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Departments",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Departments",
                newName: "LastModifiBy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdOn",
                table: "Departments",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
