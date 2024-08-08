using Microsoft.EntityFrameworkCore;
using Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.phone)
                .IsUnique();
            modelBuilder.Entity<Users>()
           .HasIndex(u => u.Username)
           .IsUnique();


            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Subjects>().HasQueryFilter(s => !s.isDeleted ?? false);

        //    modelBuilder.Entity<DaysOfWeek>().HasData(
        //    new DaysOfWeek { DayId = 1, DayName = "Sunday" },
        //    new DaysOfWeek { DayId = 2, DayName = "Monday" },
        //    new DaysOfWeek { DayId = 3, DayName = "Tuesday" },
        //    new DaysOfWeek { DayId = 4, DayName = "Wednesday" },
        //    new DaysOfWeek { DayId = 5, DayName = "Thursday" }
        //);
            
        //    modelBuilder.Entity<Subjects>().HasData(
        //        new Subjects { Id = 1, Name = "History", AcademicYear = 2023 },
        //        new Subjects { Id = 2, Name = "English", AcademicYear = 2023 },
        //        new Subjects { Id = 3, Name = "Math", AcademicYear = 2023 },
        //        new Subjects { Id = 4, Name = "Science", AcademicYear = 2023 },
        //        new Subjects { Id = 5, Name = "Arabic", AcademicYear = 2023 }
        //    );
        //    modelBuilder.Entity<Roles>().HasData(
        //        new Roles {id=1, Name = "Admin", code = "ADMIN" },
        //        new Roles {id=2, Name = "Teacher", code = "TEACHER" },
        //        new Roles { id=3,Name = "Student", code = "STUDENT" }
        //    );
        //    modelBuilder.Entity<Pages>().HasData(
        //          new Pages { id = 1 ,name = "Roles", path = "/pages/lookup/roles" },
        //          new Pages { id = 2 ,name = "Subjects", path = "/pages/lookup/subjects" },
        //          new Pages { id = 3, name = "Pages", path = "/pages/lookup/pages" }
        //      );
        }

        public DbSet<Users> users { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Subjects> subjects { get; set; }
       
        
        public DbSet<TeacherSubjects> TeacherSubjects { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<RolePage> rolepage { get; set; }
        public DbSet<Pages> pages { get; set; }
        public DbSet<DaysOfWeek> DaysOfWeek{ get; set; }

        public DbSet<Class> Classes { get; set; }
   
      
        public DbSet<ClassSchedule> ClassSchedules { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Users>().ToTable("users");
        //}
    }
}
