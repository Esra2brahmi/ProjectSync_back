using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectSync_back.Models;

namespace projectSync_back.data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }

        public DbSet<Project> Projects {get;set;}
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<ProjectSupervisor> ProjectSupervisors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepSupervisor> DepSupervisors { get; set; }
        public DbSet<DepJuryMember> DepJuryMembers { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<JuryMember> JuryMembers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectSupervisor>(x => x.HasKey(p => new {p.ProjectId,p.SupervisorId}));

            modelBuilder.Entity<ProjectSupervisor>()
              .HasOne(u => u.Project)
              .WithMany(u => u.ProjectSupervisors)
              .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectSupervisor>()
              .HasOne(u => u.Supervisor)
              .WithMany(u => u.ProjectSupervisors)
              .HasForeignKey(p => p.SupervisorId);


            modelBuilder.Entity<DepSupervisor>(x => x.HasKey(p => new {p.DepartmentId,p.SupervisorId}));

            modelBuilder.Entity<DepSupervisor>()
              .HasOne(u => u.Department)
              .WithMany(u => u.DepSupervisors)
              .HasForeignKey(p => p.DepartmentId);

            modelBuilder.Entity<DepSupervisor>()
              .HasOne(u => u.Supervisor)
              .WithMany(u => u.DepSupervisors)
              .HasForeignKey(p => p.SupervisorId);

            // Configure cascade delete for Project and ProjectTask relationship
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.ProjectTasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DepJuryMember>(x => x.HasKey(p => new {p.DepartmentId,p.JuryMemberId}));

            modelBuilder.Entity<DepJuryMember>()
              .HasOne(u => u.Department)
              .WithMany(u => u.DepJuryMembers)
              .HasForeignKey(p => p.DepartmentId);

            modelBuilder.Entity<DepJuryMember>()
              .HasOne(u => u.JuryMember)
              .WithMany(u => u.DepJuryMembers)
              .HasForeignKey(p => p.JuryMemberId);

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectReference)
                .IsUnique();
            
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Project)
                .WithOne(p => p.Report)
                .HasForeignKey<Report>(r => r.ProjectId);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.JuryMember)
                .WithMany(j => j.Reports)
                .HasForeignKey(r => r.JuryMemberId);

                
                        }

    }
}