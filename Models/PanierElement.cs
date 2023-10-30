namespace Tp1AspNet_Sqlite.Models
{
    public class PanierElement
    {
        public int Id { get; set; }
        public int LivreId { get; set; }
        public Livre Livre { get; set; }
        public int PanierId { get; set; }
        public Panier panier { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
