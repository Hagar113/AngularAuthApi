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
        }

        public DbSet<Users> users { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Subjects> subjects { get; set; }
       
        public DbSet<StudentSubjects> studentSubjects { get; set; }
        public DbSet<TeacherSubjects> TeacherSubjects { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<RolePage> rolepage { get; set; }
        public DbSet<Pages> pages { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Users>().ToTable("users");
        //}
    }
}
