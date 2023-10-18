using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
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
                name: "BusinessUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnit", x => x.Id);
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
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_BusinessUnit_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_Country_CountryId",
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
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientGroupCc",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    DistributionInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientGroupCc", x => new { x.DistributionInformationId, x.RecipientId });
                    table.ForeignKey(
                        name: "FK_RecipientGroupCc_DistributionInformation_DistributionInformationId",
                        column: x => x.DistributionInformationId,
                        principalTable: "DistributionInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientGroupCc_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientGroupTo",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    DistributionInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientGroupTo", x => new { x.DistributionInformationId, x.RecipientId });
                    table.ForeignKey(
                        name: "FK_RecipientGroupTo_DistributionInformation_DistributionInformationId",
                        column: x => x.DistributionInformationId,
                        principalTable: "DistributionInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientGroupTo_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
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
                table: "BusinessUnit",
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

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_PriorityId",
                table: "DistributionInformation",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_RegionId",
                table: "DistributionInformation",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientGroupCc_RecipientId",
                table: "RecipientGroupCc",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientGroupTo_RecipientId",
                table: "RecipientGroupTo",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_AreaId",
                table: "Region",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_BusinessUnitId",
                table: "Region",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId",
                table: "Region",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipientGroupCc");

            migrationBuilder.DropTable(
                name: "RecipientGroupTo");

            migrationBuilder.DropTable(
                name: "DistributionInformation");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "BusinessUnit");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
