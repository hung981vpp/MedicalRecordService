using MedicalRecordService.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecordService.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MedicalRecordDbContext>();

        if (context.Database.IsRelational())
        {
            await context.Database.MigrateAsync();
        }
        else
        {
            await context.Database.EnsureCreatedAsync();
        }

        if (await context.Patients.AnyAsync())
        {
            return;
        }

        var patientOneId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var patientTwoId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var patientThreeId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var recordOneId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var recordTwoId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var recordThreeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

        var prescriptionOneId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        var prescriptionTwoId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        var prescriptionThreeId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");

        var patients = new[]
        {
            new Patient
            {
                PatientId = patientOneId,
                FullName = "Nguyen Van An",
                DateOfBirth = new DateTime(1990, 5, 12),
                Gender = "Male",
                Phone = "0901234567",
                MedicalHistory = "Hypertension",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Patient
            {
                PatientId = patientTwoId,
                FullName = "Tran Thi Binh",
                DateOfBirth = new DateTime(1986, 9, 23),
                Gender = "Female",
                Phone = "0912345678",
                MedicalHistory = "Seasonal allergy",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new Patient
            {
                PatientId = patientThreeId,
                FullName = "Le Minh Chau",
                DateOfBirth = new DateTime(2001, 2, 8),
                Gender = "Female",
                Phone = "0987654321",
                MedicalHistory = null,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        };

        var medicalRecords = new[]
        {
            new MedicalRecord
            {
                RecordId = recordOneId,
                AppointmentId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                PatientId = patientOneId,
                DoctorId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                Symptoms = "Headache and elevated blood pressure",
                Diagnosis = "Stage 1 hypertension",
                DoctorNotes = "Monitor blood pressure twice daily.",
                ExamDate = DateTime.UtcNow.AddDays(-5)
            },
            new MedicalRecord
            {
                RecordId = recordTwoId,
                AppointmentId = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                PatientId = patientTwoId,
                DoctorId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                Symptoms = "Sneezing, runny nose, itchy eyes",
                Diagnosis = "Allergic rhinitis",
                DoctorNotes = "Avoid known allergens and follow up if symptoms persist.",
                ExamDate = DateTime.UtcNow.AddDays(-4)
            },
            new MedicalRecord
            {
                RecordId = recordThreeId,
                AppointmentId = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                PatientId = patientThreeId,
                DoctorId = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                Symptoms = "Fever, sore throat, fatigue",
                Diagnosis = "Acute pharyngitis",
                DoctorNotes = "Rest and drink fluids.",
                ExamDate = DateTime.UtcNow.AddDays(-2)
            }
        };

        var prescriptions = new[]
        {
            new Prescription
            {
                PrescriptionId = prescriptionOneId,
                RecordId = recordOneId,
                IssuedAt = DateTime.UtcNow.AddDays(-5),
                Status = PrescriptionStatus.Pending
            },
            new Prescription
            {
                PrescriptionId = prescriptionTwoId,
                RecordId = recordTwoId,
                IssuedAt = DateTime.UtcNow.AddDays(-4),
                Status = PrescriptionStatus.Dispensed
            },
            new Prescription
            {
                PrescriptionId = prescriptionThreeId,
                RecordId = recordThreeId,
                IssuedAt = DateTime.UtcNow.AddDays(-2),
                Status = PrescriptionStatus.Pending
            }
        };

        var prescriptionItems = new[]
        {
            new PrescriptionItem
            {
                PrescriptionItemId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                PrescriptionId = prescriptionOneId,
                MedicineId = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                MedicineName = "Amlodipine 5mg",
                Quantity = 30,
                Dosage = "1 tablet once daily"
            },
            new PrescriptionItem
            {
                PrescriptionItemId = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                PrescriptionId = prescriptionTwoId,
                MedicineId = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                MedicineName = "Loratadine 10mg",
                Quantity = 10,
                Dosage = "1 tablet once daily"
            },
            new PrescriptionItem
            {
                PrescriptionItemId = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                PrescriptionId = prescriptionThreeId,
                MedicineId = Guid.Parse("40000000-0000-0000-0000-000000000003"),
                MedicineName = "Paracetamol 500mg",
                Quantity = 12,
                Dosage = "1 tablet every 6 hours when feverish"
            },
            new PrescriptionItem
            {
                PrescriptionItemId = Guid.Parse("30000000-0000-0000-0000-000000000004"),
                PrescriptionId = prescriptionThreeId,
                MedicineId = Guid.Parse("40000000-0000-0000-0000-000000000004"),
                MedicineName = "Vitamin C 500mg",
                Quantity = 10,
                Dosage = "1 tablet once daily"
            }
        };

        context.Patients.AddRange(patients);
        context.MedicalRecords.AddRange(medicalRecords);
        context.Prescriptions.AddRange(prescriptions);
        context.PrescriptionItems.AddRange(prescriptionItems);

        await context.SaveChangesAsync();
    }
}
