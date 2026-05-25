namespace MedicalRecordService.Models;

public class MedicalRecord
{
    public string RecordId { get; set; } = string.Empty;
    public string AppointmentId { get; set; } = string.Empty;
    public string PatientId { get; set; } = string.Empty;
    public string DoctorId { get; set; } = string.Empty;
    public string Symptoms { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string? DoctorNotes { get; set; }
    public DateTime ExamDate { get; set; } = DateTime.UtcNow;

    public Patient Patient { get; set; } = null!;
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
