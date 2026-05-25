using MedicalRecordService.Data;
using MedicalRecordService.DTOs;
using MedicalRecordService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecordService.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly MedicalRecordDbContext _context;

    public PatientsController(MedicalRecordDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
    {
        return await _context.Patients
            .AsNoTracking()
            .OrderByDescending(patient => patient.CreatedAt)
            .Select(patient => ToDto(patient))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDto>> GetPatient(string id)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(patient => patient.PatientId == id);

        return patient is null ? NotFound() : ToDto(patient);
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> CreatePatient(CreatePatientRequest request)
    {
        var patient = new Patient
        {
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Phone = request.Phone,
            MedicalHistory = request.MedicalHistory
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, ToDto(patient));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(string id, UpdatePatientRequest request)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient is null)
        {
            return NotFound();
        }

        patient.FullName = request.FullName;
        patient.DateOfBirth = request.DateOfBirth;
        patient.Gender = request.Gender;
        patient.Phone = request.Phone;
        patient.MedicalHistory = request.MedicalHistory;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(string id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient is null)
        {
            return NotFound();
        }

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static PatientDto ToDto(Patient patient)
    {
        return new PatientDto(
            patient.PatientId,
            patient.FullName,
            patient.DateOfBirth,
            patient.Gender,
            patient.Phone,
            patient.MedicalHistory,
            patient.CreatedAt);
    }
}
