using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGames.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoGames.Infrastructure;
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

            builder.Services.AddDbContext<VideoGamesContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VideoGamesDB;Trusted_Connection=True;"));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));

            var app = builder.Build();
            app.Run();
        }
    }
}
