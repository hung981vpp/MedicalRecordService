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

        var patientOne = new Patient
        {
            PatientId = "BN001",
            FullName = "Nguyễn Minh An",
            DateOfBirth = new DateTime(1990, 5, 12),
            Gender = "Nam",
            Phone = "0901234567",
            MedicalHistory = "Tăng huyết áp",
            CreatedAt = DateTime.UtcNow.AddDays(-10)
        };

        var patientTwo = new Patient
        {
            PatientId = "BN002",
            FullName = "Trần Thu Hà",
            DateOfBirth = new DateTime(1986, 9, 23),
            Gender = "Nữ",
            Phone = "0912345678",
            MedicalHistory = "Dị ứng thời tiết",
            CreatedAt = DateTime.UtcNow.AddDays(-7)
        };

        var patientThree = new Patient
        {
            PatientId = "BN003",
            FullName = "Lê Bảo Châu",
            DateOfBirth = new DateTime(2001, 2, 8),
            Gender = "Nữ",
            Phone = "0987654321",
            MedicalHistory = null,
            CreatedAt = DateTime.UtcNow.AddDays(-3)
        };

        var recordOne = new MedicalRecord
        {
            RecordId = "HS001",
            AppointmentId = "LH001",
            Patient = patientOne,
            DoctorId = "BS001",
            Symptoms = "Đau đầu, huyết áp cao",
            Diagnosis = "Tăng huyết áp độ 1",
            DoctorNotes = "Theo dõi huyết áp mỗi ngày.",
            ExamDate = DateTime.UtcNow.AddDays(-5)
        };

        var recordTwo = new MedicalRecord
        {
            RecordId = "HS002",
            AppointmentId = "LH002",
            Patient = patientTwo,
            DoctorId = "BS002",
            Symptoms = "Hắt hơi, sổ mũi",
            Diagnosis = "Viêm mũi dị ứng",
            DoctorNotes = "Hạn chế tiếp xúc với bụi và phấn hoa.",
            ExamDate = DateTime.UtcNow.AddDays(-4)
        };

        var recordThree = new MedicalRecord
        {
            RecordId = "HS003",
            AppointmentId = "LH003",
            Patient = patientThree,
            DoctorId = "BS003",
            Symptoms = "Sốt nhẹ, đau họng",
            Diagnosis = "Viêm họng cấp",
            DoctorNotes = "Nghỉ ngơi và uống nhiều nước.",
            ExamDate = DateTime.UtcNow.AddDays(-2)
        };

        var prescriptionOne = new Prescription
        {
            PrescriptionId = "DT001",
            MedicalRecord = recordOne,
            IssuedAt = DateTime.UtcNow.AddDays(-5),
            Status = PrescriptionStatus.Pending
        };

        var prescriptionTwo = new Prescription
        {
            PrescriptionId = "DT002",
            MedicalRecord = recordTwo,
            IssuedAt = DateTime.UtcNow.AddDays(-4),
            Status = PrescriptionStatus.Dispensed
        };

        var prescriptionThree = new Prescription
        {
            PrescriptionId = "DT003",
            MedicalRecord = recordThree,
            IssuedAt = DateTime.UtcNow.AddDays(-2),
            Status = PrescriptionStatus.Pending
        };

        var prescriptionItems = new[]
        {
            new PrescriptionItem
            {
                PrescriptionItemId = "CT001",
                Prescription = prescriptionOne,
                MedicineId = "TH001",
                MedicineName = "Amlodipine 5mg",
                Quantity = 30,
                Dosage = "1 viên mỗi sáng"
            },
            new PrescriptionItem
            {
                PrescriptionItemId = "CT002",
                Prescription = prescriptionTwo,
                MedicineId = "TH002",
                MedicineName = "Loratadine 10mg",
                Quantity = 10,
                Dosage = "1 viên mỗi ngày"
            },
            new PrescriptionItem
            {
                PrescriptionItemId = "CT003",
                Prescription = prescriptionThree,
                MedicineId = "TH003",
                MedicineName = "Paracetamol 500mg",
                Quantity = 12,
                Dosage = "1 viên khi sốt"
            },
            new PrescriptionItem
            {
                PrescriptionItemId = "CT004",
                Prescription = prescriptionThree,
                MedicineId = "TH004",
                MedicineName = "Vitamin C 500mg",
                Quantity = 10,
                Dosage = "1 viên mỗi ngày"
            }
        };

        context.Patients.AddRange(patientOne, patientTwo, patientThree);
        context.MedicalRecords.AddRange(recordOne, recordTwo, recordThree);
        context.Prescriptions.AddRange(prescriptionOne, prescriptionTwo, prescriptionThree);
        context.PrescriptionItems.AddRange(prescriptionItems);

        await context.SaveChangesAsync();
    }
}
