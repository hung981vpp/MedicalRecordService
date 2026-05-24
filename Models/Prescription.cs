namespace MedicalRecordService.Models;

public enum PrescriptionStatus { Pending, Dispensed }

public class Prescription
{
    public Guid PrescriptionId { get; set; } = Guid.NewGuid();
    public Guid RecordId { get; set; }
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Pending;

    public MedicalRecord MedicalRecord { get; set; } = null!;
    public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
}