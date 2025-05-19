using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGames.Infrastructure.Models;
using VideoGames.Infrastructure.Repositories;
using VideoGames.Common;

namespace VideoGames.Infrastructure.Services
{
    public class GameService
    {
        private readonly ICrudServiceAsync<GameModel> _gameService;

        public GameService(ICrudServiceAsync<GameModel> gameService)
        {
            _gameService = gameService;
        }

        public async Task<List<GameModel>> GetAllGamesAsync()
        {
            return (await _gameService.ReadAllAsync()).ToList();
        }

        public async Task AddGameAsync(GameModel game)
        {
            await _gameService.CreateAsync(game);
        }
    }
}
