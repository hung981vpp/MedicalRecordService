namespace MedicalRecordService.DTOs;

public record PrescriptionItemDto(
    int PrescriptionItemId,
    int PrescriptionId,
    int MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);

public record CreatePrescriptionItemRequest(
    int PrescriptionId,
    int MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);

public record UpdatePrescriptionItemRequest(
    int PrescriptionId,
    int MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);
