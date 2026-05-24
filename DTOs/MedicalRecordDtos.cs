namespace MedicalRecordService.DTOs;

public record MedicalRecordDto(
    Guid RecordId,
    Guid AppointmentId,
    Guid PatientId,
    Guid DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime ExamDate);

public record CreateMedicalRecordRequest(
    Guid AppointmentId,
    Guid PatientId,
    Guid DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime? ExamDate);

public record UpdateMedicalRecordRequest(
    Guid AppointmentId,
    Guid PatientId,
    Guid DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime ExamDate);
