using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingPosibleNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DistributionInformation_CcRecipientsGroupId",
                table: "DistributionInformation");

            migrationBuilder.DropIndex(
                name: "IX_DistributionInformation_ToRecipientsGroupId",
                table: "DistributionInformation");

            migrationBuilder.AlterColumn<int>(
                name: "ToRecipientsGroupId",
                table: "DistributionInformation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CcRecipientsGroupId",
                table: "DistributionInformation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_CcRecipientsGroupId",
                table: "DistributionInformation",
                column: "CcRecipientsGroupId",
                unique: true,
                filter: "[CcRecipientsGroupId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_ToRecipientsGroupId",
                table: "DistributionInformation",
                column: "ToRecipientsGroupId",
                unique: true,
                filter: "[ToRecipientsGroupId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DistributionInformation_CcRecipientsGroupId",
                table: "DistributionInformation");

            migrationBuilder.DropIndex(
                name: "IX_DistributionInformation_ToRecipientsGroupId",
                table: "DistributionInformation");

            migrationBuilder.AlterColumn<int>(
                name: "ToRecipientsGroupId",
                table: "DistributionInformation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CcRecipientsGroupId",
                table: "DistributionInformation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_CcRecipientsGroupId",
                table: "DistributionInformation",
                column: "CcRecipientsGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_ToRecipientsGroupId",
                table: "DistributionInformation",
                column: "ToRecipientsGroupId",
                unique: true);
        }
    }
}
