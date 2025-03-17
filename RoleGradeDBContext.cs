using Microsoft.EntityFrameworkCore;
using RolesGrade.Models;

namespace YourNamespace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Role> Role { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Employee> Employee121 { get; set; }
        public DbSet<Department> DEPARTMENT1234 { get; set; }
        public DbSet<Salary> SALARY { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>()
    .ToTable("DEPARTMENT1234");  // Updated table name

            modelBuilder.Entity<Salary>()
                .ToTable("SALARY");  // Ensure it's pointing to the correct table


            // Configure Employee Id as auto-generated
            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Department to Employee Relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Employee to Salary Relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Salary to Employee Relationship (Bidirectional setup)
            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithOne(e => e.Salary)
                .HasForeignKey<Salary>(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
