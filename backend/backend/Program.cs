using backend.Data.DatabaseContext;
using backend.Data.Repositories;
using backend.Logic.Repositories;
using backend.Logic.Services;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

var dbContextFactory = new DbContextFactory();
var dbContext = dbContextFactory.CreateDbContext([builder.Configuration.GetConnectionString("DefaultConnection")]);

builder.Services.AddDbContext<BackendDbContext>(options =>
    options.UseSqlServer(dbContext.Database.GetDbConnection().ConnectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// TODO: Add services



builder.Services.AddControllers();

var app = builder.Build();

// ?? Appliquer automatiquement les migrations au d�marrage de l'application
using (var scope = app.Services.CreateScope())
{
    var dbContextt = scope.ServiceProvider.GetRequiredService<BackendDbContext>();
    dbContextt.Database.Migrate();  // <=== Ex�cute les migrations !
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

app.Run();
