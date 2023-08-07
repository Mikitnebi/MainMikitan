using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Common.Hasher;
using MainMikitan.Database.Features.Common.Command;
using MainMikitan.Database.Features.Common.Query;
using MainMikitan.Database.Features.Customer;
using MainMikitan.Database.Features.Customer.Command;
using MainMikitan.Domain.Interfaces.Common;
using MainMikitan.Domain.Interfaces.Customer;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.ExternalServicesAdapter.Email;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//IConfiguration? configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMediatR(typeof(CustomerRegistrationCommand).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(CustomerEmailSenderRegistrationCommand).Assembly));
//
builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<IEmailSenderCommandRepository, EmailSenderCommandRepository>(); 
builder.Services.AddScoped<IEmailSenderQueryRepository, EmailSenderQueryRepository>();
builder.Services.AddScoped<IEmailLogCommandRepository, EmailLogCommandRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStringsOptions"));
builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection("EmailSenderOptions"));
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
