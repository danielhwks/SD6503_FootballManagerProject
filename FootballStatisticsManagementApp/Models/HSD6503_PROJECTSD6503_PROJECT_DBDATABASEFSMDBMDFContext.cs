﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FootballStatisticsManagementApp.Models
{
    public partial class HSD6503_PROJECTSD6503_PROJECT_DBDATABASEFSMDBMDFContext : DbContext
    {
        public HSD6503_PROJECTSD6503_PROJECT_DBDATABASEFSMDBMDFContext()
        {
        }

        public HSD6503_PROJECTSD6503_PROJECT_DBDATABASEFSMDBMDFContext(DbContextOptions<HSD6503_PROJECTSD6503_PROJECT_DBDATABASEFSMDBMDFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<League> League { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Stats> Stats { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=H:\\SD6503_PROJECT\\SD6503_PROJECT_DB\\DATABASE\\FSMDB.MDF;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<League>(entity =>
            {
                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.League)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.LeagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeagueMatch");

                entity.HasOne(d => d.Stats)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.StatsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatsMatch");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasColumnName("DOB")
                    .HasMaxLength(35);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamPlayer");
            });

            modelBuilder.Entity<Stats>(entity =>
            {
                entity.HasOne(d => d.MatchNavigation)
                    .WithMany(p => p.StatsNavigation)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchStats");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerStats");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamStats");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
