﻿// <auto-generated />
using DbConfigurator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    [DbContext(typeof(DbConfiguratorDbContext))]
    partial class DbConfiguratorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbConfigurator.Model.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Americas"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Central Europe"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Growing Markets"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Northern Europe"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Southern Europe"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.BuisnessUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("BuisnessUnits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AreaId = 1,
                            Name = "NAO"
                        },
                        new
                        {
                            Id = 2,
                            AreaId = 1,
                            Name = "SAM"
                        },
                        new
                        {
                            Id = 3,
                            AreaId = 2,
                            Name = "GER"
                        },
                        new
                        {
                            Id = 4,
                            AreaId = 2,
                            Name = "CEE"
                        },
                        new
                        {
                            Id = 5,
                            AreaId = 3,
                            Name = "MEK"
                        },
                        new
                        {
                            Id = 6,
                            AreaId = 3,
                            Name = "AFR"
                        },
                        new
                        {
                            Id = 7,
                            AreaId = 3,
                            Name = "IND"
                        },
                        new
                        {
                            Id = 8,
                            AreaId = 4,
                            Name = "APAC"
                        },
                        new
                        {
                            Id = 9,
                            AreaId = 4,
                            Name = "BTN"
                        },
                        new
                        {
                            Id = 10,
                            AreaId = 4,
                            Name = "UK&I"
                        },
                        new
                        {
                            Id = 11,
                            AreaId = 5,
                            Name = "ITA"
                        },
                        new
                        {
                            Id = 12,
                            AreaId = 5,
                            Name = "IBE"
                        },
                        new
                        {
                            Id = 13,
                            AreaId = 5,
                            Name = "FRA"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuisnessUnitId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("BuisnessUnitId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BuisnessUnitId = 1,
                            Name = "Canada",
                            ShortCode = "CA"
                        },
                        new
                        {
                            Id = 2,
                            BuisnessUnitId = 1,
                            Name = "Guatemala",
                            ShortCode = "GT"
                        },
                        new
                        {
                            Id = 3,
                            BuisnessUnitId = 1,
                            Name = "Mexico",
                            ShortCode = "MX"
                        },
                        new
                        {
                            Id = 4,
                            BuisnessUnitId = 1,
                            Name = "Puerto Rico",
                            ShortCode = "PR"
                        },
                        new
                        {
                            Id = 5,
                            BuisnessUnitId = 1,
                            Name = "USA",
                            ShortCode = "US"
                        },
                        new
                        {
                            Id = 6,
                            BuisnessUnitId = 2,
                            Name = "Argentina",
                            ShortCode = "AR"
                        },
                        new
                        {
                            Id = 7,
                            BuisnessUnitId = 2,
                            Name = "Brazil",
                            ShortCode = "BR"
                        },
                        new
                        {
                            Id = 8,
                            BuisnessUnitId = 2,
                            Name = "Chile",
                            ShortCode = "CL"
                        },
                        new
                        {
                            Id = 9,
                            BuisnessUnitId = 2,
                            Name = "Colombia",
                            ShortCode = "CO"
                        },
                        new
                        {
                            Id = 10,
                            BuisnessUnitId = 2,
                            Name = "Peru",
                            ShortCode = "PE"
                        },
                        new
                        {
                            Id = 11,
                            BuisnessUnitId = 2,
                            Name = "Uruguay",
                            ShortCode = "UY"
                        },
                        new
                        {
                            Id = 12,
                            BuisnessUnitId = 2,
                            Name = "Venezuela",
                            ShortCode = "VE"
                        },
                        new
                        {
                            Id = 13,
                            BuisnessUnitId = 3,
                            Name = "Germany",
                            ShortCode = "DE"
                        },
                        new
                        {
                            Id = 14,
                            BuisnessUnitId = 4,
                            Name = "Poland",
                            ShortCode = "PL"
                        },
                        new
                        {
                            Id = 15,
                            BuisnessUnitId = 4,
                            Name = "Russian Federation",
                            ShortCode = "RU"
                        },
                        new
                        {
                            Id = 16,
                            BuisnessUnitId = 4,
                            Name = "Austria",
                            ShortCode = "AT"
                        },
                        new
                        {
                            Id = 17,
                            BuisnessUnitId = 4,
                            Name = "Bulgaria",
                            ShortCode = "BG"
                        },
                        new
                        {
                            Id = 18,
                            BuisnessUnitId = 4,
                            Name = "Switzerland",
                            ShortCode = "CH"
                        },
                        new
                        {
                            Id = 19,
                            BuisnessUnitId = 4,
                            Name = "Cyprus",
                            ShortCode = "CY"
                        },
                        new
                        {
                            Id = 20,
                            BuisnessUnitId = 4,
                            Name = "Czech Republic",
                            ShortCode = "CZ"
                        },
                        new
                        {
                            Id = 21,
                            BuisnessUnitId = 4,
                            Name = "Greece",
                            ShortCode = "GR"
                        },
                        new
                        {
                            Id = 22,
                            BuisnessUnitId = 4,
                            Name = "Croatia",
                            ShortCode = "HR"
                        },
                        new
                        {
                            Id = 23,
                            BuisnessUnitId = 4,
                            Name = "Hungary",
                            ShortCode = "HU"
                        },
                        new
                        {
                            Id = 24,
                            BuisnessUnitId = 4,
                            Name = "Israel",
                            ShortCode = "IL"
                        },
                        new
                        {
                            Id = 25,
                            BuisnessUnitId = 4,
                            Name = "Kasakhstan",
                            ShortCode = "KZ"
                        },
                        new
                        {
                            Id = 26,
                            BuisnessUnitId = 4,
                            Name = "Romania",
                            ShortCode = "RO"
                        },
                        new
                        {
                            Id = 27,
                            BuisnessUnitId = 4,
                            Name = "Serbia",
                            ShortCode = "RS"
                        },
                        new
                        {
                            Id = 28,
                            BuisnessUnitId = 4,
                            Name = "Slovakia",
                            ShortCode = "SK"
                        },
                        new
                        {
                            Id = 29,
                            BuisnessUnitId = 4,
                            Name = "Ukraine",
                            ShortCode = "UA"
                        },
                        new
                        {
                            Id = 30,
                            BuisnessUnitId = 5,
                            Name = "United Arab Emirates",
                            ShortCode = "AE"
                        },
                        new
                        {
                            Id = 31,
                            BuisnessUnitId = 5,
                            Name = "Egypt",
                            ShortCode = "EG"
                        },
                        new
                        {
                            Id = 32,
                            BuisnessUnitId = 5,
                            Name = "Iran",
                            ShortCode = "IR"
                        },
                        new
                        {
                            Id = 33,
                            BuisnessUnitId = 5,
                            Name = "Lebanon",
                            ShortCode = "LB"
                        },
                        new
                        {
                            Id = 34,
                            BuisnessUnitId = 5,
                            Name = "Qatar",
                            ShortCode = "QA"
                        },
                        new
                        {
                            Id = 35,
                            BuisnessUnitId = 5,
                            Name = "Saudi Arabia",
                            ShortCode = "SA"
                        },
                        new
                        {
                            Id = 36,
                            BuisnessUnitId = 5,
                            Name = "Turkey",
                            ShortCode = "TR"
                        },
                        new
                        {
                            Id = 37,
                            BuisnessUnitId = 6,
                            Name = "Burkina Faso",
                            ShortCode = "BF"
                        },
                        new
                        {
                            Id = 38,
                            BuisnessUnitId = 6,
                            Name = "Benin",
                            ShortCode = "BJ"
                        },
                        new
                        {
                            Id = 39,
                            BuisnessUnitId = 6,
                            Name = "Cote d'Ivoire",
                            ShortCode = "CI"
                        },
                        new
                        {
                            Id = 40,
                            BuisnessUnitId = 6,
                            Name = "Algeria",
                            ShortCode = "DZ"
                        },
                        new
                        {
                            Id = 41,
                            BuisnessUnitId = 6,
                            Name = "Gabon",
                            ShortCode = "GA"
                        },
                        new
                        {
                            Id = 42,
                            BuisnessUnitId = 6,
                            Name = "Ivory Coast",
                            ShortCode = "CI"
                        },
                        new
                        {
                            Id = 43,
                            BuisnessUnitId = 6,
                            Name = "Morocco",
                            ShortCode = "MA"
                        },
                        new
                        {
                            Id = 44,
                            BuisnessUnitId = 6,
                            Name = "Madagascar",
                            ShortCode = "MG"
                        },
                        new
                        {
                            Id = 45,
                            BuisnessUnitId = 6,
                            Name = "Mali",
                            ShortCode = "ML"
                        },
                        new
                        {
                            Id = 46,
                            BuisnessUnitId = 6,
                            Name = "Mauritius",
                            ShortCode = "MU"
                        },
                        new
                        {
                            Id = 47,
                            BuisnessUnitId = 6,
                            Name = "Senegal",
                            ShortCode = "SN"
                        },
                        new
                        {
                            Id = 48,
                            BuisnessUnitId = 6,
                            Name = "Tunisia",
                            ShortCode = "TN"
                        },
                        new
                        {
                            Id = 49,
                            BuisnessUnitId = 6,
                            Name = "South Africa",
                            ShortCode = "ZA"
                        },
                        new
                        {
                            Id = 50,
                            BuisnessUnitId = 7,
                            Name = "India",
                            ShortCode = "IN"
                        },
                        new
                        {
                            Id = 51,
                            BuisnessUnitId = 8,
                            Name = "Australia",
                            ShortCode = "AU"
                        },
                        new
                        {
                            Id = 52,
                            BuisnessUnitId = 8,
                            Name = "People Rep China",
                            ShortCode = "CN"
                        },
                        new
                        {
                            Id = 53,
                            BuisnessUnitId = 8,
                            Name = "Hong Kong",
                            ShortCode = "HK"
                        },
                        new
                        {
                            Id = 54,
                            BuisnessUnitId = 8,
                            Name = "Indonesia",
                            ShortCode = "ID"
                        },
                        new
                        {
                            Id = 55,
                            BuisnessUnitId = 8,
                            Name = "Japan",
                            ShortCode = "JP"
                        },
                        new
                        {
                            Id = 56,
                            BuisnessUnitId = 8,
                            Name = "Korea",
                            ShortCode = "KR"
                        },
                        new
                        {
                            Id = 57,
                            BuisnessUnitId = 8,
                            Name = "Malaysia",
                            ShortCode = "MY"
                        },
                        new
                        {
                            Id = 58,
                            BuisnessUnitId = 8,
                            Name = "New Zealand",
                            ShortCode = "NZ"
                        },
                        new
                        {
                            Id = 59,
                            BuisnessUnitId = 8,
                            Name = "Philippines",
                            ShortCode = "PH"
                        },
                        new
                        {
                            Id = 60,
                            BuisnessUnitId = 8,
                            Name = "Singapore",
                            ShortCode = "SG"
                        },
                        new
                        {
                            Id = 61,
                            BuisnessUnitId = 8,
                            Name = "Thailand",
                            ShortCode = "TH"
                        },
                        new
                        {
                            Id = 62,
                            BuisnessUnitId = 8,
                            Name = "Taiwan",
                            ShortCode = "TW"
                        },
                        new
                        {
                            Id = 63,
                            BuisnessUnitId = 9,
                            Name = "Belgium",
                            ShortCode = "BE"
                        },
                        new
                        {
                            Id = 64,
                            BuisnessUnitId = 9,
                            Name = "Denmark",
                            ShortCode = "DK"
                        },
                        new
                        {
                            Id = 65,
                            BuisnessUnitId = 9,
                            Name = "Estonia",
                            ShortCode = "EE"
                        },
                        new
                        {
                            Id = 66,
                            BuisnessUnitId = 9,
                            Name = "Finland",
                            ShortCode = "FI"
                        },
                        new
                        {
                            Id = 67,
                            BuisnessUnitId = 9,
                            Name = "Lithuania",
                            ShortCode = "LT"
                        },
                        new
                        {
                            Id = 68,
                            BuisnessUnitId = 9,
                            Name = "Luxembourg",
                            ShortCode = "LU"
                        },
                        new
                        {
                            Id = 69,
                            BuisnessUnitId = 9,
                            Name = "Netherlands",
                            ShortCode = "NL"
                        },
                        new
                        {
                            Id = 70,
                            BuisnessUnitId = 9,
                            Name = "Norway",
                            ShortCode = "NO"
                        },
                        new
                        {
                            Id = 71,
                            BuisnessUnitId = 9,
                            Name = "Sweden",
                            ShortCode = "SE"
                        },
                        new
                        {
                            Id = 72,
                            BuisnessUnitId = 10,
                            Name = "United Kingdom",
                            ShortCode = "GB"
                        },
                        new
                        {
                            Id = 73,
                            BuisnessUnitId = 10,
                            Name = "Ireland",
                            ShortCode = "IE"
                        },
                        new
                        {
                            Id = 74,
                            BuisnessUnitId = 11,
                            Name = "Italy",
                            ShortCode = "IT"
                        },
                        new
                        {
                            Id = 75,
                            BuisnessUnitId = 12,
                            Name = "Andorra",
                            ShortCode = "AD"
                        },
                        new
                        {
                            Id = 76,
                            BuisnessUnitId = 12,
                            Name = "Spain",
                            ShortCode = "ES"
                        },
                        new
                        {
                            Id = 77,
                            BuisnessUnitId = 12,
                            Name = "Portugal",
                            ShortCode = "PT"
                        },
                        new
                        {
                            Id = 78,
                            BuisnessUnitId = 13,
                            Name = "France",
                            ShortCode = "FR"
                        },
                        new
                        {
                            Id = 79,
                            BuisnessUnitId = 13,
                            Name = "Morocco",
                            ShortCode = "MA"
                        },
                        new
                        {
                            Id = 80,
                            BuisnessUnitId = 13,
                            Name = "New Caledonia",
                            ShortCode = "NC"
                        },
                        new
                        {
                            Id = 81,
                            BuisnessUnitId = 13,
                            Name = "French Polynesia",
                            ShortCode = "PF"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.DestinationField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("DestinationFields");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "TO"
                        },
                        new
                        {
                            Id = 2,
                            Name = "CC"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.DistributionInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("PriorityId");

                    b.ToTable("DistributionInformations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 4,
                            PriorityId = 5
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.DistributionInformationView", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuisnessUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("DistributionInformationView", (string)null);
                });

            modelBuilder.Entity("DbConfigurator.Model.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("Id");

                    b.ToTable("Priorities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "P1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "P2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "P3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "P4"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Any"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.Recipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "John.Doe@company.net",
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            Email = "Josh.Smith@company.net",
                            FirstName = "Josh",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.RecipientsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DestinationFieldId")
                        .HasColumnType("int");

                    b.Property<int>("DistributionInformationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationFieldId");

                    b.HasIndex("DistributionInformationId");

                    b.ToTable("RecipientsGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DestinationFieldId = 1,
                            DistributionInformationId = 1
                        },
                        new
                        {
                            Id = 2,
                            DestinationFieldId = 2,
                            DistributionInformationId = 1
                        });
                });

            modelBuilder.Entity("RecipientRecipientsGroup", b =>
                {
                    b.Property<int>("RecipientsGroupsId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientsId")
                        .HasColumnType("int");

                    b.HasKey("RecipientsGroupsId", "RecipientsId");

                    b.HasIndex("RecipientsId");

                    b.ToTable("RecipientRecipientsGroup");
                });

            modelBuilder.Entity("DbConfigurator.Model.BuisnessUnit", b =>
                {
                    b.HasOne("DbConfigurator.Model.Area", "Area")
                        .WithMany("BuisnessUnits")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("DbConfigurator.Model.Country", b =>
                {
                    b.HasOne("DbConfigurator.Model.BuisnessUnit", "BuisnessUnit")
                        .WithMany("Countries")
                        .HasForeignKey("BuisnessUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuisnessUnit");
                });

            modelBuilder.Entity("DbConfigurator.Model.DistributionInformation", b =>
                {
                    b.HasOne("DbConfigurator.Model.Country", "Country")
                        .WithMany("DistributionInformations")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Priority", "Priority")
                        .WithMany("DistributionInformations")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Priority");
                });

            modelBuilder.Entity("DbConfigurator.Model.RecipientsGroup", b =>
                {
                    b.HasOne("DbConfigurator.Model.DestinationField", "DestinationField")
                        .WithMany("RecipientsGroups")
                        .HasForeignKey("DestinationFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.DistributionInformation", "DistributionInformation")
                        .WithMany("RecipientsGroup_Collection")
                        .HasForeignKey("DistributionInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationField");

                    b.Navigation("DistributionInformation");
                });

            modelBuilder.Entity("RecipientRecipientsGroup", b =>
                {
                    b.HasOne("DbConfigurator.Model.RecipientsGroup", null)
                        .WithMany()
                        .HasForeignKey("RecipientsGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Recipient", null)
                        .WithMany()
                        .HasForeignKey("RecipientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DbConfigurator.Model.Area", b =>
                {
                    b.Navigation("BuisnessUnits");
                });

            modelBuilder.Entity("DbConfigurator.Model.BuisnessUnit", b =>
                {
                    b.Navigation("Countries");
                });

            modelBuilder.Entity("DbConfigurator.Model.Country", b =>
                {
                    b.Navigation("DistributionInformations");
                });

            modelBuilder.Entity("DbConfigurator.Model.DestinationField", b =>
                {
                    b.Navigation("RecipientsGroups");
                });

            modelBuilder.Entity("DbConfigurator.Model.DistributionInformation", b =>
                {
                    b.Navigation("RecipientsGroup_Collection");
                });

            modelBuilder.Entity("DbConfigurator.Model.Priority", b =>
                {
                    b.Navigation("DistributionInformations");
                });
#pragma warning restore 612, 618
        }
    }
}
