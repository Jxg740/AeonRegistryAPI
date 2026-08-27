

using AeonRegistryAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCustomSwagger();

// get a connection to the database
var connectionString = DataUtility.GetConnectionString(builder.Configuration);

// Configure the context for PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapGet("/api/Welcome", () =>
{
    var response = new
    {
        Message = "Welcome to the Aeon Registry API",
        Version = "1.0.0",
        TimeOnly = DateTime.Now.ToString("T")
    };

       return Results.Ok(response);
}).WithName("WelcomeMessage");

app.Run();

