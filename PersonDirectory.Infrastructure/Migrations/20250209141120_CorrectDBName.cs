using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectDBName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.Persons",
                table: "dbo.Persons");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "dbo.Persons",
                newName: "Persons",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                schema: "dbo",
                table: "Persons",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                schema: "dbo",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                schema: "dbo",
                newName: "dbo.Persons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.Persons",
                table: "dbo.Persons",
                column: "Id");
        }
    }
}
