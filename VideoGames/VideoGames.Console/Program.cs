using System.Threading.Tasks;
using VideoGames.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoGames.Infrastructure.Models;
using VideoGames.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;


namespace VideoGames.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Реєстрація DbContext
            builder.Services.AddDbContext<VideoGamesContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VideoGamesDB;Trusted_Connection=True;"));

            // Реєстрація репозиторію
            builder.Services.AddScoped<IRepository<GameModel>, Repository<GameModel>>();

            // Реєстрація сервісу CRUD
            builder.Services.AddScoped<ICrudServiceAsync<GameModel>, CrudServiceAsync<GameModel>>();
            
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.MapControllers();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }
    }
}
