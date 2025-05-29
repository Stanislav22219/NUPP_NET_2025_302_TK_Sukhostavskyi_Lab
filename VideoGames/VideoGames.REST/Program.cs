using Microsoft.EntityFrameworkCore;
using VideoGames.Common;
using VideoGames.Infrastructure.Models;
using VideoGames.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VideoGamesContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VideoGamesDB;Trusted_Connection=True;"));

// Реєстрація репозиторію
builder.Services.AddScoped<IRepository<GameModel>, Repository<GameModel>>();
builder.Services.AddScoped<IRepository<GamePlayer>, Repository<GamePlayer>>();

// Реєстрація сервісу CRUD
builder.Services.AddScoped<ICrudServiceAsync<GameModel>, CrudServiceAsync<GameModel>>();
builder.Services.AddScoped<ICrudServiceAsync<GamePlayer>, CrudServiceAsync<GamePlayer>>();

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
