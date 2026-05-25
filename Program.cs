using System.Text.Json.Serialization;
using MedicalRecordService.Data;
using MedicalRecordService.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString =
    builder.Configuration.GetConnectionString("MedicalRecordDatabase") ??
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MedicalRecordDbContext>(options =>
{
    if (string.IsNullOrWhiteSpace(connectionString) ||
        builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
        options.UseInMemoryDatabase("MedicalRecordServiceDemoDb");
        return;
    }

    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<SwaggerExampleSchemaFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

await DatabaseSeeder.SeedAsync(app.Services);

app.Run();
