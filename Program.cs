using Microsoft.EntityFrameworkCore;
using AsistMedAPI.Data;
using AsistMedAPI.Services; // ⬅️ NECESARIO para que reconozca EvaluadorIA

var builder = WebApplication.CreateBuilder(args);

// Registro de servicios personalizados
builder.Services.AddScoped<EvaluadorIA>(); // ⬅️ REGISTRO FALTANTE

builder.Services.AddDbContext<AsistMedContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
    builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
