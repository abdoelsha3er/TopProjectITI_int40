using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40.AppDBContext
{
    public class DBGProjectITI_Int40 : DbContext
    {
        public DBGProjectITI_Int40()
        {

        }
        public DBGProjectITI_Int40(DbContextOptions<DBGProjectITI_Int40> options) : base(options)
        {
        }
        
        public DbSet<CategorySubject> CategorySubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Government> Governments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Grade> Grades { get; set; }
        // Teachers
        public DbSet<Teacher> Teachers { get; set; }  // 
        public DbSet<TeacherSubjects> TeacherSubjects { get; set; }
        public DbSet<TeacherPhone> TeacherPhones { get; set; }
        public DbSet<TeacherExperience> TeacherExperiences { get; set; }
        public DbSet<TeacherSocialLink> TeacherSocialLinks { get; set; }
        public DbSet<TeacherSchool> TeacherSchools { get; set; }
        public DbSet<TeacherEduction> TeacherEductions { get; set; }

        // Eductional Center
        public DbSet<EductionalCenter> EductionalCenters { get; set; }  //
        public DbSet<EductionalCenterPhone> EductionalCenterPhones { get; set; }
        public DbSet<EductionalCenterSubjects> EductionalCenterSubjects { get; set; }
        public DbSet<EductionalCenterSoicalLink> EductionalCenterSoicalLinks { get; set; }
        public DbSet<EductionalCenterGroup> EductionalCenterGroups { get; set; }

        // Parent
        public DbSet<Parent> Parents { get; set; }

        // Student
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSkill> StudentSkills { get; set; }
        public DbSet<StudentGroup> StudentsGroups { get; set; }


        // use Fluent API for Composit Key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // teacher subjects ( M - M )   
            modelBuilder.Entity<TeacherSubjects>()
                .HasKey(ts => new { ts.TeacherId, ts.SubjectId });
            // Teacher Phones (1 - M)  for unique
            modelBuilder.Entity<TeacherPhone>()
                .HasIndex(tp => tp.TeacherPhoneNumber)
                .IsUnique();
            // Eductional Center (1 - M) Phone  >> make unique
            modelBuilder.Entity<EductionalCenterPhone>()
                .HasIndex(tp => tp.EductionalCenterPhoneNumber)
                .IsUnique();

            //Eductional Center(M - M) with subject
            modelBuilder.Entity<EductionalCenterSubjects>()
                .HasKey(ts => new { ts.SubjectId, ts.EductionalCenterId });

            //EductionalCenterGroup(M - M) with Student
            modelBuilder.Entity<StudentGroup>()
                .HasKey(sg => new { sg.EductionalCenterGroupId, sg.StudentId});

            modelBuilder.Entity<StudentGroup>()
                .HasOne(ecg => ecg.EductionalCenterGroup)
                .WithMany(sg => sg.StudentsGroups)
                .HasForeignKey(ecg => ecg.EductionalCenterGroupId);
            modelBuilder.Entity<StudentGroup>()
                .HasOne(s => s.Student)
                .WithMany(sg => sg.StudentsGroups)
                .HasForeignKey(s => s.StudentId);

        }
    }
}
