using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VideoGames.Infrastructure.Models;

namespace VideoGames.MVC
{
    public class CreateModel : PageModel
    {
        private readonly VideoGames.Infrastructure.Models.VideoGamesContext _context;

        public CreateModel(VideoGames.Infrastructure.Models.VideoGamesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public GameModel GameModel { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.GameModel.Add(GameModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
