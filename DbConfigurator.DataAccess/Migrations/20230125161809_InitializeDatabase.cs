using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinationFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
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
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessUnits_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipientsGroups_DestinationFields_DestinationFieldId",
                        column: x => x.DestinationFieldId,
                        principalTable: "DestinationFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BuisnessUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_BuisnessUnits_BuisnessUnitId",
                        column: x => x.BuisnessUnitId,
                        principalTable: "BuisnessUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DistributionInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    BuisnessUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionInformations_BuisnessUnits_BuisnessUnitId",
                        column: x => x.BuisnessUnitId,
                        principalTable: "BuisnessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistributionInformations_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
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
                        name: "FK_RecipientRecipientsGroup_RecipientsGroups_RecipientsGroupsId",
                        column: x => x.RecipientsGroupsId,
                        principalTable: "RecipientsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientRecipientsGroup_Recipients_RecipientsId",
                        column: x => x.RecipientsId,
                        principalTable: "Recipients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributionInformationRecipientsGroup",
                columns: table => new
                {
                    DistributionInformationsId = table.Column<int>(type: "int", nullable: false),
                    RecipientsGroupCollectionId = table.Column<int>(name: "RecipientsGroup_CollectionId", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionInformationRecipientsGroup", x => new { x.DistributionInformationsId, x.RecipientsGroupCollectionId });
                    table.ForeignKey(
                        name: "FK_DistributionInformationRecipientsGroup_DistributionInformations_DistributionInformationsId",
                        column: x => x.DistributionInformationsId,
                        principalTable: "DistributionInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributionInformationRecipientsGroup_RecipientsGroups_RecipientsGroup_CollectionId",
                        column: x => x.RecipientsGroupCollectionId,
                        principalTable: "RecipientsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessUnits_AreaId",
                table: "BuisnessUnits",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_BuisnessUnitId",
                table: "Countries",
                column: "BuisnessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformationRecipientsGroup_RecipientsGroup_CollectionId",
                table: "DistributionInformationRecipientsGroup",
                column: "RecipientsGroup_CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformations_BuisnessUnitId",
                table: "DistributionInformations",
                column: "BuisnessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionInformations_PriorityId",
                table: "DistributionInformations",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientRecipientsGroup_RecipientsId",
                table: "RecipientRecipientsGroup",
                column: "RecipientsId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientsGroups_DestinationFieldId",
                table: "RecipientsGroups",
                column: "DestinationFieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "DistributionInformationRecipientsGroup");

            migrationBuilder.DropTable(
                name: "RecipientRecipientsGroup");

            migrationBuilder.DropTable(
                name: "DistributionInformations");

            migrationBuilder.DropTable(
                name: "RecipientsGroups");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "BuisnessUnits");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "DestinationFields");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
