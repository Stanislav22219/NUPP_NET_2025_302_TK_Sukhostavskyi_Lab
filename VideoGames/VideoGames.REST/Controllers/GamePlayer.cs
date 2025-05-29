using Microsoft.AspNetCore.Mvc;
using VideoGames.Infrastructure.Models;
using VideoGames.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoGames.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlayerController : ControllerBase
    {
        private readonly ICrudServiceAsync<GamePlayer> _gamePlayerService;

        public GamePlayerController(ICrudServiceAsync<GamePlayer> gamePlayerService)
        {
            _gamePlayerService = gamePlayerService;
        }

        // Отримати всіх гравців у грі
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gamePlayers = await _gamePlayerService.ReadAllAsync();
            return Ok(gamePlayers);
        }

        // Отримати конкретного гравця у грі
        [HttpGet("{gameId}/{playerId}")]
        public async Task<IActionResult> Get(Guid gameId, Guid playerId)
        {
            var gamePlayer = await _gamePlayerService.ReadAsync(gameId);
            return gamePlayer != null && gamePlayer.PlayerId == playerId ? Ok(gamePlayer) : NotFound();
        }

        // Додати гравця до гри
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GamePlayer gamePlayer)
        {
            if (gamePlayer == null) return BadRequest();
            var result = await _gamePlayerService.CreateAsync(gamePlayer);
            return result ? StatusCode(201) : BadRequest();
        }

        // Оновити інформацію про гравця у грі
        [HttpPut("{gameId}/{playerId}")]
        public async Task<IActionResult> Update(Guid gameId, Guid playerId, [FromBody] GamePlayer gamePlayer)
        {
            if (gamePlayer.GameId != gameId || gamePlayer.PlayerId != playerId)
                return BadRequest("GameId або PlayerId не співпадають.");

            var result = await _gamePlayerService.UpdateAsync(gamePlayer);
            return result ? NoContent() : NotFound();
        }

        // Видалити гравця з гри
        [HttpDelete("{gameId}/{playerId}")]
        public async Task<IActionResult> Delete(Guid gameId, Guid playerId)
        {
            var gamePlayer = await _gamePlayerService.ReadAsync(gameId);
            if (gamePlayer == null || gamePlayer.PlayerId != playerId) return NotFound();

            var result = await _gamePlayerService.RemoveAsync(gamePlayer);
            return result ? NoContent() : BadRequest();
        }
    }
}