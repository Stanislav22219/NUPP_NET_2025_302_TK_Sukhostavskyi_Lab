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
    public class DeleteModel : PageModel
    {
        private readonly VideoGames.Infrastructure.Models.VideoGamesContext _context;

        public DeleteModel(VideoGames.Infrastructure.Models.VideoGamesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GameModel GameModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamemodel = await _context.GameModel.FirstOrDefaultAsync(m => m.Id == id);

            if (gamemodel == null)
            {
                return NotFound();
            }
            else
            {
                GameModel = gamemodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamemodel = await _context.GameModel.FindAsync(id);
            if (gamemodel != null)
            {
                GameModel = gamemodel;
                _context.GameModel.Remove(GameModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
