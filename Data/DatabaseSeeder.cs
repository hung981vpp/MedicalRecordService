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
            FullName = "Nguyễn Minh An",
            DateOfBirth = new DateTime(1990, 5, 12),
            Gender = "Nam",
            Phone = "0901234567",
            MedicalHistory = "Tăng huyết áp",
            CreatedAt = DateTime.UtcNow.AddDays(-10)
        };

        var patientTwo = new Patient
        {
            FullName = "Trần Thu Hà",
            DateOfBirth = new DateTime(1986, 9, 23),
            Gender = "Nữ",
            Phone = "0912345678",
            MedicalHistory = "Dị ứng thời tiết",
            CreatedAt = DateTime.UtcNow.AddDays(-7)
        };

        var patientThree = new Patient
        {
            FullName = "Lê Bảo Châu",
            DateOfBirth = new DateTime(2001, 2, 8),
            Gender = "Nữ",
            Phone = "0987654321",
            MedicalHistory = null,
            CreatedAt = DateTime.UtcNow.AddDays(-3)
        };

        var recordOne = new MedicalRecord
        {
            AppointmentId = 101,
            Patient = patientOne,
            DoctorId = 201,
            Symptoms = "Đau đầu, huyết áp cao",
            Diagnosis = "Tăng huyết áp độ 1",
            DoctorNotes = "Theo dõi huyết áp mỗi ngày.",
            ExamDate = DateTime.UtcNow.AddDays(-5)
        };

        var recordTwo = new MedicalRecord
        {
            AppointmentId = 102,
            Patient = patientTwo,
            DoctorId = 202,
            Symptoms = "Hắt hơi, sổ mũi",
            Diagnosis = "Viêm mũi dị ứng",
            DoctorNotes = "Hạn chế tiếp xúc với bụi và phấn hoa.",
            ExamDate = DateTime.UtcNow.AddDays(-4)
        };

        var recordThree = new MedicalRecord
        {
            AppointmentId = 103,
            Patient = patientThree,
            DoctorId = 203,
            Symptoms = "Sốt nhẹ, đau họng",
            Diagnosis = "Viêm họng cấp",
            DoctorNotes = "Nghỉ ngơi và uống nhiều nước.",
            ExamDate = DateTime.UtcNow.AddDays(-2)
        };

        var prescriptionOne = new Prescription
        {
            MedicalRecord = recordOne,
            IssuedAt = DateTime.UtcNow.AddDays(-5),
            Status = PrescriptionStatus.Pending
        };

        var prescriptionTwo = new Prescription
        {
            MedicalRecord = recordTwo,
            IssuedAt = DateTime.UtcNow.AddDays(-4),
            Status = PrescriptionStatus.Dispensed
        };

        var prescriptionThree = new Prescription
        {
            MedicalRecord = recordThree,
            IssuedAt = DateTime.UtcNow.AddDays(-2),
            Status = PrescriptionStatus.Pending
        };

        var prescriptionItems = new[]
        {
            new PrescriptionItem
            {
                Prescription = prescriptionOne,
                MedicineId = 401,
                MedicineName = "Amlodipine 5mg",
                Quantity = 30,
                Dosage = "1 viên mỗi sáng"
            },
            new PrescriptionItem
            {
                Prescription = prescriptionTwo,
                MedicineId = 402,
                MedicineName = "Loratadine 10mg",
                Quantity = 10,
                Dosage = "1 viên mỗi ngày"
            },
            new PrescriptionItem
            {
                Prescription = prescriptionThree,
                MedicineId = 403,
                MedicineName = "Paracetamol 500mg",
                Quantity = 12,
                Dosage = "1 viên khi sốt"
            },
            new PrescriptionItem
            {
                Prescription = prescriptionThree,
                MedicineId = 404,
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
