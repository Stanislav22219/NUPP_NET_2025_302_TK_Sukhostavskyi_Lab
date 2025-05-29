using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGames.Infrastructure.Models;
using VideoGames.Common;
using System;
using System.Threading.Tasks;

namespace VideoGames.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GamePlayerController : ControllerBase
    {
        private readonly ICrudServiceAsync<GamePlayer> _gamePlayerService;

        public GamePlayerController(ICrudServiceAsync<GamePlayer> gamePlayerService)
        {
            _gamePlayerService = gamePlayerService;
        }

        [Authorize(Roles = "User,Admin,Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gamePlayers = await _gamePlayerService.ReadAllAsync();
            return Ok(gamePlayers);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GamePlayer gamePlayer)
        {
            if (gamePlayer == null) return BadRequest();
            var result = await _gamePlayerService.CreateAsync(gamePlayer);
            return result ? StatusCode(201) : BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpPut("{gameId}/{playerId}")]
        public async Task<IActionResult> Update(Guid gameId, Guid playerId, [FromBody] GamePlayer gamePlayer)
        {
            if (gamePlayer.GameId != gameId || gamePlayer.PlayerId != playerId)
                return BadRequest("GameId або PlayerId не співпадають.");

            var result = await _gamePlayerService.UpdateAsync(gamePlayer);
            return result ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
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