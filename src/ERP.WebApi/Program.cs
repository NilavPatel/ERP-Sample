using System.Reflection;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Application.Modules.Employees.Commands;
using ERP.Infrastructure.Data;
using ERP.Infrastructure.Repositories;
using ERP.Infrastructure.Services;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ERPDbContext>(options =>
    options.UseSqlServer("name=ConnectionStrings:ERP",
    x => x.MigrationsAssembly("ERP.DbMigrations")));

builder.Services.AddMediatR(typeof(CreateEmployeeCommand).GetTypeInfo().Assembly);

builder.Services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
    options.Filters.Add<CustomAuthorizeFilter>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = new DataInitializer(scope.ServiceProvider);
    initializer.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
