using backend.Data.DatabaseContext;
using backend.Data.Repositories;
using backend.Logic.Repositories;
using backend.Logic.Services;
using backend.Network.Middlewares;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbContextFactory = new DbContextFactory();
var dbContext = dbContextFactory.CreateDbContext([builder.Configuration.GetConnectionString("DefaultConnection")]);

builder.Services.AddDbContext<BackendDbContext>(options =>
    options.UseSqlServer(dbContext.Database.GetDbConnection().ConnectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// TODO: Add services

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
