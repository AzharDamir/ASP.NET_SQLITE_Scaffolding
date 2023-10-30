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
    public class IndexAdminModel : PageModel
    {

        private readonly DataContext _context;

        public IndexAdminModel(DataContext context)
        {
            _context = context;
        }

        public IList<Livre> Livre { get; set; } = default!;


        public async Task OnGetAsync()
        {
            var livres = from m in _context.Livre
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                livres = livres.Where(s => s.titre.Contains(SearchString));
            }
           // Genres = new SelectList(await livres.Distinct().ToListAsync());

            Livre = await livres.ToListAsync();

        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Titres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieTitres { get; set; }

    }
}
