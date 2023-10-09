﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TESTWebApp.Infrastructure.Database;

#nullable disable

namespace TESTWebApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230817164318_InitialMigrationsV2")]
    partial class InitialMigrationsV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TESTWebApp.Infrastructure.Database.Tables.MAJORWORKITEM", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("major_work_item_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("created_by");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("MajorWorkItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("major_work_item_name");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("modified_by");

                    b.HasKey("Id");

                    b.ToTable("major_work_item");
                });

            modelBuilder.Entity("TESTWebApp.Infrastructure.Database.Tables.USER", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("created_by");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("modified_by");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("TESTWebApp.Infrastructure.Database.Tables.WORKINPUT", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("work_input_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("user_id");

                    b.Property<string>("WorkItem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("work_item_id");

                    b.HasKey("Id");

                    b.ToTable("work_input");
                });
#pragma warning restore 612, 618
        }
    }
}
