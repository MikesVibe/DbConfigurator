﻿// <auto-generated />
using System;
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

            modelBuilder.Entity("AreaBuisnessUnit", b =>
                {
                    b.Property<int>("AreasId")
                        .HasColumnType("int");

                    b.Property<int>("BuisnessUnitsId")
                        .HasColumnType("int");

                    b.HasKey("AreasId", "BuisnessUnitsId");

                    b.HasIndex("BuisnessUnitsId");

                    b.ToTable("AreaBuisnessUnit");
                });

            modelBuilder.Entity("BuisnessUnitCountry", b =>
                {
                    b.Property<int>("BuisnessUnitsId")
                        .HasColumnType("int");

                    b.Property<int>("CountriesId")
                        .HasColumnType("int");

                    b.HasKey("BuisnessUnitsId", "CountriesId");

                    b.HasIndex("CountriesId");

                    b.ToTable("BuisnessUnitCountry");
                });

            modelBuilder.Entity("DbConfigurator.Model.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Area");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("BuisnessUnit");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "NAO"
                        },
                        new
                        {
                            Id = 2,
                            Name = "SAM"
                        },
                        new
                        {
                            Id = 3,
                            Name = "GER"
                        },
                        new
                        {
                            Id = 4,
                            Name = "CEE"
                        },
                        new
                        {
                            Id = 5,
                            Name = "MEK"
                        },
                        new
                        {
                            Id = 6,
                            Name = "AFR"
                        },
                        new
                        {
                            Id = 7,
                            Name = "IND"
                        },
                        new
                        {
                            Id = 8,
                            Name = "APAC"
                        },
                        new
                        {
                            Id = 9,
                            Name = "BTN"
                        },
                        new
                        {
                            Id = 10,
                            Name = "UK&I"
                        },
                        new
                        {
                            Id = 11,
                            Name = "ITA"
                        },
                        new
                        {
                            Id = 12,
                            Name = "IBE"
                        },
                        new
                        {
                            Id = 13,
                            Name = "FRA"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Canada",
                            ShortCode = "CA"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Guatemala",
                            ShortCode = "GT"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mexico",
                            ShortCode = "MX"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Puerto Rico",
                            ShortCode = "PR"
                        },
                        new
                        {
                            Id = 5,
                            Name = "USA",
                            ShortCode = "US"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Argentina",
                            ShortCode = "AR"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Brazil",
                            ShortCode = "BR"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Chile",
                            ShortCode = "CL"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Colombia",
                            ShortCode = "CO"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Peru",
                            ShortCode = "PE"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Uruguay",
                            ShortCode = "UY"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Venezuela",
                            ShortCode = "VE"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Germany",
                            ShortCode = "DE"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Poland",
                            ShortCode = "PL"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Russian Federation",
                            ShortCode = "RU"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Austria",
                            ShortCode = "AT"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Bulgaria",
                            ShortCode = "BG"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Switzerland",
                            ShortCode = "CH"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Cyprus",
                            ShortCode = "CY"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Czech Republic",
                            ShortCode = "CZ"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Greece",
                            ShortCode = "GR"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Croatia",
                            ShortCode = "HR"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Hungary",
                            ShortCode = "HU"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Israel",
                            ShortCode = "IL"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Kasakhstan",
                            ShortCode = "KZ"
                        },
                        new
                        {
                            Id = 26,
                            Name = "Romania",
                            ShortCode = "RO"
                        },
                        new
                        {
                            Id = 27,
                            Name = "Serbia",
                            ShortCode = "RS"
                        },
                        new
                        {
                            Id = 28,
                            Name = "Slovakia",
                            ShortCode = "SK"
                        },
                        new
                        {
                            Id = 29,
                            Name = "Ukraine",
                            ShortCode = "UA"
                        },
                        new
                        {
                            Id = 30,
                            Name = "United Arab Emirates",
                            ShortCode = "AE"
                        },
                        new
                        {
                            Id = 31,
                            Name = "Egypt",
                            ShortCode = "EG"
                        },
                        new
                        {
                            Id = 32,
                            Name = "Iran",
                            ShortCode = "IR"
                        },
                        new
                        {
                            Id = 33,
                            Name = "Lebanon",
                            ShortCode = "LB"
                        },
                        new
                        {
                            Id = 34,
                            Name = "Qatar",
                            ShortCode = "QA"
                        },
                        new
                        {
                            Id = 35,
                            Name = "Saudi Arabia",
                            ShortCode = "SA"
                        },
                        new
                        {
                            Id = 36,
                            Name = "Turkey",
                            ShortCode = "TR"
                        },
                        new
                        {
                            Id = 37,
                            Name = "Burkina Faso",
                            ShortCode = "BF"
                        },
                        new
                        {
                            Id = 38,
                            Name = "Benin",
                            ShortCode = "BJ"
                        },
                        new
                        {
                            Id = 39,
                            Name = "Cote d'Ivoire",
                            ShortCode = "CI"
                        },
                        new
                        {
                            Id = 40,
                            Name = "Algeria",
                            ShortCode = "DZ"
                        },
                        new
                        {
                            Id = 41,
                            Name = "Gabon",
                            ShortCode = "GA"
                        },
                        new
                        {
                            Id = 42,
                            Name = "Ivory Coast",
                            ShortCode = "CI"
                        },
                        new
                        {
                            Id = 43,
                            Name = "Morocco",
                            ShortCode = "MA"
                        },
                        new
                        {
                            Id = 44,
                            Name = "Madagascar",
                            ShortCode = "MG"
                        },
                        new
                        {
                            Id = 45,
                            Name = "Mali",
                            ShortCode = "ML"
                        },
                        new
                        {
                            Id = 46,
                            Name = "Mauritius",
                            ShortCode = "MU"
                        },
                        new
                        {
                            Id = 47,
                            Name = "Senegal",
                            ShortCode = "SN"
                        },
                        new
                        {
                            Id = 48,
                            Name = "Tunisia",
                            ShortCode = "TN"
                        },
                        new
                        {
                            Id = 49,
                            Name = "South Africa",
                            ShortCode = "ZA"
                        },
                        new
                        {
                            Id = 50,
                            Name = "India",
                            ShortCode = "IN"
                        },
                        new
                        {
                            Id = 51,
                            Name = "Australia",
                            ShortCode = "AU"
                        },
                        new
                        {
                            Id = 52,
                            Name = "People Rep China",
                            ShortCode = "CN"
                        },
                        new
                        {
                            Id = 53,
                            Name = "Hong Kong",
                            ShortCode = "HK"
                        },
                        new
                        {
                            Id = 54,
                            Name = "Indonesia",
                            ShortCode = "ID"
                        },
                        new
                        {
                            Id = 55,
                            Name = "Japan",
                            ShortCode = "JP"
                        },
                        new
                        {
                            Id = 56,
                            Name = "Korea",
                            ShortCode = "KR"
                        },
                        new
                        {
                            Id = 57,
                            Name = "Malaysia",
                            ShortCode = "MY"
                        },
                        new
                        {
                            Id = 58,
                            Name = "New Zealand",
                            ShortCode = "NZ"
                        },
                        new
                        {
                            Id = 59,
                            Name = "Philippines",
                            ShortCode = "PH"
                        },
                        new
                        {
                            Id = 60,
                            Name = "Singapore",
                            ShortCode = "SG"
                        },
                        new
                        {
                            Id = 61,
                            Name = "Thailand",
                            ShortCode = "TH"
                        },
                        new
                        {
                            Id = 62,
                            Name = "Taiwan",
                            ShortCode = "TW"
                        },
                        new
                        {
                            Id = 63,
                            Name = "Belgium",
                            ShortCode = "BE"
                        },
                        new
                        {
                            Id = 64,
                            Name = "Denmark",
                            ShortCode = "DK"
                        },
                        new
                        {
                            Id = 65,
                            Name = "Estonia",
                            ShortCode = "EE"
                        },
                        new
                        {
                            Id = 66,
                            Name = "Finland",
                            ShortCode = "FI"
                        },
                        new
                        {
                            Id = 67,
                            Name = "Lithuania",
                            ShortCode = "LT"
                        },
                        new
                        {
                            Id = 68,
                            Name = "Luxembourg",
                            ShortCode = "LU"
                        },
                        new
                        {
                            Id = 69,
                            Name = "Netherlands",
                            ShortCode = "NL"
                        },
                        new
                        {
                            Id = 70,
                            Name = "Norway",
                            ShortCode = "NO"
                        },
                        new
                        {
                            Id = 71,
                            Name = "Sweden",
                            ShortCode = "SE"
                        },
                        new
                        {
                            Id = 72,
                            Name = "United Kingdom",
                            ShortCode = "GB"
                        },
                        new
                        {
                            Id = 73,
                            Name = "Ireland",
                            ShortCode = "IE"
                        },
                        new
                        {
                            Id = 74,
                            Name = "Italy",
                            ShortCode = "IT"
                        },
                        new
                        {
                            Id = 75,
                            Name = "Andorra",
                            ShortCode = "AD"
                        },
                        new
                        {
                            Id = 76,
                            Name = "Spain",
                            ShortCode = "ES"
                        },
                        new
                        {
                            Id = 77,
                            Name = "Portugal",
                            ShortCode = "PT"
                        },
                        new
                        {
                            Id = 78,
                            Name = "France",
                            ShortCode = "FR"
                        },
                        new
                        {
                            Id = 79,
                            Name = "Morocco",
                            ShortCode = "MA"
                        },
                        new
                        {
                            Id = 80,
                            Name = "New Caledonia",
                            ShortCode = "NC"
                        },
                        new
                        {
                            Id = 81,
                            Name = "French Polynesia",
                            ShortCode = "PF"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.DistributionInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CcRecipientsGroupId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("ToRecipientsGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CcRecipientsGroupId");

                    b.HasIndex("CountryId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ToRecipientsGroupId");

                    b.ToTable("DistributionInformation");
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

                    b.ToTable("Priority");

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

                    b.ToTable("Recipient");

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

                    b.Property<int?>("DistributionInformationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DistributionInformationId");

                    b.ToTable("RecipientsGroup");
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

            modelBuilder.Entity("AreaBuisnessUnit", b =>
                {
                    b.HasOne("DbConfigurator.Model.Area", null)
                        .WithMany()
                        .HasForeignKey("AreasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.BuisnessUnit", null)
                        .WithMany()
                        .HasForeignKey("BuisnessUnitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BuisnessUnitCountry", b =>
                {
                    b.HasOne("DbConfigurator.Model.BuisnessUnit", null)
                        .WithMany()
                        .HasForeignKey("BuisnessUnitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Country", null)
                        .WithMany()
                        .HasForeignKey("CountriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DbConfigurator.Model.DistributionInformation", b =>
                {
                    b.HasOne("DbConfigurator.Model.RecipientsGroup", "CcRecipientsGroup")
                        .WithMany()
                        .HasForeignKey("CcRecipientsGroupId");

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

                    b.HasOne("DbConfigurator.Model.RecipientsGroup", "ToRecipientsGroup")
                        .WithMany()
                        .HasForeignKey("ToRecipientsGroupId");

                    b.Navigation("CcRecipientsGroup");

                    b.Navigation("Country");

                    b.Navigation("Priority");

                    b.Navigation("ToRecipientsGroup");
                });

            modelBuilder.Entity("DbConfigurator.Model.RecipientsGroup", b =>
                {
                    b.HasOne("DbConfigurator.Model.DistributionInformation", "DistributionInformation")
                        .WithMany()
                        .HasForeignKey("DistributionInformationId");

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

            modelBuilder.Entity("DbConfigurator.Model.Country", b =>
                {
                    b.Navigation("DistributionInformations");
                });

            modelBuilder.Entity("DbConfigurator.Model.Priority", b =>
                {
                    b.Navigation("DistributionInformations");
                });
#pragma warning restore 612, 618
        }
    }
}
