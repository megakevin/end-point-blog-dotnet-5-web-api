﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VehicleQuotes;

namespace VehicleQuotes.Migrations
{
    [DbContext(typeof(VehicleQuotesContext))]
    [Migration("20210625234824_AddUniqueIndexesForVehicleModelTables")]
    partial class AddUniqueIndexesForVehicleModelTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("VehicleQuotes.Models.BodyType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_body_types");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_body_types_name");

                    b.ToTable("body_types");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Make", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_makes");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_makes_name");

                    b.ToTable("makes");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Model", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MakeID")
                        .HasColumnType("integer")
                        .HasColumnName("make_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_models");

                    b.HasIndex("MakeID")
                        .HasDatabaseName("ix_models_make_id");

                    b.HasIndex("Name", "MakeID")
                        .IsUnique()
                        .HasDatabaseName("ix_models_name_make_id");

                    b.ToTable("models");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BodyTypeID")
                        .HasColumnType("integer")
                        .HasColumnName("body_type_id");

                    b.Property<int>("ModelID")
                        .HasColumnType("integer")
                        .HasColumnName("model_id");

                    b.Property<int>("SizeID")
                        .HasColumnType("integer")
                        .HasColumnName("size_id");

                    b.HasKey("ID")
                        .HasName("pk_model_styles");

                    b.HasIndex("BodyTypeID")
                        .HasDatabaseName("ix_model_styles_body_type_id");

                    b.HasIndex("SizeID")
                        .HasDatabaseName("ix_model_styles_size_id");

                    b.HasIndex("ModelID", "BodyTypeID", "SizeID")
                        .IsUnique()
                        .HasDatabaseName("ix_model_styles_model_id_body_type_id_size_id");

                    b.ToTable("model_styles");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyleYear", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ModelStyleID")
                        .HasColumnType("integer")
                        .HasColumnName("model_style_id");

                    b.Property<string>("Year")
                        .HasColumnType("text")
                        .HasColumnName("year");

                    b.HasKey("ID")
                        .HasName("pk_model_style_years");

                    b.HasIndex("ModelStyleID")
                        .HasDatabaseName("ix_model_style_years_model_style_id");

                    b.HasIndex("Year", "ModelStyleID")
                        .IsUnique()
                        .HasDatabaseName("ix_model_style_years_year_model_style_id");

                    b.ToTable("model_style_years");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Size", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_sizes");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_sizes_name");

                    b.ToTable("sizes");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Model", b =>
                {
                    b.HasOne("VehicleQuotes.Models.Make", "Make")
                        .WithMany()
                        .HasForeignKey("MakeID")
                        .HasConstraintName("fk_models_makes_make_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Make");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyle", b =>
                {
                    b.HasOne("VehicleQuotes.Models.BodyType", "BodyType")
                        .WithMany()
                        .HasForeignKey("BodyTypeID")
                        .HasConstraintName("fk_model_styles_body_types_body_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleQuotes.Models.Model", "Model")
                        .WithMany("ModelStyles")
                        .HasForeignKey("ModelID")
                        .HasConstraintName("fk_model_styles_models_model_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehicleQuotes.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID")
                        .HasConstraintName("fk_model_styles_sizes_size_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyType");

                    b.Navigation("Model");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyleYear", b =>
                {
                    b.HasOne("VehicleQuotes.Models.ModelStyle", "ModelStyle")
                        .WithMany("ModelStyleYears")
                        .HasForeignKey("ModelStyleID")
                        .HasConstraintName("fk_model_style_years_model_styles_model_style_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModelStyle");
                });

            modelBuilder.Entity("VehicleQuotes.Models.Model", b =>
                {
                    b.Navigation("ModelStyles");
                });

            modelBuilder.Entity("VehicleQuotes.Models.ModelStyle", b =>
                {
                    b.Navigation("ModelStyleYears");
                });
#pragma warning restore 612, 618
        }
    }
}