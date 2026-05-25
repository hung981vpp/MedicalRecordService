using MedicalRecordService.Models;

namespace MedicalRecordService.DTOs;

public record PrescriptionDto(
    string PrescriptionId,
    string RecordId,
    DateTime IssuedAt,
    PrescriptionStatus Status);

public record CreatePrescriptionRequest(
    string RecordId,
    PrescriptionStatus? Status,
    DateTime? IssuedAt);

public record UpdatePrescriptionRequest(
    string RecordId,
    PrescriptionStatus Status,
    DateTime IssuedAt);
