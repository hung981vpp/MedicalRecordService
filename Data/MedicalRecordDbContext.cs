using MedicalRecordService.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecordService.Data;

public class MedicalRecordDbContext : DbContext
{
    public MedicalRecordDbContext(DbContextOptions<MedicalRecordDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId);
            entity.Property(e => e.FullName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Gender).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(30).IsRequired();
            entity.Property(e => e.MedicalHistory).HasMaxLength(2000);
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId);
            entity.Property(e => e.Symptoms).HasMaxLength(2000).IsRequired();
            entity.Property(e => e.Diagnosis).HasMaxLength(2000).IsRequired();
            entity.Property(e => e.DoctorNotes).HasMaxLength(2000);

            entity.HasOne(e => e.Patient)
                .WithMany(e => e.MedicalRecords)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId);
            entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(50);

            entity.HasOne(e => e.MedicalRecord)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.RecordId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PrescriptionItem>(entity =>
        {
            entity.HasKey(e => e.PrescriptionItemId);
            entity.Property(e => e.MedicineName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Dosage).HasMaxLength(200).IsRequired();

            entity.HasOne(e => e.Prescription)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
