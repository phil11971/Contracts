using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<StageContract> StageContract { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StageContract>().HasKey(c => new { c.ContractId, c.StageId });

            modelBuilder.Entity<StageContract>()
            .HasOne(sc => sc.Contract)
            .WithMany(s => s.StageContracts)
            .HasForeignKey(sc => sc.ContractId);

            modelBuilder.Entity<StageContract>()
                .HasOne(sc => sc.Stage)
                .WithMany(c => c.StageContracts)
                .HasForeignKey(sc => sc.StageId);
        }
    }
}
