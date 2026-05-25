namespace MedicalRecordService.Models;

public enum PrescriptionStatus { Pending, Dispensed }

public class Prescription
{
    public string PrescriptionId { get; set; } = string.Empty;
    public string RecordId { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Pending;

    public MedicalRecord MedicalRecord { get; set; } = null!;
    public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
}
