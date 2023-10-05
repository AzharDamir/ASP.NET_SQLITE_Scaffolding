using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tp1AspNet_Sqlite.Data;
using Tp1AspNet_Sqlite.Models;

namespace Tp1AspNet_Sqlite.Pages.Livres
{
    public class CreateModel : PageModel
    {
        private readonly Tp1AspNet_Sqlite.Data.DataContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(DataContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Livre Livre { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(imageFile.FileName);

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                var filePath = Path.Combine(uploadsFolder, fileName);

                Directory.CreateDirectory(uploadsFolder);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Save the file path in your database
                Livre.ImagePath = "/uploads/" + fileName; // Update the path as per your project structure
                _context.Livre.Add(Livre);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
