﻿// <auto-generated />
using DbConfigurator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbConfigurator.DataAccess.Migrations
{
    [DbContext(typeof(DbConfiguratorDbContext))]
    [Migration("20230109224335_NotificationTableCreation")]
    partial class NotificationTableCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbConfigurator.Model.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessUnitId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientsGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}