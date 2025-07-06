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
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // علاقات Instructor و Student
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(u => u.Instructor)
                    .WithOne(i => i.User)
                    .HasForeignKey<Instructor>(i => i.UserId);

                entity.HasOne(u => u.Student)
                    .WithOne(s => s.User)
                    .HasForeignKey<Student>(s => s.UserId);
            });

            // علاقات StudentExam
            builder.Entity<StudentExam>(entity =>
            {
                entity.HasKey(se => new { se.StudentId, se.ExamId });

                entity.HasOne(se => se.Student)
                    .WithMany(s => s.StudentExams)
                    .HasForeignKey(se => se.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(se => se.Exam)
                    .WithMany(e => e.StudentExams)
                    .HasForeignKey(se => se.ExamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Many-to-Many Student <-> Course
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
