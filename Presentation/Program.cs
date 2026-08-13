using Application.Extensions;
using Infrastructure.Extensions;
using Presentation.Endpoints;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnvironmentVariables();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map Endpoints
app.MapContaEndpoints();

app.Run();
