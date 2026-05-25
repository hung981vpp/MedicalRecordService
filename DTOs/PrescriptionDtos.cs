using MedicalRecordService.Models;

namespace MedicalRecordService.DTOs;

public record PrescriptionDto(
    int PrescriptionId,
    int RecordId,
    DateTime IssuedAt,
    PrescriptionStatus Status);

public record CreatePrescriptionRequest(
    int RecordId,
    PrescriptionStatus? Status,
    DateTime? IssuedAt);

public record UpdatePrescriptionRequest(
    int RecordId,
    PrescriptionStatus Status,
    DateTime IssuedAt);
