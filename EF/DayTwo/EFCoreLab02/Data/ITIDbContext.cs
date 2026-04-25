using Microsoft.EntityFrameworkCore;
using EFCoreLab02.Models;

namespace EFCoreLab02.Data
{
    public class ITIDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<StudCourse> StudCourses => Set<StudCourse>();
        public DbSet<InsCourse> InsCourses => Set<InsCourse>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=SARA-ELGHAZALLY\\MSSSQLSERVER;Database=ITI_DB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            // Primary Keys
            modelBuilder.Entity<Course>().HasKey(c => c.Crs_Id);
            modelBuilder.Entity<Student>().HasKey(s => s.St_Id);
            modelBuilder.Entity<Instructor>().HasKey(i => i.Ins_Id);
            modelBuilder.Entity<Department>().HasKey(d => d.Dept_Id);
            modelBuilder.Entity<Topic>().HasKey(t => t.Top_Id);

        
            // Composite Keys
            modelBuilder.Entity<StudCourse>()
                .HasKey(sc => new { sc.Crs_Id, sc.St_Id });

            modelBuilder.Entity<InsCourse>()
                .HasKey(ic => new { ic.Ins_Id, ic.Crs_Id });

         
            // Relationships
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Topic)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.Top_Id);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.Dept_Id);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.Dept_Id);

    
            modelBuilder.Entity<StudCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudCourses)
                .HasForeignKey(sc => sc.St_Id);

            modelBuilder.Entity<StudCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudCourses)
                .HasForeignKey(sc => sc.Crs_Id);

            modelBuilder.Entity<InsCourse>()
                .HasOne(ic => ic.Instructor)
                .WithMany(i => i.InsCourses)
                .HasForeignKey(ic => ic.Ins_Id);

            modelBuilder.Entity<InsCourse>()
                .HasOne(ic => ic.Course)
                .WithMany(c => c.InsCourses)
                .HasForeignKey(ic => ic.Crs_Id);
        }
    }
}