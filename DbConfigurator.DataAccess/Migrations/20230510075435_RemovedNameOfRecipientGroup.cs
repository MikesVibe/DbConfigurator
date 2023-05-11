using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNameOfRecipientGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "RecipientGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RecipientGroup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
