using Microsoft.EntityFrameworkCore;
using PetriNetEngine.Application;
using PetriNetEngine.Domain.Services;
using PetriNetEngine.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<ValidatePetriNetService, ValidatePetriNetService>();
builder.Services.AddScoped<SimulatePetriNetService, SimulatePetriNetService>();
builder.Services.AddScoped<ModelPetriNetService, ModelPetriNetService>();
builder.Services.AddScoped<IPetriNetRepository, PetriNetRepositoryImpl>();

builder.Services.AddDbContext<PetriNetContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
