using System.Collections.Generic;

namespace Service.Models
{
    public class Book
    {
        
        public string ISBN { get; set; } //Primary Key

        public string Title { get; set; }

        public int PublicationYear { get; set; }

        public string Publicationinfo { get; set; }

        public short Pages { get; set; }

        public List<Author> Authors { get; set; }

        public Classification Classification { get; set; }

        public int SignId { get; set; }

        public string ShortDescription
        {
            get
            {
                if (this.Publicationinfo == null)
                {
                    return "No description available";
                }
                else if (this.Publicationinfo.Length < 255)
                {
                    return this.Publicationinfo;
                }
                return this.Publicationinfo.Substring(0, 255) + "...";
            }
        }
    }
}