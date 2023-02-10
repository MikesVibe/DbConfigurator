using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NullableLocationOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionInformation_LocationOption_LocationOptionId",
                table: "DistributionInformation");

            migrationBuilder.AlterColumn<int>(
                name: "LocationOptionId",
                table: "DistributionInformation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionInformation_LocationOption_LocationOptionId",
                table: "DistributionInformation",
                column: "LocationOptionId",
                principalTable: "LocationOption",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionInformation_LocationOption_LocationOptionId",
                table: "DistributionInformation");

            migrationBuilder.AlterColumn<int>(
                name: "LocationOptionId",
                table: "DistributionInformation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionInformation_LocationOption_LocationOptionId",
                table: "DistributionInformation",
                column: "LocationOptionId",
                principalTable: "LocationOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
