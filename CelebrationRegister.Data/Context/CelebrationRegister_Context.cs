using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.DynamicSettings;
using CelebrationRegister.Data.Entities.Role;
using Microsoft.EntityFrameworkCore;

namespace CelebrationRegister.Data.Context
{
    public class CelebrationRegister_Context:DbContext
    {
        public CelebrationRegister_Context(DbContextOptions<CelebrationRegister_Context> options):base(options)
        {
            
        }

        public DbSet<Notification> Notfications { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<ReportCard> ReportCards { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<OptionalDetails> OptionalDetails { get; set; }
        public DbSet<DetailTitle> DetailTitles { get; set; }

        #region Role

        public DbSet<Role> Roles { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }


        #endregion

        #region Settings

        public DbSet<Setting> Settings { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => !e.IsDelete);

            modelBuilder.Entity<Grade>()
                .HasQueryFilter(g => !g.IsDelete);
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
