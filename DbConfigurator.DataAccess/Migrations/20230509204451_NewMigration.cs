using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    BuisnessUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaBuisnessUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaBuisnessUnit_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaBuisnessUnit_BuisnessUnit_BuisnessUnitId",
                        column: x => x.BuisnessUnitId,
                        principalTable: "BuisnessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessUnitCountry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuisnessUnitId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessUnitCountry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessUnitCountry_BuisnessUnit_BuisnessUnitId",
                        column: x => x.BuisnessUnitId,
                        principalTable: "BuisnessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessUnitCountry_Country_CountryId",
                        column: x => x.CountryId,
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

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Americas" },
                    { 2, "Central Europe" },
                    { 3, "Growing Markets" },
                    { 4, "Northern Europe" },
                    { 5, "Southern Europe" },
                    { 99, "ANY" }
                });

            migrationBuilder.InsertData(
                table: "BuisnessUnit",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "NAO" },
                    { 2, "SAM" },
                    { 3, "GER" },
                    { 4, "CEE" },
                    { 5, "MEK" },
                    { 6, "AFR" },
                    { 7, "IND" },
                    { 8, "APAC" },
                    { 9, "BTN" },
                    { 10, "UK&I" },
                    { 11, "ITA" },
                    { 12, "IBE" },
                    { 13, "FRA" },
                    { 99, "ANY" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name", "ShortCode" },
                values: new object[,]
                {
                    { 1, "Canada", "CA" },
                    { 2, "Guatemala", "GT" },
                    { 3, "Mexico", "MX" },
                    { 4, "Puerto Rico", "PR" },
                    { 5, "USA", "US" },
                    { 6, "Argentina", "AR" },
                    { 7, "Brazil", "BR" },
                    { 8, "Chile", "CL" },
                    { 9, "Colombia", "CO" },
                    { 10, "Peru", "PE" },
                    { 11, "Uruguay", "UY" },
                    { 12, "Venezuela", "VE" },
                    { 13, "Germany", "DE" },
                    { 14, "Poland", "PL" },
                    { 15, "Russian Federation", "RU" },
                    { 16, "Austria", "AT" },
                    { 17, "Bulgaria", "BG" },
                    { 18, "Switzerland", "CH" },
                    { 19, "Cyprus", "CY" },
                    { 20, "Czech Republic", "CZ" },
                    { 21, "Greece", "GR" },
                    { 22, "Croatia", "HR" },
                    { 23, "Hungary", "HU" },
                    { 24, "Israel", "IL" },
                    { 25, "Kasakhstan", "KZ" },
                    { 26, "Romania", "RO" },
                    { 27, "Serbia", "RS" },
                    { 28, "Slovakia", "SK" },
                    { 29, "Ukraine", "UA" },
                    { 30, "United Arab Emirates", "AE" },
                    { 31, "Egypt", "EG" },
                    { 32, "Iran", "IR" },
                    { 33, "Lebanon", "LB" },
                    { 34, "Qatar", "QA" },
                    { 35, "Saudi Arabia", "SA" },
                    { 36, "Turkey", "TR" },
                    { 37, "Burkina Faso", "BF" },
                    { 38, "Benin", "BJ" },
                    { 39, "Cote d'Ivoire", "CI" },
                    { 40, "Algeria", "DZ" },
                    { 41, "Gabon", "GA" },
                    { 42, "Ivory Coast", "CI" },
                    { 43, "Morocco", "MA" },
                    { 44, "Madagascar", "MG" },
                    { 45, "Mali", "ML" },
                    { 46, "Mauritius", "MU" },
                    { 47, "Senegal", "SN" },
                    { 48, "Tunisia", "TN" },
                    { 49, "South Africa", "ZA" },
                    { 50, "India", "IN" },
                    { 51, "Australia", "AU" },
                    { 52, "People Rep China", "CN" },
                    { 53, "Hong Kong", "HK" },
                    { 54, "Indonesia", "ID" },
                    { 55, "Japan", "JP" },
                    { 56, "Korea", "KR" },
                    { 57, "Malaysia", "MY" },
                    { 58, "New Zealand", "NZ" },
                    { 59, "Philippines", "PH" },
                    { 60, "Singapore", "SG" },
                    { 61, "Thailand", "TH" },
                    { 62, "Taiwan", "TW" },
                    { 63, "Belgium", "BE" },
                    { 64, "Denmark", "DK" },
                    { 65, "Estonia", "EE" },
                    { 66, "Finland", "FI" },
                    { 67, "Lithuania", "LT" },
                    { 68, "Luxembourg", "LU" },
                    { 69, "Netherlands", "NL" },
                    { 70, "Norway", "NO" },
                    { 71, "Sweden", "SE" },
                    { 72, "United Kingdom", "GB" },
                    { 73, "Ireland", "IE" },
                    { 74, "Italy", "IT" },
                    { 75, "Andorra", "AD" },
                    { 76, "Spain", "ES" },
                    { 77, "Portugal", "PT" },
                    { 78, "France", "FR" },
                    { 79, "Morocco", "MA" },
                    { 80, "New Caledonia", "NC" },
                    { 81, "French Polynesia", "PF" },
                    { 99, "ANY", "ANY" }
                });

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "P1" },
                    { 2, "P2" },
                    { 3, "P3" },
                    { 4, "P4" },
                    { 99, "ANY" }
                });

            migrationBuilder.InsertData(
                table: "AreaBuisnessUnit",
                columns: new[] { "Id", "AreaId", "BuisnessUnitId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 6, 3, 6 },
                    { 7, 3, 7 },
                    { 8, 4, 8 },
                    { 9, 4, 9 },
                    { 10, 4, 10 },
                    { 11, 5, 11 },
                    { 12, 5, 12 },
                    { 13, 5, 13 },
                    { 14, 1, 99 },
                    { 15, 2, 99 },
                    { 16, 3, 99 },
                    { 17, 4, 99 },
                    { 18, 5, 99 },
                    { 19, 99, 99 }
                });

            migrationBuilder.InsertData(
                table: "BuisnessUnitCountry",
                columns: new[] { "Id", "BuisnessUnitId", "CountryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 },
                    { 6, 2, 6 },
                    { 7, 2, 7 },
                    { 8, 2, 8 },
                    { 9, 2, 9 },
                    { 10, 2, 10 },
                    { 11, 2, 11 },
                    { 12, 2, 12 },
                    { 13, 3, 13 },
                    { 14, 4, 14 },
                    { 15, 4, 15 },
                    { 16, 4, 16 },
                    { 17, 4, 17 },
                    { 18, 4, 18 },
                    { 19, 4, 19 },
                    { 20, 4, 20 },
                    { 21, 4, 21 },
                    { 22, 4, 22 },
                    { 23, 4, 23 },
                    { 24, 4, 24 },
                    { 25, 4, 25 },
                    { 26, 4, 26 },
                    { 27, 4, 27 },
                    { 28, 4, 28 },
                    { 29, 4, 29 },
                    { 30, 5, 30 },
                    { 31, 5, 31 },
                    { 32, 5, 32 },
                    { 33, 5, 33 },
                    { 34, 5, 34 },
                    { 35, 5, 35 },
                    { 36, 5, 36 },
                    { 37, 6, 37 },
                    { 38, 6, 38 },
                    { 39, 6, 39 },
                    { 40, 6, 40 },
                    { 41, 6, 41 },
                    { 42, 6, 42 },
                    { 43, 6, 43 },
                    { 44, 6, 44 },
                    { 45, 6, 45 },
                    { 46, 6, 46 },
                    { 47, 6, 47 },
                    { 48, 6, 48 },
                    { 49, 6, 49 },
                    { 50, 7, 50 },
                    { 51, 8, 51 },
                    { 52, 8, 52 },
                    { 53, 8, 53 },
                    { 54, 8, 54 },
                    { 55, 8, 55 },
                    { 56, 8, 56 },
                    { 57, 8, 57 },
                    { 58, 8, 58 },
                    { 59, 8, 59 },
                    { 60, 8, 60 },
                    { 61, 8, 61 },
                    { 62, 8, 62 },
                    { 63, 9, 63 },
                    { 64, 9, 64 },
                    { 65, 9, 65 },
                    { 66, 9, 66 },
                    { 67, 9, 67 },
                    { 68, 9, 68 },
                    { 69, 9, 69 },
                    { 70, 9, 70 },
                    { 71, 9, 71 },
                    { 72, 10, 72 },
                    { 73, 10, 73 },
                    { 74, 11, 74 },
                    { 75, 12, 75 },
                    { 76, 12, 76 },
                    { 77, 12, 77 },
                    { 78, 13, 78 },
                    { 79, 13, 79 },
                    { 80, 13, 80 },
                    { 81, 13, 81 },
                    { 82, 1, 99 },
                    { 83, 2, 99 },
                    { 84, 3, 99 },
                    { 85, 4, 99 },
                    { 86, 5, 99 },
                    { 87, 6, 99 },
                    { 88, 7, 99 },
                    { 89, 8, 99 },
                    { 90, 9, 99 },
                    { 91, 10, 99 },
                    { 92, 11, 99 },
                    { 93, 12, 99 },
                    { 94, 13, 99 },
                    { 95, 99, 99 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaBuisnessUnit_AreaId",
                table: "AreaBuisnessUnit",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaBuisnessUnit_BuisnessUnitId",
                table: "AreaBuisnessUnit",
                column: "BuisnessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessUnitCountry_BuisnessUnitId",
                table: "BuisnessUnitCountry",
                column: "BuisnessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessUnitCountry_CountryId",
                table: "BuisnessUnitCountry",
                column: "CountryId");

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
