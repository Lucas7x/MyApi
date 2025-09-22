using MyApi.Extensions;
using MyApi.Services;
using MyWallet.Application.Interfaces;
using MyWallet.Application.Services;
using MyWallet.Infrastructure.Database;
using MyWallet.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuring Automapper
builder.Services.AddAutoMapper(x => x.AddMaps(typeof(Program).Assembly));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DataContext
builder.Services.ConfigureDatabaseContext(builder.Configuration);

// Registering Services and Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

//builder.Services.AddScoped<DebtInService>();

var app = builder.Build();

app.UseCors(
    options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
);

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
