using AutoMapper;
using FluentValidation;
using TestMaxiT;
using TestMaxiT.Models.Data;
using TestMaxiT.Models.Entities;
using TestMaxiT.Profiles;
using TestMaxiT.Repository;
using TestMaxiT.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<MyAppContext>();
builder.Services.AddScoped<IEmployeeRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IBeneficiaryRepository<Beneficiary>, BeneficiaryRepository>();
builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();
builder.Services.AddScoped<IValidator<Beneficiary>, BeneficiaryValidator>();

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
