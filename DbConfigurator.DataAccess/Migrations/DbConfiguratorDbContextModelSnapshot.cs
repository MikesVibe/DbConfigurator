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

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Area", b =>
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
                        },
                        new
                        {
                            Id = 99,
                            Name = "ANY"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.BusinessUnit", b =>
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

                    b.ToTable("BusinessUnit");

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
                        },
                        new
                        {
                            Id = 99,
                            Name = "ANY"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryCode = "CA",
                            CountryName = "Canada"
                        },
                        new
                        {
                            Id = 2,
                            CountryCode = "GT",
                            CountryName = "Guatemala"
                        },
                        new
                        {
                            Id = 3,
                            CountryCode = "MX",
                            CountryName = "Mexico"
                        },
                        new
                        {
                            Id = 4,
                            CountryCode = "PR",
                            CountryName = "Puerto Rico"
                        },
                        new
                        {
                            Id = 5,
                            CountryCode = "US",
                            CountryName = "USA"
                        },
                        new
                        {
                            Id = 6,
                            CountryCode = "AR",
                            CountryName = "Argentina"
                        },
                        new
                        {
                            Id = 7,
                            CountryCode = "BR",
                            CountryName = "Brazil"
                        },
                        new
                        {
                            Id = 8,
                            CountryCode = "CL",
                            CountryName = "Chile"
                        },
                        new
                        {
                            Id = 9,
                            CountryCode = "CO",
                            CountryName = "Colombia"
                        },
                        new
                        {
                            Id = 10,
                            CountryCode = "PE",
                            CountryName = "Peru"
                        },
                        new
                        {
                            Id = 11,
                            CountryCode = "UY",
                            CountryName = "Uruguay"
                        },
                        new
                        {
                            Id = 12,
                            CountryCode = "VE",
                            CountryName = "Venezuela"
                        },
                        new
                        {
                            Id = 13,
                            CountryCode = "DE",
                            CountryName = "Germany"
                        },
                        new
                        {
                            Id = 14,
                            CountryCode = "PL",
                            CountryName = "Poland"
                        },
                        new
                        {
                            Id = 15,
                            CountryCode = "RU",
                            CountryName = "Russian Federation"
                        },
                        new
                        {
                            Id = 16,
                            CountryCode = "AT",
                            CountryName = "Austria"
                        },
                        new
                        {
                            Id = 17,
                            CountryCode = "BG",
                            CountryName = "Bulgaria"
                        },
                        new
                        {
                            Id = 18,
                            CountryCode = "CH",
                            CountryName = "Switzerland"
                        },
                        new
                        {
                            Id = 19,
                            CountryCode = "CY",
                            CountryName = "Cyprus"
                        },
                        new
                        {
                            Id = 20,
                            CountryCode = "CZ",
                            CountryName = "Czech Republic"
                        },
                        new
                        {
                            Id = 21,
                            CountryCode = "GR",
                            CountryName = "Greece"
                        },
                        new
                        {
                            Id = 22,
                            CountryCode = "HR",
                            CountryName = "Croatia"
                        },
                        new
                        {
                            Id = 23,
                            CountryCode = "HU",
                            CountryName = "Hungary"
                        },
                        new
                        {
                            Id = 24,
                            CountryCode = "IL",
                            CountryName = "Israel"
                        },
                        new
                        {
                            Id = 25,
                            CountryCode = "KZ",
                            CountryName = "Kasakhstan"
                        },
                        new
                        {
                            Id = 26,
                            CountryCode = "RO",
                            CountryName = "Romania"
                        },
                        new
                        {
                            Id = 27,
                            CountryCode = "RS",
                            CountryName = "Serbia"
                        },
                        new
                        {
                            Id = 28,
                            CountryCode = "SK",
                            CountryName = "Slovakia"
                        },
                        new
                        {
                            Id = 29,
                            CountryCode = "UA",
                            CountryName = "Ukraine"
                        },
                        new
                        {
                            Id = 30,
                            CountryCode = "AE",
                            CountryName = "United Arab Emirates"
                        },
                        new
                        {
                            Id = 31,
                            CountryCode = "EG",
                            CountryName = "Egypt"
                        },
                        new
                        {
                            Id = 32,
                            CountryCode = "IR",
                            CountryName = "Iran"
                        },
                        new
                        {
                            Id = 33,
                            CountryCode = "LB",
                            CountryName = "Lebanon"
                        },
                        new
                        {
                            Id = 34,
                            CountryCode = "QA",
                            CountryName = "Qatar"
                        },
                        new
                        {
                            Id = 35,
                            CountryCode = "SA",
                            CountryName = "Saudi Arabia"
                        },
                        new
                        {
                            Id = 36,
                            CountryCode = "TR",
                            CountryName = "Turkey"
                        },
                        new
                        {
                            Id = 37,
                            CountryCode = "BF",
                            CountryName = "Burkina Faso"
                        },
                        new
                        {
                            Id = 38,
                            CountryCode = "BJ",
                            CountryName = "Benin"
                        },
                        new
                        {
                            Id = 39,
                            CountryCode = "CI",
                            CountryName = "Cote d'Ivoire"
                        },
                        new
                        {
                            Id = 40,
                            CountryCode = "DZ",
                            CountryName = "Algeria"
                        },
                        new
                        {
                            Id = 41,
                            CountryCode = "GA",
                            CountryName = "Gabon"
                        },
                        new
                        {
                            Id = 42,
                            CountryCode = "CI",
                            CountryName = "Ivory Coast"
                        },
                        new
                        {
                            Id = 43,
                            CountryCode = "MA",
                            CountryName = "Morocco"
                        },
                        new
                        {
                            Id = 44,
                            CountryCode = "MG",
                            CountryName = "Madagascar"
                        },
                        new
                        {
                            Id = 45,
                            CountryCode = "ML",
                            CountryName = "Mali"
                        },
                        new
                        {
                            Id = 46,
                            CountryCode = "MU",
                            CountryName = "Mauritius"
                        },
                        new
                        {
                            Id = 47,
                            CountryCode = "SN",
                            CountryName = "Senegal"
                        },
                        new
                        {
                            Id = 48,
                            CountryCode = "TN",
                            CountryName = "Tunisia"
                        },
                        new
                        {
                            Id = 49,
                            CountryCode = "ZA",
                            CountryName = "South Africa"
                        },
                        new
                        {
                            Id = 50,
                            CountryCode = "IN",
                            CountryName = "India"
                        },
                        new
                        {
                            Id = 51,
                            CountryCode = "AU",
                            CountryName = "Australia"
                        },
                        new
                        {
                            Id = 52,
                            CountryCode = "CN",
                            CountryName = "People Rep China"
                        },
                        new
                        {
                            Id = 53,
                            CountryCode = "HK",
                            CountryName = "Hong Kong"
                        },
                        new
                        {
                            Id = 54,
                            CountryCode = "ID",
                            CountryName = "Indonesia"
                        },
                        new
                        {
                            Id = 55,
                            CountryCode = "JP",
                            CountryName = "Japan"
                        },
                        new
                        {
                            Id = 56,
                            CountryCode = "KR",
                            CountryName = "Korea"
                        },
                        new
                        {
                            Id = 57,
                            CountryCode = "MY",
                            CountryName = "Malaysia"
                        },
                        new
                        {
                            Id = 58,
                            CountryCode = "NZ",
                            CountryName = "New Zealand"
                        },
                        new
                        {
                            Id = 59,
                            CountryCode = "PH",
                            CountryName = "Philippines"
                        },
                        new
                        {
                            Id = 60,
                            CountryCode = "SG",
                            CountryName = "Singapore"
                        },
                        new
                        {
                            Id = 61,
                            CountryCode = "TH",
                            CountryName = "Thailand"
                        },
                        new
                        {
                            Id = 62,
                            CountryCode = "TW",
                            CountryName = "Taiwan"
                        },
                        new
                        {
                            Id = 63,
                            CountryCode = "BE",
                            CountryName = "Belgium"
                        },
                        new
                        {
                            Id = 64,
                            CountryCode = "DK",
                            CountryName = "Denmark"
                        },
                        new
                        {
                            Id = 65,
                            CountryCode = "EE",
                            CountryName = "Estonia"
                        },
                        new
                        {
                            Id = 66,
                            CountryCode = "FI",
                            CountryName = "Finland"
                        },
                        new
                        {
                            Id = 67,
                            CountryCode = "LT",
                            CountryName = "Lithuania"
                        },
                        new
                        {
                            Id = 68,
                            CountryCode = "LU",
                            CountryName = "Luxembourg"
                        },
                        new
                        {
                            Id = 69,
                            CountryCode = "NL",
                            CountryName = "Netherlands"
                        },
                        new
                        {
                            Id = 70,
                            CountryCode = "NO",
                            CountryName = "Norway"
                        },
                        new
                        {
                            Id = 71,
                            CountryCode = "SE",
                            CountryName = "Sweden"
                        },
                        new
                        {
                            Id = 72,
                            CountryCode = "GB",
                            CountryName = "United Kingdom"
                        },
                        new
                        {
                            Id = 73,
                            CountryCode = "IE",
                            CountryName = "Ireland"
                        },
                        new
                        {
                            Id = 74,
                            CountryCode = "IT",
                            CountryName = "Italy"
                        },
                        new
                        {
                            Id = 75,
                            CountryCode = "AD",
                            CountryName = "Andorra"
                        },
                        new
                        {
                            Id = 76,
                            CountryCode = "ES",
                            CountryName = "Spain"
                        },
                        new
                        {
                            Id = 77,
                            CountryCode = "PT",
                            CountryName = "Portugal"
                        },
                        new
                        {
                            Id = 78,
                            CountryCode = "FR",
                            CountryName = "France"
                        },
                        new
                        {
                            Id = 79,
                            CountryCode = "MA",
                            CountryName = "Morocco"
                        },
                        new
                        {
                            Id = 80,
                            CountryCode = "NC",
                            CountryName = "New Caledonia"
                        },
                        new
                        {
                            Id = 81,
                            CountryCode = "PF",
                            CountryName = "French Polynesia"
                        },
                        new
                        {
                            Id = 99,
                            CountryCode = "ANY",
                            CountryName = "ANY"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.DistributionInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("RegionId");

                    b.ToTable("DistributionInformation");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Priority", b =>
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
                            Id = 99,
                            Name = "ANY"
                        });
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Recipient", b =>
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
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.RecipientGroupCc", b =>
                {
                    b.Property<int>("DistributionInformationId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.HasKey("DistributionInformationId", "RecipientId");

                    b.HasIndex("RecipientId");

                    b.ToTable("RecipientGroupCc");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.RecipientGroupTo", b =>
                {
                    b.Property<int>("DistributionInformationId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.HasKey("DistributionInformationId", "RecipientId");

                    b.HasIndex("RecipientId");

                    b.ToTable("RecipientGroupTo");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessUnitId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("BusinessUnitId");

                    b.HasIndex("CountryId");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.DistributionInformation", b =>
                {
                    b.HasOne("DbConfigurator.Model.Entities.Core.Priority", "Priority")
                        .WithMany("DistributionInformations")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Entities.Core.Region", "Region")
                        .WithMany("DistributionInformations")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Priority");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.RecipientGroupCc", b =>
                {
                    b.HasOne("DbConfigurator.Model.Entities.Core.DistributionInformation", "RecipientGroup")
                        .WithMany()
                        .HasForeignKey("DistributionInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Entities.Core.Recipient", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("RecipientGroup");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.RecipientGroupTo", b =>
                {
                    b.HasOne("DbConfigurator.Model.Entities.Core.DistributionInformation", "RecipientGroup")
                        .WithMany()
                        .HasForeignKey("DistributionInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Entities.Core.Recipient", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("RecipientGroup");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Region", b =>
                {
                    b.HasOne("DbConfigurator.Model.Entities.Core.Area", "Area")
                        .WithMany("Regions")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Entities.Core.BusinessUnit", "BusinessUnit")
                        .WithMany("Regions")
                        .HasForeignKey("BusinessUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbConfigurator.Model.Entities.Core.Country", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("BusinessUnit");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Area", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.BusinessUnit", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Country", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Priority", b =>
                {
                    b.Navigation("DistributionInformations");
                });

            modelBuilder.Entity("DbConfigurator.Model.Entities.Core.Region", b =>
                {
                    b.Navigation("DistributionInformations");
                });
#pragma warning restore 612, 618
        }
    }
}
