using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipientsGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistributionInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientsGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AreaBuisnessUnit",
                columns: table => new
                {
                    AreasId = table.Column<int>(type: "int", nullable: false),
                    BuisnessUnitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaBuisnessUnit", x => new { x.AreasId, x.BuisnessUnitsId });
                    table.ForeignKey(
                        name: "FK_AreaBuisnessUnit_Area_AreasId",
                        column: x => x.AreasId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaBuisnessUnit_BuisnessUnit_BuisnessUnitsId",
                        column: x => x.BuisnessUnitsId,
                        principalTable: "BuisnessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessUnitCountry",
                columns: table => new
                {
                    BuisnessUnitsId = table.Column<int>(type: "int", nullable: false),
                    CountriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessUnitCountry", x => new { x.BuisnessUnitsId, x.CountriesId });
                    table.ForeignKey(
                        name: "FK_BuisnessUnitCountry_BuisnessUnit_BuisnessUnitsId",
                        column: x => x.BuisnessUnitsId,
                        principalTable: "BuisnessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessUnitCountry_Country_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributionInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    BuisnessUnitId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    RecipientsGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_BuisnessUnit_BuisnessUnitId",
                        column: x => x.BuisnessUnitId,
                        principalTable: "BuisnessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_RecipientsGroup_RecipientsGroupId",
                        column: x => x.RecipientsGroupId,
                        principalTable: "RecipientsGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipientsGroupCc",
                columns: table => new
                {
                    RecipientsCcId = table.Column<int>(type: "int", nullable: false),
                    RecipientsGroupsCcId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientsGroupCc", x => new { x.RecipientsCcId, x.RecipientsGroupsCcId });
                    table.ForeignKey(
                        name: "FK_RecipientsGroupCc_Recipient_RecipientsCcId",
                        column: x => x.RecipientsCcId,
                        principalTable: "Recipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientsGroupCc_RecipientsGroup_RecipientsGroupsCcId",
                        column: x => x.RecipientsGroupsCcId,
                        principalTable: "RecipientsGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientsGroupTo",
                columns: table => new
                {
                    RecipientsGroupsToId = table.Column<int>(type: "int", nullable: false),
                    RecipientsToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientsGroupTo", x => new { x.RecipientsGroupsToId, x.RecipientsToId });
                    table.ForeignKey(
                        name: "FK_RecipientsGroupTo_Recipient_RecipientsToId",
                        column: x => x.RecipientsToId,
                        principalTable: "Recipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientsGroupTo_RecipientsGroup_RecipientsGroupsToId",
                        column: x => x.RecipientsGroupsToId,
                        principalTable: "RecipientsGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaBuisnessUnit_BuisnessUnitsId",
                table: "AreaBuisnessUnit",
                column: "BuisnessUnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessUnitCountry_CountriesId",
                table: "BuisnessUnitCountry",
                column: "CountriesId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_AreaId",
                table: "DistributionInformation",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_BuisnessUnitId",
                table: "DistributionInformation",
                column: "BuisnessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_CountryId",
                table: "DistributionInformation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_PriorityId",
                table: "DistributionInformation",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_RecipientsGroupId",
                table: "DistributionInformation",
                column: "RecipientsGroupId",
                unique: true,
                filter: "[RecipientsGroupId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsGroupCc_RecipientsGroupsCcId",
                table: "RecipientsGroupCc",
                column: "RecipientsGroupsCcId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsGroupTo_RecipientsToId",
                table: "RecipientsGroupTo",
                column: "RecipientsToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaBuisnessUnit");

            migrationBuilder.DropTable(
                name: "BuisnessUnitCountry");

            migrationBuilder.DropTable(
                name: "DistributionInformation");

            migrationBuilder.DropTable(
                name: "RecipientsGroupCc");

            migrationBuilder.DropTable(
                name: "RecipientsGroupTo");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "BuisnessUnit");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "RecipientsGroup");
        }
    }
}
