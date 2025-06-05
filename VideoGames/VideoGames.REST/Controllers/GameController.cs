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
    public class GameController : ControllerBase
    {
        private readonly ICrudServiceAsync<GameModel> _gameService;

        public GameController(ICrudServiceAsync<GameModel> gameService)
        {
            _gameService = gameService;
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GameModel game)
        {
            var result = await _gameService.CreateAsync(game);
            return result ? StatusCode(201) : BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            var game = await _gameService.ReadAsync(id);
            return game != null ? Ok(game) : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GameModel game)
        {
            var result = await _gameService.UpdateAsync(game);
            return result ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var game = await _gameService.ReadAsync(id);
            if (game == null) return NotFound();

            var result = await _gameService.RemoveAsync(game);
            return result ? NoContent() : BadRequest();
        }
    }
}
