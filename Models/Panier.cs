namespace Tp1AspNet_Sqlite.Models
{
    public class Panier
    {
        public int Id { get; set; }

        public List<PanierElement> Items { get; set; } = new List<PanierElement>();
    }
}
