using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGames.Infrastructure.Models;

namespace VideoGames.MVC
{
    public class IndexModel : PageModel
    {
        private readonly VideoGames.Infrastructure.Models.VideoGamesContext _context;

        public IndexModel(VideoGames.Infrastructure.Models.VideoGamesContext context)
        {
            _context = context;
        }

        public IList<GameModel> GameModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            GameModel = await _context.GameModel.ToListAsync();
        }
    }
}
