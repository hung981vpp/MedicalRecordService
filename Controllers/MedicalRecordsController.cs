using MedicalRecordService.Data;
using MedicalRecordService.DTOs;
using MedicalRecordService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecordService.Controllers;

[ApiController]
[Route("api/medical-records")]
public class MedicalRecordsController : ControllerBase
{
    private readonly MedicalRecordDbContext _context;

    public MedicalRecordsController(MedicalRecordDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetMedicalRecords()
    {
        return await _context.MedicalRecords
            .AsNoTracking()
            .OrderByDescending(record => record.ExamDate)
            .Select(record => ToDto(record))
            .ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecord(Guid id)
    {
        var record = await _context.MedicalRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(record => record.RecordId == id);

        return record is null ? NotFound() : ToDto(record);
    }

    [HttpPost]
    public async Task<ActionResult<MedicalRecordDto>> CreateMedicalRecord(CreateMedicalRecordRequest request)
    {
        var patientExists = await _context.Patients.AnyAsync(patient => patient.PatientId == request.PatientId);
        if (!patientExists)
        {
            return BadRequest("PatientId does not exist.");
        }

        var record = new MedicalRecord
        {
            AppointmentId = request.AppointmentId,
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            Symptoms = request.Symptoms,
            Diagnosis = request.Diagnosis,
            DoctorNotes = request.DoctorNotes,
            ExamDate = request.ExamDate ?? DateTime.UtcNow
        };

        _context.MedicalRecords.Add(record);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMedicalRecord), new { id = record.RecordId }, ToDto(record));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMedicalRecord(Guid id, UpdateMedicalRecordRequest request)
    {
        var record = await _context.MedicalRecords.FindAsync(id);
        if (record is null)
        {
            return NotFound();
        }

        var patientExists = await _context.Patients.AnyAsync(patient => patient.PatientId == request.PatientId);
        if (!patientExists)
        {
            return BadRequest("PatientId does not exist.");
        }

        record.AppointmentId = request.AppointmentId;
        record.PatientId = request.PatientId;
        record.DoctorId = request.DoctorId;
        record.Symptoms = request.Symptoms;
        record.Diagnosis = request.Diagnosis;
        record.DoctorNotes = request.DoctorNotes;
        record.ExamDate = request.ExamDate;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMedicalRecord(Guid id)
    {
        var record = await _context.MedicalRecords.FindAsync(id);
        if (record is null)
        {
            return NotFound();
        }

        _context.MedicalRecords.Remove(record);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static MedicalRecordDto ToDto(MedicalRecord record)
    {
        return new MedicalRecordDto(
            record.RecordId,
            record.AppointmentId,
            record.PatientId,
            record.DoctorId,
            record.Symptoms,
            record.Diagnosis,
            record.DoctorNotes,
            record.ExamDate);
    }
}
