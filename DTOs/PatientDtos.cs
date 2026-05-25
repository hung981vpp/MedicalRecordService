namespace MedicalRecordService.DTOs;

public record PatientDto(
    int PatientId,
    string FullName,
    DateTime DateOfBirth,
    string Gender,
    string Phone,
    string? MedicalHistory,
    DateTime CreatedAt);

public record CreatePatientRequest(
    string FullName,
    DateTime DateOfBirth,
    string Gender,
    string Phone,
    string? MedicalHistory);

public record UpdatePatientRequest(
    string FullName,
    DateTime DateOfBirth,
    string Gender,
    string Phone,
    string? MedicalHistory);
