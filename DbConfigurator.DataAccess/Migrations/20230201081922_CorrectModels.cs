using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CorrectModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionInformations_BuisnessUnits_BuisnessUnitId",
                table: "DistributionInformations");

            migrationBuilder.AlterColumn<int>(
                name: "BuisnessUnitId",
                table: "DistributionInformations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionInformations_BuisnessUnits_BuisnessUnitId",
                table: "DistributionInformations",
                column: "BuisnessUnitId",
                principalTable: "BuisnessUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionInformations_BuisnessUnits_BuisnessUnitId",
                table: "DistributionInformations");

            migrationBuilder.AlterColumn<int>(
                name: "BuisnessUnitId",
                table: "DistributionInformations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionInformations_BuisnessUnits_BuisnessUnitId",
                table: "DistributionInformations",
                column: "BuisnessUnitId",
                principalTable: "BuisnessUnits",
                principalColumn: "Id");
        }
    }
}
