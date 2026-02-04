using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Forma1.Models;

public partial class Forma1Context : DbContext
{
    public Forma1Context()
    {
    }

    public Forma1Context(DbContextOptions<Forma1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Csapatok> Csapatoks { get; set; }

    public virtual DbSet<Eredmenyek> Eredmenyeks { get; set; }

    public virtual DbSet<Pilotak> Pilotaks { get; set; }

    public virtual DbSet<Versenyek> Versenyeks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=forma1;USER=root;PASSWORD=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Csapatok>(entity =>
        {
            entity.HasKey(e => e.Csazon).HasName("PRIMARY");

            entity.ToTable("csapatok");

            entity.Property(e => e.Csazon)
                .HasColumnType("int(11)")
                .HasColumnName("csazon");
            entity.Property(e => e.Csnev)
                .HasMaxLength(50)
                .HasColumnName("csnev");
        });

        modelBuilder.Entity<Eredmenyek>(entity =>
        {
            entity.HasKey(e => new { e.Pilota, e.Nagydij }).HasName("PRIMARY");

            entity.ToTable("eredmenyek");

            entity.HasIndex(e => e.Nagydij, "nagydij");

            entity.Property(e => e.Pilota)
                .HasColumnType("int(11)")
                .HasColumnName("pilota");
            entity.Property(e => e.Nagydij)
                .HasMaxLength(10)
                .HasColumnName("nagydij");
            entity.Property(e => e.Celpoz)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("celpoz");
            entity.Property(e => e.Startpoz)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("startpoz");

            entity.HasOne(d => d.NagydijNavigation).WithMany(p => p.Eredmenyeks)
                .HasForeignKey(d => d.Nagydij)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eredmenyek_ibfk_2");

            entity.HasOne(d => d.PilotaNavigation).WithMany(p => p.Eredmenyeks)
                .HasForeignKey(d => d.Pilota)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eredmenyek_ibfk_1");
        });

        modelBuilder.Entity<Pilotak>(entity =>
        {
            entity.HasKey(e => e.Pazon).HasName("PRIMARY");

            entity.ToTable("pilotak");

            entity.HasIndex(e => e.Csapat, "csapat");

            entity.Property(e => e.Pazon)
                .HasColumnType("int(11)")
                .HasColumnName("pazon");
            entity.Property(e => e.Csapat)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("csapat");
            entity.Property(e => e.Pnev)
                .HasMaxLength(100)
                .HasColumnName("pnev");
            entity.Property(e => e.Szev)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("szev");

            entity.HasOne(d => d.CsapatNavigation).WithMany(p => p.Pilotaks)
                .HasForeignKey(d => d.Csapat)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pilotak_ibfk_1");
        });

        modelBuilder.Entity<Versenyek>(entity =>
        {
            entity.HasKey(e => e.Vkod).HasName("PRIMARY");

            entity.ToTable("versenyek");

            entity.Property(e => e.Vkod)
                .HasMaxLength(10)
                .HasColumnName("vkod");
            entity.Property(e => e.Datum)
                .HasColumnType("date")
                .HasColumnName("datum");
            entity.Property(e => e.Hely)
                .HasMaxLength(100)
                .HasColumnName("hely");
            entity.Property(e => e.Hossz)
                .HasColumnType("int(11)")
                .HasColumnName("hossz");
            entity.Property(e => e.Kor)
                .HasColumnType("int(11)")
                .HasColumnName("kor");
            entity.Property(e => e.Vnev)
                .HasMaxLength(100)
                .HasColumnName("vnev");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
