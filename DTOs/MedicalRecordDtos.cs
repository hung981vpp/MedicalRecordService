namespace MedicalRecordService.DTOs;

public record MedicalRecordDto(
    int RecordId,
    int AppointmentId,
    int PatientId,
    int DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime ExamDate);

public record CreateMedicalRecordRequest(
    int AppointmentId,
    int PatientId,
    int DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime? ExamDate);

public record UpdateMedicalRecordRequest(
    int AppointmentId,
    int PatientId,
    int DoctorId,
    string Symptoms,
    string Diagnosis,
    string? DoctorNotes,
    DateTime ExamDate);
