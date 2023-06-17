using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCountryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortCode",
                table: "Country",
                newName: "CountryCode");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Country",
                newName: "CountryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryName",
                table: "Country",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Country",
                newName: "ShortCode");
        }
    }
}
