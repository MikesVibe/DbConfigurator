using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDB : Migration
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
                name: "DestinationField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripiton = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationOption", x => x.Id);
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
                name: "BuisnessUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessUnit_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BuisnessUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_BuisnessUnit_BuisnessUnitId",
                        column: x => x.BuisnessUnitId,
                        principalTable: "BuisnessUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributionInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    LocationOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_LocationOption_LocationOptionId",
                        column: x => x.LocationOptionId,
                        principalTable: "LocationOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformation_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientsGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationFieldId = table.Column<int>(type: "int", nullable: false),
                    DistributionInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientsGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipientsGroup_DestinationField_DestinationFieldId",
                        column: x => x.DestinationFieldId,
                        principalTable: "DestinationField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientsGroup_DistributionInformation_DistributionInformationId",
                        column: x => x.DistributionInformationId,
                        principalTable: "DistributionInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientRecipientsGroup",
                columns: table => new
                {
                    RecipientsGroupsId = table.Column<int>(type: "int", nullable: false),
                    RecipientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientRecipientsGroup", x => new { x.RecipientsGroupsId, x.RecipientsId });
                    table.ForeignKey(
                        name: "FK_RecipientRecipientsGroup_Recipient_RecipientsId",
                        column: x => x.RecipientsId,
                        principalTable: "Recipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientRecipientsGroup_RecipientsGroup_RecipientsGroupsId",
                        column: x => x.RecipientsGroupsId,
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
                    { 5, "Southern Europe" }
                });

            migrationBuilder.InsertData(
                table: "DestinationField",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "TO" },
                    { 2, "CC" }
                });

            migrationBuilder.InsertData(
                table: "LocationOption",
                columns: new[] { "Id", "Descripiton", "Name" },
                values: new object[] { 1, "Any Country", "Any Country" });

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "P1" },
                    { 2, "P2" },
                    { 3, "P3" },
                    { 4, "P4" },
                    { 5, "Any" }
                });

            migrationBuilder.InsertData(
                table: "Recipient",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "John.Doe@company.net", "John", "Doe" },
                    { 2, "Josh.Smith@company.net", "Josh", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "BuisnessUnit",
                columns: new[] { "Id", "AreaId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "NAO" },
                    { 2, 1, "SAM" },
                    { 3, 2, "GER" },
                    { 4, 2, "CEE" },
                    { 5, 3, "MEK" },
                    { 6, 3, "AFR" },
                    { 7, 3, "IND" },
                    { 8, 4, "APAC" },
                    { 9, 4, "BTN" },
                    { 10, 4, "UK&I" },
                    { 11, 5, "ITA" },
                    { 12, 5, "IBE" },
                    { 13, 5, "FRA" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "BuisnessUnitId", "Name", "ShortCode" },
                values: new object[,]
                {
                    { 1, 1, "Canada", "CA" },
                    { 2, 1, "Guatemala", "GT" },
                    { 3, 1, "Mexico", "MX" },
                    { 4, 1, "Puerto Rico", "PR" },
                    { 5, 1, "USA", "US" },
                    { 6, 2, "Argentina", "AR" },
                    { 7, 2, "Brazil", "BR" },
                    { 8, 2, "Chile", "CL" },
                    { 9, 2, "Colombia", "CO" },
                    { 10, 2, "Peru", "PE" },
                    { 11, 2, "Uruguay", "UY" },
                    { 12, 2, "Venezuela", "VE" },
                    { 13, 3, "Germany", "DE" },
                    { 14, 4, "Poland", "PL" },
                    { 15, 4, "Russian Federation", "RU" },
                    { 16, 4, "Austria", "AT" },
                    { 17, 4, "Bulgaria", "BG" },
                    { 18, 4, "Switzerland", "CH" },
                    { 19, 4, "Cyprus", "CY" },
                    { 20, 4, "Czech Republic", "CZ" },
                    { 21, 4, "Greece", "GR" },
                    { 22, 4, "Croatia", "HR" },
                    { 23, 4, "Hungary", "HU" },
                    { 24, 4, "Israel", "IL" },
                    { 25, 4, "Kasakhstan", "KZ" },
                    { 26, 4, "Romania", "RO" },
                    { 27, 4, "Serbia", "RS" },
                    { 28, 4, "Slovakia", "SK" },
                    { 29, 4, "Ukraine", "UA" },
                    { 30, 5, "United Arab Emirates", "AE" },
                    { 31, 5, "Egypt", "EG" },
                    { 32, 5, "Iran", "IR" },
                    { 33, 5, "Lebanon", "LB" },
                    { 34, 5, "Qatar", "QA" },
                    { 35, 5, "Saudi Arabia", "SA" },
                    { 36, 5, "Turkey", "TR" },
                    { 37, 6, "Burkina Faso", "BF" },
                    { 38, 6, "Benin", "BJ" },
                    { 39, 6, "Cote d'Ivoire", "CI" },
                    { 40, 6, "Algeria", "DZ" },
                    { 41, 6, "Gabon", "GA" },
                    { 42, 6, "Ivory Coast", "CI" },
                    { 43, 6, "Morocco", "MA" },
                    { 44, 6, "Madagascar", "MG" },
                    { 45, 6, "Mali", "ML" },
                    { 46, 6, "Mauritius", "MU" },
                    { 47, 6, "Senegal", "SN" },
                    { 48, 6, "Tunisia", "TN" },
                    { 49, 6, "South Africa", "ZA" },
                    { 50, 7, "India", "IN" },
                    { 51, 8, "Australia", "AU" },
                    { 52, 8, "People Rep China", "CN" },
                    { 53, 8, "Hong Kong", "HK" },
                    { 54, 8, "Indonesia", "ID" },
                    { 55, 8, "Japan", "JP" },
                    { 56, 8, "Korea", "KR" },
                    { 57, 8, "Malaysia", "MY" },
                    { 58, 8, "New Zealand", "NZ" },
                    { 59, 8, "Philippines", "PH" },
                    { 60, 8, "Singapore", "SG" },
                    { 61, 8, "Thailand", "TH" },
                    { 62, 8, "Taiwan", "TW" },
                    { 63, 9, "Belgium", "BE" },
                    { 64, 9, "Denmark", "DK" },
                    { 65, 9, "Estonia", "EE" },
                    { 66, 9, "Finland", "FI" },
                    { 67, 9, "Lithuania", "LT" },
                    { 68, 9, "Luxembourg", "LU" },
                    { 69, 9, "Netherlands", "NL" },
                    { 70, 9, "Norway", "NO" },
                    { 71, 9, "Sweden", "SE" },
                    { 72, 10, "United Kingdom", "GB" },
                    { 73, 10, "Ireland", "IE" },
                    { 74, 11, "Italy", "IT" },
                    { 75, 12, "Andorra", "AD" },
                    { 76, 12, "Spain", "ES" },
                    { 77, 12, "Portugal", "PT" },
                    { 78, 13, "France", "FR" },
                    { 79, 13, "Morocco", "MA" },
                    { 80, 13, "New Caledonia", "NC" },
                    { 81, 13, "French Polynesia", "PF" }
                });

            migrationBuilder.InsertData(
                table: "DistributionInformation",
                columns: new[] { "Id", "CountryId", "LocationOptionId", "PriorityId" },
                values: new object[,]
                {
                    { 1, 4, 1, 5 },
                    { 2, 4, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "RecipientsGroup",
                columns: new[] { "Id", "DestinationFieldId", "DistributionInformationId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessUnit_AreaId",
                table: "BuisnessUnit",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_BuisnessUnitId",
                table: "Country",
                column: "BuisnessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_CountryId",
                table: "DistributionInformation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_LocationOptionId",
                table: "DistributionInformation",
                column: "LocationOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformation_PriorityId",
                table: "DistributionInformation",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientRecipientsGroup_RecipientsId",
                table: "RecipientRecipientsGroup",
                column: "RecipientsId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsGroup_DestinationFieldId",
                table: "RecipientsGroup",
                column: "DestinationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsGroup_DistributionInformationId",
                table: "RecipientsGroup",
                column: "DistributionInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipientRecipientsGroup");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "RecipientsGroup");

            migrationBuilder.DropTable(
                name: "DestinationField");

            migrationBuilder.DropTable(
                name: "DistributionInformation");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "LocationOption");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "BuisnessUnit");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
