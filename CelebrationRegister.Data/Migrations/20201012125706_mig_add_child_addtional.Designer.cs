﻿// <auto-generated />
using System;
using CelebrationRegister.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CelebrationRegister.Data.Migrations
{
    [DbContext(typeof(CelebrationRegister_Context))]
    [Migration("20201012125706_mig_add_child_addtional")]
    partial class mig_add_child_addtional
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CelebrationRegister.Data.Entities.AdditionalOptions.AdditionalOptions", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("OptionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("OptionId");

                    b.ToTable("AdditionalOptions");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.AdditionalOptions.Child_AdditionalOption", b =>
                {
                    b.Property<int>("CH_AOId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdditionalOptionsOptionId")
                        .HasColumnType("int");

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<int>("OptionId")
                        .HasColumnType("int");

                    b.HasKey("CH_AOId");

                    b.HasIndex("AdditionalOptionsOptionId");

                    b.HasIndex("ChildId");

                    b.ToTable("ChildAdditionalOptions");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Child", b =>
                {
                    b.Property<int>("ChildId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("GradeId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("NationalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int>("ReportCardId")
                        .HasColumnType("int");

                    b.HasKey("ChildId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GradeId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("CityId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.DetailTitle", b =>
                {
                    b.Property<int>("DetailTitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetailTitleId");

                    b.ToTable("DetailTitles");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.DynamicSettings.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDayLimitation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SettingTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SettingId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChildCount")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<bool>("FirstLogin")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("NationalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ProsonnelCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("EmployeeId");

                    b.HasIndex("CityId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GradeTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.HasKey("GradeId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("NotificationId");

                    b.ToTable("Notfications");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.OptionalDetails", b =>
                {
                    b.Property<int>("ODetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChildId")
                        .HasColumnType("int");

                    b.Property<int>("DetailTitleId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int?>("ReportCardId")
                        .HasColumnType("int");

                    b.HasKey("ODetailsId");

                    b.HasIndex("ChildId");

                    b.HasIndex("DetailTitleId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReportCardId");

                    b.ToTable("OptionalDetails");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.ReportCard", b =>
                {
                    b.Property<int>("ReportCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageGrade")
                        .HasColumnType("float");

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<int>("OptionalDetailId")
                        .HasColumnType("int");

                    b.Property<string>("ReportCardImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("ReportCardId");

                    b.HasIndex("ChildId");

                    b.HasIndex("StatusId");

                    b.ToTable("ReportCards");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Role.EmployeeRole", b =>
                {
                    b.Property<int>("ER_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("ER_Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("RoleId");

                    b.ToTable("EmployeeRoles");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Role.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PermissionId");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Role.PermissionRole", b =>
                {
                    b.Property<int>("PR_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("PR_Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("PermissionRoles");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Role.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.AdditionalOptions.Child_AdditionalOption", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.AdditionalOptions.AdditionalOptions", "AdditionalOptions")
                        .WithMany("Children")
                        .HasForeignKey("AdditionalOptionsOptionId");

                    b.HasOne("CelebrationRegister.Data.Entities.Child", "Child")
                        .WithMany("AdditionalOptions")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Child", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.Employee", "Employee")
                        .WithMany("Children")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CelebrationRegister.Data.Entities.Grade", "Grade")
                        .WithMany("ChildrenList")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Employee", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.City", "City")
                        .WithMany("Employees")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.OptionalDetails", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.Child", "Child")
                        .WithMany("OptionalDetails")
                        .HasForeignKey("ChildId");

                    b.HasOne("CelebrationRegister.Data.Entities.DetailTitle", "DetailTitle")
                        .WithMany("OptionalDetails")
                        .HasForeignKey("DetailTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CelebrationRegister.Data.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("CelebrationRegister.Data.Entities.ReportCard", null)
                        .WithMany("OptionalDetails")
                        .HasForeignKey("ReportCardId");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.ReportCard", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.Child", "Child")
                        .WithMany("ReportCards")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CelebrationRegister.Data.Entities.Status", "Status")
                        .WithMany("RportStatus")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Role.EmployeeRole", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.Employee", "Employee")
                        .WithMany("EmployeeRoles")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CelebrationRegister.Data.Entities.Role.Role", "Role")
                        .WithMany("EmployeeRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Role.Permission", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.Role.Permission", null)
                        .WithMany("Permissions")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("CelebrationRegister.Data.Entities.Role.PermissionRole", b =>
                {
                    b.HasOne("CelebrationRegister.Data.Entities.Role.Permission", "Permission")
                        .WithMany("PermissionRoles")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CelebrationRegister.Data.Entities.Role.Role", "Role")
                        .WithMany("PermissionRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
