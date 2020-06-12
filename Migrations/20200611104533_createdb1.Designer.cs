﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopProjectITI_int40.AppDBContext;

namespace TopProjectITI_int40.Migrations
{
    [DbContext(typeof(DBGProjectITI_Int40))]
    [Migration("20200611104533_createdb1")]
    partial class createdb1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TopProjectITI_int40.Models.CategorySubject", b =>
                {
                    b.Property<int>("CategorySubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CategorySubjectId");

                    b.ToTable("CategorySubjects");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GovernmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CityId");

                    b.HasIndex("GovernmentId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.EductionalCenter", b =>
                {
                    b.Property<int>("EductionalCenterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovernmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EductionalCenterId");

                    b.ToTable("EductionalCenters");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.EductionalCenterPhone", b =>
                {
                    b.Property<int>("EductionalCenterPhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EductionalCenterId")
                        .HasColumnType("int");

                    b.Property<string>("EductionalCenterPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EductionalCenterPhoneId");

                    b.HasIndex("EductionalCenterId");

                    b.HasIndex("EductionalCenterPhoneNumber")
                        .IsUnique();

                    b.ToTable("EductionalCenterPhones");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.EductionalCenterSoicalLink", b =>
                {
                    b.Property<int>("EductionalCenterSoicalLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EductionalCenterId")
                        .HasColumnType("int");

                    b.Property<string>("LinkAddess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EductionalCenterSoicalLinkId");

                    b.HasIndex("EductionalCenterId");

                    b.ToTable("EductionalCenterSoicalLinks");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.EductionalCenterSubjects", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("EductionalCenterId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId", "EductionalCenterId");

                    b.HasIndex("EductionalCenterId");

                    b.ToTable("EductionalCenterSubjects");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.Government", b =>
                {
                    b.Property<int>("GovernmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("GovernmentId");

                    b.ToTable("Governments");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategorySubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("SubjectId");

                    b.HasIndex("CategorySubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("AddressDetails")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherEduction", b =>
                {
                    b.Property<int>("TeacherEductionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EductionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("TeacherEductionId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherEductions");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherExperience", b =>
                {
                    b.Property<int>("TeacherExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("TeacherExperienceId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherExperiences");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherPhone", b =>
                {
                    b.Property<int>("TeacherPhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("TeacherPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TeacherPhoneId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("TeacherPhoneNumber")
                        .IsUnique();

                    b.ToTable("TeacherPhones");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherSchool", b =>
                {
                    b.Property<int>("TeacherSchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("TeacherSchoolId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherSchools");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherSocialLink", b =>
                {
                    b.Property<int>("TeacherSocialLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LinkAddess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("TeacherSocialLinkId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherSocialLinks");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherSubjects", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("TeacherSubjects");
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.City", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.Government", "Government")
                        .WithMany("Cities")
                        .HasForeignKey("GovernmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.EductionalCenterPhone", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.EductionalCenter", "EductionalCenter")
                        .WithMany("EductionalCenterPhones")
                        .HasForeignKey("EductionalCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.EductionalCenterSoicalLink", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.EductionalCenter", "EductionalCenter")
                        .WithMany("EductionalCenterSoicalLinks")
                        .HasForeignKey("EductionalCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.EductionalCenterSubjects", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.EductionalCenter", "EductionalCenter")
                        .WithMany("EductionalCenterSubjects")
                        .HasForeignKey("EductionalCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TopProjectITI_int40.Models.Subject", "Subject")
                        .WithMany("EductionalCenterSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.Subject", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.CategorySubject", "CategorySubject")
                        .WithMany("Subjects")
                        .HasForeignKey("CategorySubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherEduction", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.Teacher", "Teacher")
                        .WithMany("TeacherEductions")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherExperience", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.Teacher", "Teacher")
                        .WithMany("TeacherExperiences")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherPhone", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.Teacher", "Teacher")
                        .WithMany("TeacherPhones")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherSchool", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.Teacher", "Teacher")
                        .WithMany("TeacherSchools")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherSocialLink", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.Teacher", "Teacher")
                        .WithMany("TeacherSocialLinks")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopProjectITI_int40.Models.TeacherSubjects", b =>
                {
                    b.HasOne("TopProjectITI_int40.Models.Subject", "Subject")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TopProjectITI_int40.Models.Teacher", "Teacher")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
