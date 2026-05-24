namespace MedicalRecordService.Models;

public class Patient
{
    public Guid PatientId { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? MedicalHistory { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}