using MedicalRecordService.DTOs;
using MedicalRecordService.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MedicalRecordService.Swagger;

public class SwaggerExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(PrescriptionStatus))
        {
            schema.Type = "string";
            schema.Format = null;
            schema.Enum = new List<IOpenApiAny>
            {
                new OpenApiString(nameof(PrescriptionStatus.Pending)),
                new OpenApiString(nameof(PrescriptionStatus.Dispensed))
            };
            schema.Example = new OpenApiString(nameof(PrescriptionStatus.Pending));
            return;
        }

        schema.Example = context.Type.Name switch
        {
            nameof(PatientDto) => PatientExample(),
            nameof(CreatePatientRequest) => CreatePatientExample(),
            nameof(UpdatePatientRequest) => CreatePatientExample(),
            nameof(MedicalRecordDto) => MedicalRecordExample(),
            nameof(CreateMedicalRecordRequest) => CreateMedicalRecordExample(),
            nameof(UpdateMedicalRecordRequest) => CreateMedicalRecordExample(),
            nameof(PrescriptionDto) => PrescriptionExample(),
            nameof(CreatePrescriptionRequest) => CreatePrescriptionExample(),
            nameof(UpdatePrescriptionRequest) => CreatePrescriptionExample(),
            nameof(PrescriptionItemDto) => PrescriptionItemExample(),
            nameof(CreatePrescriptionItemRequest) => CreatePrescriptionItemExample(),
            nameof(UpdatePrescriptionItemRequest) => CreatePrescriptionItemExample(),
            _ => schema.Example
        };
    }

    private static OpenApiObject PatientExample()
    {
        return new OpenApiObject
        {
            ["patientId"] = new OpenApiInteger(1),
            ["fullName"] = new OpenApiString("Nguyễn Minh An"),
            ["dateOfBirth"] = new OpenApiString("1990-05-12T00:00:00Z"),
            ["gender"] = new OpenApiString("Nam"),
            ["phone"] = new OpenApiString("0901234567"),
            ["medicalHistory"] = new OpenApiString("Tăng huyết áp"),
            ["createdAt"] = new OpenApiString("2026-05-25T09:00:00Z")
        };
    }

    private static OpenApiObject CreatePatientExample()
    {
        return new OpenApiObject
        {
            ["fullName"] = new OpenApiString("Trần Thu Hà"),
            ["dateOfBirth"] = new OpenApiString("1986-09-23T00:00:00Z"),
            ["gender"] = new OpenApiString("Nữ"),
            ["phone"] = new OpenApiString("0912345678"),
            ["medicalHistory"] = new OpenApiString("Dị ứng thời tiết")
        };
    }

    private static OpenApiObject MedicalRecordExample()
    {
        return new OpenApiObject
        {
            ["recordId"] = new OpenApiInteger(1),
            ["appointmentId"] = new OpenApiInteger(101),
            ["patientId"] = new OpenApiInteger(1),
            ["doctorId"] = new OpenApiInteger(201),
            ["symptoms"] = new OpenApiString("Đau đầu, huyết áp cao"),
            ["diagnosis"] = new OpenApiString("Tăng huyết áp độ 1"),
            ["doctorNotes"] = new OpenApiString("Theo dõi huyết áp mỗi ngày."),
            ["examDate"] = new OpenApiString("2026-05-25T09:30:00Z")
        };
    }

    private static OpenApiObject CreateMedicalRecordExample()
    {
        return new OpenApiObject
        {
            ["appointmentId"] = new OpenApiInteger(101),
            ["patientId"] = new OpenApiInteger(1),
            ["doctorId"] = new OpenApiInteger(201),
            ["symptoms"] = new OpenApiString("Đau đầu, huyết áp cao"),
            ["diagnosis"] = new OpenApiString("Tăng huyết áp độ 1"),
            ["doctorNotes"] = new OpenApiString("Theo dõi huyết áp mỗi ngày."),
            ["examDate"] = new OpenApiString("2026-05-25T09:30:00Z")
        };
    }

    private static OpenApiObject PrescriptionExample()
    {
        return new OpenApiObject
        {
            ["prescriptionId"] = new OpenApiInteger(1),
            ["recordId"] = new OpenApiInteger(1),
            ["issuedAt"] = new OpenApiString("2026-05-25T10:00:00Z"),
            ["status"] = new OpenApiString(nameof(PrescriptionStatus.Pending))
        };
    }

    private static OpenApiObject CreatePrescriptionExample()
    {
        return new OpenApiObject
        {
            ["recordId"] = new OpenApiInteger(1),
            ["status"] = new OpenApiString(nameof(PrescriptionStatus.Pending)),
            ["issuedAt"] = new OpenApiString("2026-05-25T10:00:00Z")
        };
    }

    private static OpenApiObject PrescriptionItemExample()
    {
        return new OpenApiObject
        {
            ["prescriptionItemId"] = new OpenApiInteger(1),
            ["prescriptionId"] = new OpenApiInteger(1),
            ["medicineId"] = new OpenApiInteger(401),
            ["medicineName"] = new OpenApiString("Amlodipine 5mg"),
            ["quantity"] = new OpenApiInteger(30),
            ["dosage"] = new OpenApiString("1 viên mỗi sáng")
        };
    }

    private static OpenApiObject CreatePrescriptionItemExample()
    {
        return new OpenApiObject
        {
            ["prescriptionId"] = new OpenApiInteger(1),
            ["medicineId"] = new OpenApiInteger(401),
            ["medicineName"] = new OpenApiString("Amlodipine 5mg"),
            ["quantity"] = new OpenApiInteger(30),
            ["dosage"] = new OpenApiString("1 viên mỗi sáng")
        };
    }
}
