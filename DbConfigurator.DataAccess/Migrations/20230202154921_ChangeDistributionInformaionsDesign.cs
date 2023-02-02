using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDistributionInformaionsDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionInformations_BuisnessUnits_BuisnessUnitId",
                table: "DistributionInformations");

            migrationBuilder.RenameColumn(
                name: "BuisnessUnitId",
                table: "DistributionInformations",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_DistributionInformations_BuisnessUnitId",
                table: "DistributionInformations",
                newName: "IX_DistributionInformations_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionInformations_Countries_CountryId",
                table: "DistributionInformations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionInformations_Countries_CountryId",
                table: "DistributionInformations");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "DistributionInformations",
                newName: "BuisnessUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DistributionInformations_CountryId",
                table: "DistributionInformations",
                newName: "IX_DistributionInformations_BuisnessUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionInformations_BuisnessUnits_BuisnessUnitId",
                table: "DistributionInformations",
                column: "BuisnessUnitId",
                principalTable: "BuisnessUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
