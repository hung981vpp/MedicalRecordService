namespace MedicalRecordService.Models;

public class PrescriptionItem
{
    public Guid PrescriptionItemId { get; set; } = Guid.NewGuid();
    public Guid PrescriptionId { get; set; }
    public Guid MedicineId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Dosage { get; set; } = string.Empty;

    public Prescription Prescription { get; set; } = null!;
}