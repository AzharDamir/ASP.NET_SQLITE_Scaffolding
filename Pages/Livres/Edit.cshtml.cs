using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp1AspNet_Sqlite.Data;
using Tp1AspNet_Sqlite.Models;

namespace Tp1AspNet_Sqlite.Pages.Livres
{
    public class EditModel : PageModel
    {
        private readonly Tp1AspNet_Sqlite.Data.DataContext _context;

        public EditModel(Tp1AspNet_Sqlite.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Livre Livre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Livre == null)
            {
                return NotFound();
            }

            var livre =  await _context.Livre.FirstOrDefaultAsync(m => m.id == id);
            if (livre == null)
            {
                return NotFound();
            }
            Livre = livre;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Livre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(Livre.id))
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

        private bool LivreExists(int id)
        {
          return (_context.Livre?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
