using System.ComponentModel.DataAnnotations.Schema;

namespace Tp1AspNet_Sqlite.Models
{
    public class Livre
    {
        public string titre { get; set; }

        public int id { get; set; }
        public String desc { get; set; }
        public String category { get; set; }
        public string ImagePath { get; set; }
        public int price {  get; set; }
        [NotMapped] public IFormFile ImageFile { get; set; }

        public Livre(string titre, string imagePath, string desc, string category, int price)
        {

            this.titre = titre;
            ImagePath = imagePath;
            this.desc = desc;
            this.category = category;
            this.price = price;
        }

        public Livre()
        {
        }
    }
}
