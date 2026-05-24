namespace MedicalRecordService.DTOs;

public record PrescriptionItemDto(
    Guid PrescriptionItemId,
    Guid PrescriptionId,
    Guid MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);

public record CreatePrescriptionItemRequest(
    Guid PrescriptionId,
    Guid MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);

public record UpdatePrescriptionItemRequest(
    Guid PrescriptionId,
    Guid MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);
