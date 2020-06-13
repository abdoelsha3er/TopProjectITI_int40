﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Teacher> Teachers { get; set; }  // 
        public DbSet<TeacherSubjects> TeacherSubjects { get; set; }
        public DbSet<TeacherPhone> TeacherPhones { get; set; }
        public DbSet<TeacherExperience> TeacherExperiences { get; set; }
        public DbSet<TeacherSocialLink> TeacherSocialLinks { get; set; }
        public DbSet<TeacherSchool> TeacherSchools { get; set; }
        public DbSet<TeacherEduction> TeacherEductions { get; set; }

        //center
        public DbSet<EductionalCenter> EductionalCenters { get; set; }  //
        public DbSet<EductionalCenterPhone> EductionalCenterPhones { get; set; }
        public DbSet<EductionalCenterSubjects> EductionalCenterSubjects { get; set; }

        public DbSet<EductionalCenterSoicalLink> EductionalCenterSoicalLinks { get; set; }



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
        }
    }
}