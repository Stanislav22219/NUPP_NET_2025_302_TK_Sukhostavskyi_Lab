using System;
using System.Linq;
using System.Threading.Tasks;
using VideoGames.Common;
using Xunit;

namespace VideoGames.Tests
{
    public class CrudServiceTests
    {
        private readonly CrudServiceAsync<Game> _gameService;

        public CrudServiceTests()
        {
            _gameService = new CrudServiceAsync<Game>(game => game.Id);
        }

        [Fact]
        public async Task CreateGame_ShouldAddGame()
        {
            var game = Game.CreateNew();

            await _gameService.CreateAsync(game);
            var result = await _gameService.ReadAsync(game.Id);

            Assert.NotNull(result);
            Assert.Equal(game.Id, result.Id);
        }

        [Fact]
        public async Task ReadGame_ShouldReturnCorrectGame()
        {
            var game1 = Game.CreateNew();
            var game2 = Game.CreateNew();

            await _gameService.CreateAsync(game1);
            await _gameService.CreateAsync(game2);

            var result = await _gameService.ReadAsync(game1.Id);

            Assert.NotNull(result);
            Assert.Equal(game1.Id, result.Id);
        }

        [Fact]
        public async Task UpdateGame_ShouldModifyGameData()
        {
            var game = Game.CreateNew();
            await _gameService.CreateAsync(game);

            game.CriticsRating = 9.5; // Оновлюємо рейтинг
            await _gameService.UpdateAsync(game);

            var updatedGame = await _gameService.ReadAsync(game.Id);
            Assert.NotNull(updatedGame);
            Assert.Equal(9.5, updatedGame.CriticsRating);
        }

        [Fact]
        public async Task RemoveGame_ShouldDeleteGame()
        {
            var game = Game.CreateNew();
            await _gameService.CreateAsync(game);
            await _gameService.RemoveAsync(game);

            var result = await _gameService.ReadAsync(game.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task ReadAllGames_ShouldReturnAllGames()
        {
            await _gameService.CreateAsync(Game.CreateNew());
            await _gameService.CreateAsync(Game.CreateNew());

            var games = await _gameService.ReadAllAsync();
            Assert.NotEmpty(games);
        }
    }
}