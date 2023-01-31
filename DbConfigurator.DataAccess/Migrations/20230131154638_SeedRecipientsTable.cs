using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedRecipientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipients",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "John.Doe@company.net", "John", "Doe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipients",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
