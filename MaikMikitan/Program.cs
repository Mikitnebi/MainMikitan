using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.InternalServiceAdapterService;
using MainMikitan.Database;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.ExternalServicesAdapter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using MainMikitan.Application;
using MainMikitan.Domain;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon;
using AutoMapper;
using MainMikitan.Cache;
using MainMikitan.Database.DbContext;
using MainMikitan.InternalServicesAdapter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(options => builder.Configuration.GetSection("JwtOptions").Bind(options));

var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

builder.Services.AddCors(sa => sa.AddDefaultPolicy((p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod())));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidAudience = jwtOptions.Audience,
            ValidIssuer = jwtOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSecurityKey)),
        };
    });
// Add services to the container.
//IConfiguration? configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MikDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration["ConnectionStringsOptions:MainMik"]));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MainMikitan.API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStringsOptions"));
builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection("EmailSenderOptions"));
builder.Services.Configure<OtpOptions>(builder.Configuration.GetSection("OtpOptions"));
builder.Services.Configure<SecurityOptions>(builder.Configuration.GetSection("SecurityOptions"));
builder.Services.AddMainMikitanDatabase();
builder.Services.AddMainMikitanCache();
builder.Services.AddMainMikitanInternalService();
builder.Services.AddMainMikitanExternalService();
builder.Services.AddMainMikitanApplication();
builder.Services.AddAWSService<IAmazonS3>(new AWSOptions
{
    Credentials = new BasicAWSCredentials("AKIASI7N5JDUHAB7VXEZ", "Rq3/D+4o1+bLCcgMnlKD+DVUIog9W02mi2LbKj3s"),
    Region = RegionEndpoint.EUCentral1, // Replace with your AWS region
});
var app = builder.Build();
app.UseCors(x =>
    x.WithOrigins([
        "http://127.0.0.1:5173", "http://127.0.0.1:5174", "http://213.200.15.25:5173", "http://213.200.15.25:5174", "http://192.168.100.62:5173"
    ]).AllowCredentials().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
