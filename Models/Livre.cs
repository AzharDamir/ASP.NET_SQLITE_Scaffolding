using System.ComponentModel.DataAnnotations.Schema;

namespace Tp1AspNet_Sqlite.Models
{
    public class Livre
    {
        public string titre { get; set; }

        public int id { get; set; }
        public string ImagePath { get; set; }
        [NotMapped] public IFormFile ImageFile { get; set; }

        public Livre(string titre, string imagePath)
        {

            this.titre = titre;
            ImagePath = imagePath;
        }

        public Livre()
        {
        }
    }
}
