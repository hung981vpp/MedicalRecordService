using MedicalRecordService.Data;
using MedicalRecordService.DTOs;
using MedicalRecordService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecordService.Controllers;

[ApiController]
[Route("api/prescription-items")]
public class PrescriptionItemsController : ControllerBase
{
    private readonly MedicalRecordDbContext _context;

    public PrescriptionItemsController(MedicalRecordDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrescriptionItemDto>>> GetPrescriptionItems()
    {
        return await _context.PrescriptionItems
            .AsNoTracking()
            .OrderBy(item => item.MedicineName)
            .Select(item => ToDto(item))
            .ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PrescriptionItemDto>> GetPrescriptionItem(Guid id)
    {
        var item = await _context.PrescriptionItems
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.PrescriptionItemId == id);

        return item is null ? NotFound() : ToDto(item);
    }

    [HttpPost]
    public async Task<ActionResult<PrescriptionItemDto>> CreatePrescriptionItem(CreatePrescriptionItemRequest request)
    {
        var prescriptionExists = await _context.Prescriptions.AnyAsync(prescription =>
            prescription.PrescriptionId == request.PrescriptionId);
        if (!prescriptionExists)
        {
            return BadRequest("PrescriptionId does not exist.");
        }

        var item = new PrescriptionItem
        {
            PrescriptionId = request.PrescriptionId,
            MedicineId = request.MedicineId,
            MedicineName = request.MedicineName,
            Quantity = request.Quantity,
            Dosage = request.Dosage
        };

        _context.PrescriptionItems.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrescriptionItem), new { id = item.PrescriptionItemId }, ToDto(item));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePrescriptionItem(Guid id, UpdatePrescriptionItemRequest request)
    {
        var item = await _context.PrescriptionItems.FindAsync(id);
        if (item is null)
        {
            return NotFound();
        }

        var prescriptionExists = await _context.Prescriptions.AnyAsync(prescription =>
            prescription.PrescriptionId == request.PrescriptionId);
        if (!prescriptionExists)
        {
            return BadRequest("PrescriptionId does not exist.");
        }

        item.PrescriptionId = request.PrescriptionId;
        item.MedicineId = request.MedicineId;
        item.MedicineName = request.MedicineName;
        item.Quantity = request.Quantity;
        item.Dosage = request.Dosage;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePrescriptionItem(Guid id)
    {
        var item = await _context.PrescriptionItems.FindAsync(id);
        if (item is null)
        {
            return NotFound();
        }

        _context.PrescriptionItems.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static PrescriptionItemDto ToDto(PrescriptionItem item)
    {
        return new PrescriptionItemDto(
            item.PrescriptionItemId,
            item.PrescriptionId,
            item.MedicineId,
            item.MedicineName,
            item.Quantity,
            item.Dosage);
    }
}
