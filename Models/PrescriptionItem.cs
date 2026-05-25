namespace MedicalRecordService.Models;

public class PrescriptionItem
{
    public string PrescriptionItemId { get; set; } = string.Empty;
    public string PrescriptionId { get; set; } = string.Empty;
    public string MedicineId { get; set; } = string.Empty;
    public string MedicineName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Dosage { get; set; } = string.Empty;

    public Prescription Prescription { get; set; } = null!;
}
