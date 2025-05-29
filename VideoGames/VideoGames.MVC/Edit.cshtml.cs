using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGames.Infrastructure.Models;

namespace VideoGames.MVC
{
    public class EditModel : PageModel
    {
        private readonly VideoGames.Infrastructure.Models.VideoGamesContext _context;

        public EditModel(VideoGames.Infrastructure.Models.VideoGamesContext context)
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

            var gamemodel =  await _context.GameModel.FirstOrDefaultAsync(m => m.Id == id);
            if (gamemodel == null)
            {
                return NotFound();
            }
            GameModel = gamemodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GameModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameModelExists(GameModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GameModelExists(Guid id)
        {
            return _context.GameModel.Any(e => e.Id == id);
        }
    }
}
