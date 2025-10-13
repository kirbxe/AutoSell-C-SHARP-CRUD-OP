using System;
using System.Collections.Generic;
using AutoSellCourses.AppData.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSellCourses.AppData;

public partial class AutoSellContext : DbContext
{
    public AutoSellContext()
    {
    }

    public AutoSellContext(DbContextOptions<AutoSellContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientCar> ClientCars { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Diller> Dillers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AutoSell;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK_client_id");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientAddress)
                .HasMaxLength(150)
                .HasColumnName("client_address");
            entity.Property(e => e.ClientLastName)
                .HasMaxLength(50)
                .HasColumnName("client_last_name");
            entity.Property(e => e.ClientMiddleName)
                .HasMaxLength(50)
                .HasColumnName("client_middle_name");
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .HasColumnName("client_name");
            entity.Property(e => e.ClientNumber)
                .HasMaxLength(20)
                .HasColumnName("client_number");
            entity.Property(e => e.ClientTown)
                .HasMaxLength(60)
                .HasColumnName("client_town");
        });

        modelBuilder.Entity<ClientCar>(entity =>
        {
            entity.HasKey(e => e.ClientcarId).HasName("PK__ClientCa__7EBE4B96AC6C9EBA");

            entity.Property(e => e.ClientcarId).HasColumnName("clientcar_id");
            entity.Property(e => e.CarBrand)
                .HasMaxLength(70)
                .HasColumnName("car_brand");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ColorCar)
                .HasMaxLength(50)
                .HasColumnName("color_car");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.EngineCapacity)
                .HasMaxLength(50)
                .HasColumnName("engine_capacity");
            entity.Property(e => e.ManufactureDate).HasColumnName("manufacture_date");
            entity.Property(e => e.Mileage).HasColumnName("mileage");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.TransmissionType)
                .HasMaxLength(20)
                .HasColumnName("transmission_type");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientCars)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientsCar");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__F8D664230A6CED64");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.CarBrand)
                .HasMaxLength(50)
                .HasColumnName("car_brand");
            entity.Property(e => e.CarPhoto).HasColumnName("car_photo");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Commission)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("commission");
            entity.Property(e => e.ContractDate).HasColumnName("contract_date");
            entity.Property(e => e.DillerId).HasColumnName("diller_id");
            entity.Property(e => e.ManufactureDate).HasColumnName("manufacture_date");
            entity.Property(e => e.Mileage).HasColumnName("mileage");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sale_price");

            entity.HasOne(d => d.Client).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Contracts_Clients");

            entity.HasOne(d => d.Diller).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.DillerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contracts_Dillers");
        });

        modelBuilder.Entity<Diller>(entity =>
        {
            entity.HasKey(e => e.DillersId).HasName("PK_dillers_id");

            entity.Property(e => e.DillersId).HasColumnName("dillers_id");
            entity.Property(e => e.DillersAddress)
                .HasMaxLength(150)
                .HasColumnName("dillers_address");
            entity.Property(e => e.DillersLastName)
                .HasMaxLength(50)
                .HasColumnName("dillers_last_name");
            entity.Property(e => e.DillersMiddleName)
                .HasMaxLength(50)
                .HasColumnName("dillers_middle_name");
            entity.Property(e => e.DillersName)
                .HasMaxLength(50)
                .HasColumnName("dillers_name");
            entity.Property(e => e.DillersNumber)
                .HasMaxLength(20)
                .HasColumnName("dillers_number");
            entity.Property(e => e.DillersTown)
                .HasMaxLength(60)
                .HasColumnName("dillers_town");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
