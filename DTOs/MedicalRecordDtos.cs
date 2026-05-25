namespace MedicalRecordService.DTOs;

public record MedicalRecordDto(
    string RecordId,
    string AppointmentId,
    string PatientId,
    string DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime ExamDate);

public record CreateMedicalRecordRequest(
    string AppointmentId,
    string PatientId,
    string DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime? ExamDate);

public record UpdateMedicalRecordRequest(
    string AppointmentId,
    string PatientId,
    string DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime ExamDate);
