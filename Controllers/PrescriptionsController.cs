using MedicalRecordService.Data;
using MedicalRecordService.DTOs;
using MedicalRecordService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecordService.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionsController : ControllerBase
{
    private readonly MedicalRecordDbContext _context;

    public PrescriptionsController(MedicalRecordDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetPrescriptions()
    {
        return await _context.Prescriptions
            .AsNoTracking()
            .OrderByDescending(prescription => prescription.IssuedAt)
            .Select(prescription => ToDto(prescription))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrescriptionDto>> GetPrescription(string id)
    {
        var prescription = await _context.Prescriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(prescription => prescription.PrescriptionId == id);

        return prescription is null ? NotFound() : ToDto(prescription);
    }

    [HttpPost]
    public async Task<ActionResult<PrescriptionDto>> CreatePrescription(CreatePrescriptionRequest request)
    {
        var recordExists = await _context.MedicalRecords.AnyAsync(record => record.RecordId == request.RecordId);
        if (!recordExists)
        {
            return BadRequest("RecordId does not exist.");
        }

        var prescription = new Prescription
        {
            RecordId = request.RecordId,
            Status = request.Status ?? PrescriptionStatus.Pending,
            IssuedAt = request.IssuedAt ?? DateTime.UtcNow
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrescription), new { id = prescription.PrescriptionId }, ToDto(prescription));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrescription(string id, UpdatePrescriptionRequest request)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        if (prescription is null)
        {
            return NotFound();
        }

        var recordExists = await _context.MedicalRecords.AnyAsync(record => record.RecordId == request.RecordId);
        if (!recordExists)
        {
            return BadRequest("RecordId does not exist.");
        }

        prescription.RecordId = request.RecordId;
        prescription.Status = request.Status;
        prescription.IssuedAt = request.IssuedAt;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrescription(string id)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        if (prescription is null)
        {
            return NotFound();
        }

        _context.Prescriptions.Remove(prescription);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static PrescriptionDto ToDto(Prescription prescription)
    {
        return new PrescriptionDto(
            prescription.PrescriptionId,
            prescription.RecordId,
            prescription.IssuedAt,
            prescription.Status);
    }
}
