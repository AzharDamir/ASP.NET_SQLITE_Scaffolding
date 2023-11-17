using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tp1AspNet_Sqlite.Data;
using Tp1AspNet_Sqlite.Models;
using Microsoft.AspNetCore.Http;

namespace Tp1AspNet_Sqlite.Pages.Livres
{
    public class CartModel : PageModel
    {
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartModel(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<PanierElement> CartItems { get; set; } = new List<PanierElement>();

        public IActionResult OnGet()
        {
            // Récupérer le panier depuis les cookies et désérialiser les PanierElement
            CartItems = GetCartItems();
            return Page();
        }
       /* public IActionResult OnPostAddToCart(int livreId,int quantity)
        {
            var livre = GetLivreById(livreId);
           
            if (livre != null)
            {
                var panierElement = new PanierElement
                {
                    LivreId = livre.id,
                    Livre = livre,
                    Quantity = quantity, // Vous pouvez obtenir la quantité à partir du formulaire
                    Price = livre.price
                };

                // Ajouter l'élément au panier
                CartItems.Add(panierElement);

                // Enregistrer le panier dans les cookies
                SetCartItems(CartItems);
            }

            return RedirectToPage("CartModel");
        }
   */
        public IActionResult OnPostRemoveFromCart(int livreId)
        {
            var existingItem = CartItems.FirstOrDefault(item => item.LivreId == livreId);

            if (existingItem != null)
            {
                // Supprimez l'élément du panier
                CartItems.Remove(existingItem);

                // Mettez à jour le panier dans les cookies après la suppression
                SetCartItems(CartItems);
            }

            return RedirectToPage("CartModel");
        }


        private List<PanierElement> GetCartItems()
        {
            var cart = HttpContext.Request.Cookies["CartModel"];
            return !string.IsNullOrEmpty(cart) ? JsonSerializer.Deserialize<List<PanierElement>>(cart) : new List<PanierElement>();
        }

        
        public void SetCartItems(List<PanierElement> items)
        {
            var serializedCart = JsonSerializer.Serialize(items);
            HttpContext.Response.Cookies.Append("CartModel", serializedCart);

        }

        private Livre GetLivreById(int livreId)
        {
            return _context.Livre.FirstOrDefault(l => l.id == livreId);
        }
    }
}
