using Microsoft.AspNetCore.Mvc;
using VideoGames.Infrastructure.Models;
using VideoGames.Common;

namespace VideoGames.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ICrudServiceAsync<GameModel> _gameService;

        public GameController(ICrudServiceAsync<GameModel> gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GameModel game)
        {
            var result = await _gameService.CreateAsync(game);
            return result ? StatusCode(201) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            var game = await _gameService.ReadAsync(id);
            return game != null ? Ok(game) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GameModel game)
        {
            var result = await _gameService.UpdateAsync(game);
            return result ? NoContent() : NotFound();
        }

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
