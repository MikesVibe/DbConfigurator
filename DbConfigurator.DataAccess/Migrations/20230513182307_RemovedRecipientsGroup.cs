using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRecipientsGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionInformation_RecipientGroup_RecipientGroupId",
                table: "DistributionInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipientGroupCc_RecipientGroup_RecipientGroupId",
                table: "RecipientGroupCc");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipientGroupTo_RecipientGroup_RecipientGroupId",
                table: "RecipientGroupTo");

            migrationBuilder.DropTable(
                name: "RecipientGroup");

            migrationBuilder.DropIndex(
                name: "IX_DistributionInformation_RecipientGroupId",
                table: "DistributionInformation");

            migrationBuilder.DropColumn(
                name: "RecipientGroupId",
                table: "DistributionInformation");

            migrationBuilder.RenameColumn(
                name: "RecipientGroupId",
                table: "RecipientGroupTo",
                newName: "DistributionInformationId");

            migrationBuilder.RenameColumn(
                name: "RecipientGroupId",
                table: "RecipientGroupCc",
                newName: "DistributionInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipientGroupCc_DistributionInformation_DistributionInformationId",
                table: "RecipientGroupCc",
                column: "DistributionInformationId",
                principalTable: "DistributionInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipientGroupTo_DistributionInformation_DistributionInformationId",
                table: "RecipientGroupTo",
                column: "DistributionInformationId",
                principalTable: "DistributionInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipientGroupCc_DistributionInformation_DistributionInformationId",
                table: "RecipientGroupCc");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipientGroupTo_DistributionInformation_DistributionInformationId",
                table: "RecipientGroupTo");

            migrationBuilder.RenameColumn(
                name: "DistributionInformationId",
                table: "RecipientGroupTo",
                newName: "RecipientGroupId");

            migrationBuilder.RenameColumn(
                name: "DistributionInformationId",
                table: "RecipientGroupCc",
                newName: "RecipientGroupId");

            migrationBuilder.AddColumn<int>(
                name: "RecipientGroupId",
                table: "DistributionInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipientGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributionInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_RecipientGroupId",
                table: "DistributionInformation",
                column: "RecipientGroupId",
                unique: true,
                filter: "[RecipientGroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionInformation_RecipientGroup_RecipientGroupId",
                table: "DistributionInformation",
                column: "RecipientGroupId",
                principalTable: "RecipientGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipientGroupCc_RecipientGroup_RecipientGroupId",
                table: "RecipientGroupCc",
                column: "RecipientGroupId",
                principalTable: "RecipientGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipientGroupTo_RecipientGroup_RecipientGroupId",
                table: "RecipientGroupTo",
                column: "RecipientGroupId",
                principalTable: "RecipientGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
