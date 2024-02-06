using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/people-minimalapi", ([FromForm] Person person, [FromForm] Address address)
    => TypedResults.NoContent())
.WithOpenApi();

app.MapControllers();

app.Run();

public record class Person(string FirstName, string LastName);

public record class Address(string Street, string City, string State, string ZipCode);