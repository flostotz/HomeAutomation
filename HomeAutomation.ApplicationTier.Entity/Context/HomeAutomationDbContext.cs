using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;
using Microsoft.EntityFrameworkCore;

namespace HomeAutomation.ApplicationTier.Entity.Context;

public partial class HomeAutomationDbContext : DbContext
{
    public HomeAutomationDbContext()
    {
    }

    public HomeAutomationDbContext(DbContextOptions<HomeAutomationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<DeviceValueHistory> DeviceValueHistories { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=HomeAutomationDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Building__3214EC0751CFC14B");

            entity.ToTable("Building");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Housenumber).HasMaxLength(5);
            entity.Property(e => e.ZipCode).HasMaxLength(10);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Device__3214EC07D5853B29");

            entity.ToTable("Device");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Device)
                .HasForeignKey<Device>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_DeviceType");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeviceTy__3214EC07A3042A6F");

            entity.ToTable("DeviceType");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DeviceValueHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeviceVa__3214EC076F17C0A8");

            entity.ToTable("DeviceValueHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.DeviceValueHistory)
                .HasForeignKey<DeviceValueHistory>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeviceValueHistory_Device");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3214EC077FD1A548");

            entity.ToTable("Room");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Room)
                .HasForeignKey<Room>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Room_Building");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
