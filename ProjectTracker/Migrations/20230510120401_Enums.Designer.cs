﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectTracker.Data;

#nullable disable

namespace ProjectTracker.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230510120401_Enums")]
    partial class Enums
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PersonProject", b =>
                {
                    b.Property<int>("PersonsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("PersonsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("PersonProject");
                });

            modelBuilder.Entity("PersonProjectItem", b =>
                {
                    b.Property<int>("PersonsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectItemsId")
                        .HasColumnType("int");

                    b.HasKey("PersonsId", "ProjectItemsId");

                    b.HasIndex("ProjectItemsId");

                    b.ToTable("PersonProjectItem");
                });

            modelBuilder.Entity("ProjectTracker.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("ProjectTracker.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectTracker.Models.ProjectItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Completed")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Importance")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("TimeToComplete")
                        .HasColumnType("time(6)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectItem");
                });

            modelBuilder.Entity("PersonProject", b =>
                {
                    b.HasOne("ProjectTracker.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectTracker.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonProjectItem", b =>
                {
                    b.HasOne("ProjectTracker.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectTracker.Models.ProjectItem", null)
                        .WithMany()
                        .HasForeignKey("ProjectItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectTracker.Models.ProjectItem", b =>
                {
                    b.HasOne("ProjectTracker.Models.Project", "Project")
                        .WithMany("ProjectItems")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectTracker.Models.Project", b =>
                {
                    b.Navigation("ProjectItems");
                });
#pragma warning restore 612, 618
        }
    }
}
