using LoanOriginationService.API.Exceptions;
using LoanOriginationService.Application.DTO.Customer;
using LoanOriginationService.Application.DTO.Officer;
using LoanOriginationService.Application.Interfaces;
using LoanOriginationService.Application.Mapper;
using LoanOriginationService.Infrastructure.Data;
using LoanOriginationService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/log-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}") 
    .CreateLogger();


builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull;

        options.JsonSerializerOptions.Converters
            .Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoanRepo, LoanRepo>();
builder.Services.AddScoped<IloanDealsRepo, LoanDealsRepo>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("dbconn")
    ));


builder.Services.AddHttpClient<ScorecardClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ScoreCardAddress"] ?? "https://eligibilityscoreservice-csaucxgkabbvd5d4.canadacentral-01.azurewebsites.net/");
});



builder.Services.AddHttpClient<CustomerClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CustomerServiceAddress"] ?? "https://customerservicee-hdb9g9akdcd9fjam.canadacentral-01.azurewebsites.net/");
});



builder.Services.AddHttpClient<OfficerClient>(client =>
{
    client.BaseAddress = new Uri("https://authservicee-gkefb8d7anfwfwfd.canadacentral-01.azurewebsites.net/");
    client.DefaultRequestVersion = HttpVersion.Version11; 
    client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
    client.Timeout = TimeSpan.FromSeconds(30);
})
.ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        SslProtocols = System.Security.Authentication.SslProtocols.Tls12
    });



builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

Log.Error("Test error log");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseExceptionHandler();  

app.UseSerilogRequestLogging();           

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
