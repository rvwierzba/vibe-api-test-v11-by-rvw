using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VibeApiTestV11ByRvw.Services;
using VibeApiTestV11ByRvw.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços ao contêiner.
builder.Services.AddControllers();

builder.Services.AddSingleton<IKmlService, KmlService>();
builder.Services.AddTransient<IFilterValidator, FilterValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure o pipeline de solicitações HTTP.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
