using Microsoft.OpenApi.Models;
using MyApi.Extensions;
using MyApi.Utils;
using MyWallet.Application.Interfaces;
using MyWallet.Application.Service;
using MyWallet.Application.Services;
using MyWallet.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuring Automapper
builder.Services.AddAutoMapper(x => x.AddMaps(typeof(Program).Assembly));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyWallet API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configure DataContext
builder.Services.ConfigureDatabaseContext(builder.Configuration);

// Configure Authentication
builder.Services.ConfigureAuthentication(builder.Configuration);

// Registering Services and Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

//builder.Services.AddScoped<DebtInService>();

builder.Services.AddScoped<IAppSettingsProvider, AppSettingsProvider>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
