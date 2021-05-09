using CodeFirstProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirstProject
{
    class CodeFirstDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<PerformerNickName> PerformerNickNames { get; set; }

        public CodeFirstDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=laptop-o5fg9v0m\\sqlexpress;Database=CodeFirstDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasNoKey();

            modelBuilder.Entity<Song>()
                .HasNoKey();

            modelBuilder.Entity<PerformerNickName>()
                .HasNoKey();
        }
    }
}