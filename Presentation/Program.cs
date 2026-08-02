using Infrastructure.Extensions;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnvironmentVariables();

builder.Services.AddOpenApi();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
