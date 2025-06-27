using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Presistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Fields
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<Question> Questions { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rename Identity tables
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");

                entity.HasOne(entity => entity.Instructor)
                    .WithOne(instructor => instructor.User)
                    .HasForeignKey<Instructor>(instructor => instructor.UserId);

                entity.HasOne(entity => entity.Student)
                    .WithOne(student => student.User)
                    .HasForeignKey<Student>(student => student.UserId);
            });

            // Composite key for StudentExam (Many-to-Many)
            builder.Entity<StudentExam>(entity =>
            {
                entity.HasKey(se => new { se.StudentId, se.ExamId });
                entity.HasOne(se => se.Student)
                    .WithMany(s => s.StudentExams)
                    .HasForeignKey(se => se.StudentId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(se => se.Exam)
                    .WithMany(e => e.StudentExams)
                    .HasForeignKey(se => se.ExamId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Student>(entity =>
            {
                entity.HasMany(student => student.Courses)
                    .WithMany(course => course.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentCourses",
                        j => j
                            .HasOne<Course>()
                            .WithMany()
                            .HasForeignKey("CourseId")
                            .OnDelete(DeleteBehavior.Restrict),

                        j => j
                            .HasOne<Student>()
                            .WithMany()
                            .HasForeignKey("StudentId")
                            .OnDelete(DeleteBehavior.Restrict),

                        j =>
                        {
                            j.HasKey("StudentId", "CourseId");
                            j.ToTable("StudentCourses");
                        });
            });

        }

    }
}
