using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp1AspNet_Sqlite.Data;
using Tp1AspNet_Sqlite.Models;
using System.Text.Json;

namespace Tp1AspNet_Sqlite.Pages.Livres
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;
        private readonly CartModel _cartModel;

        public IndexModel(DataContext context, CartModel cartModel)
        {
            _context = context;
            _cartModel = cartModel;
        }
       


        public IList<Livre> Livre { get; set; } = new List<Livre>();

        public async Task OnGetAsync()
        {
            var livres = from m in _context.Livre
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                livres = livres.Where(s => s.titre.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(productGenre))
            {
                livres = livres.Where(s => s.category == productGenre);
            }

            Livre = await livres.ToListAsync();
        }
       
      
        public IActionResult OnPostAddToCart(int livreId, int quantity)
        {
            var livre = _context.Livre.FirstOrDefault(l => l.id == livreId);
            
                if (livre != null)
                {
                    var panierElement = new PanierElement
                    {
                        LivreId = livre.id,
                        Livre = livre,
                        Quantity = quantity,
                        Price = livre.price
                    };

                    // Récupérer le panier depuis les cookies
                    List<PanierElement> panierItems = GetCartItems();

                    // Ajouter l'élément au panier
                    panierItems.Add(panierElement);

                    // Enregistrer le panier dans les cookies
                    SetCartItems(panierItems);
                
            }
            

            // Redirigez l'utilisateur vers la page du panier (Cart)
            return RedirectToPage("CartModel");
        }

        private List<PanierElement> GetCartItems()
        {
            var cart = HttpContext.Request.Cookies["CartModel"];
            return !string.IsNullOrEmpty(cart) ? JsonSerializer.Deserialize<List<PanierElement>>(cart) : new List<PanierElement>();
        }

        private void SetCartItems(List<PanierElement> items)
        {
            var serializedCart = JsonSerializer.Serialize(items);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1),
                HttpOnly = true,
                IsEssential = true
            };

            HttpContext.Response.Cookies.Append("CartModel", serializedCart, cookieOptions);
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Titres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? productGenre { get; set; }

       // public int quantity { get; set; }

    }
}
