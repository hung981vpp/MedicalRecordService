namespace MedicalRecordService.DTOs;

public record PrescriptionItemDto(
    string PrescriptionItemId,
    string PrescriptionId,
    string MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);

public record CreatePrescriptionItemRequest(
    string PrescriptionId,
    string MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);

public record UpdatePrescriptionItemRequest(
    string PrescriptionId,
    string MedicineId,
    string MedicineName,
    int Quantity,
    string Dosage);
