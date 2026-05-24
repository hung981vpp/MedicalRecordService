using MedicalRecordService.Models;

namespace MedicalRecordService.DTOs;

public record PrescriptionDto(
    Guid PrescriptionId,
    Guid RecordId,
    DateTime IssuedAt,
    PrescriptionStatus Status);

public record CreatePrescriptionRequest(
    Guid RecordId,
    PrescriptionStatus? Status,
    DateTime? IssuedAt);

public record UpdatePrescriptionRequest(
    Guid RecordId,
    PrescriptionStatus Status,
    DateTime IssuedAt);
