using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Models
{
    public class Movie
    {
      
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        // Foreign Key
      
        public int CategoryId { get; set; }

        // Navigation Property
        // كل فيلم ينتمي لتصنيف واحد
        public Category Category { get; set; }

        // الفيلم الواحد يقدر يحتوي Reviews كثيرة
        public List<Review> Reviews { get; set; } = new List<Review>();

        // الفيلم الواحد يقدر يكون في Watchlists كثيرة
        public List<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
    }
}
