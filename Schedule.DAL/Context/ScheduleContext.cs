using Microsoft.EntityFrameworkCore;
using Schedule.DAL.Entities;

namespace Schedule.DAL.Context;

public class ScheduleContext : DbContext
{
    public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();
            e.HasIndex(u => u.Username).IsUnique();
            e.Property(u => u.Role).HasConversion<string>();
        });

        modelBuilder.Entity<Teacher>(e =>
        {
            e.HasOne(t => t.User)
             .WithOne(u => u.Teacher)
             .HasForeignKey<Teacher>(t => t.UserId)
             .OnDelete(DeleteBehavior.SetNull);

            e.HasOne(t => t.Department)
             .WithMany(d => d.Teachers)
             .HasForeignKey(t => t.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Group>(e =>
        {
            e.HasOne(g => g.Department)
             .WithMany(d => d.Groups)
             .HasForeignKey(g => g.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Lesson>(e =>
        {
            e.HasOne(l => l.Teacher)
             .WithMany(t => t.Lessons)
             .HasForeignKey(l => l.TeacherId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(l => l.Group)
             .WithMany(g => g.Lessons)
             .HasForeignKey(l => l.GroupId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(l => l.Classroom)
             .WithMany(c => c.Lessons)
             .HasForeignKey(l => l.ClassroomId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(l => l.Discipline)
             .WithMany(d => d.Lessons)
             .HasForeignKey(l => l.DisciplineId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(l => new { l.ClassroomId, l.DayOfWeek, l.LessonNumber, l.IsOddWeek })
             .IsUnique().HasDatabaseName("UQ_Lesson_Classroom_Slot");

            e.HasIndex(l => new { l.TeacherId, l.DayOfWeek, l.LessonNumber, l.IsOddWeek })
             .IsUnique().HasDatabaseName("UQ_Lesson_Teacher_Slot");

            e.HasIndex(l => new { l.GroupId, l.DayOfWeek, l.LessonNumber, l.IsOddWeek })
             .IsUnique().HasDatabaseName("UQ_Lesson_Group_Slot");

            e.Property(l => l.LessonNumber).HasColumnType("tinyint");
        });
    }
}