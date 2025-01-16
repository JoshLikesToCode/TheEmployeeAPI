using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TheEmployeeAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();

public partial class Program {}