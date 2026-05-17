using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Models
{
    public class User
    {
     
        public int Id { get; set; }

     
        public string Name { get; set; }

     
        public string Email { get; set; }

        // Navigation Property
        // المستخدم الواحد يقدر يكتب Reviews كثيرة
        public List<Review> Reviews { get; set; } = new List<Review>();

        // المستخدم الواحد يقدر يضيف أفلام كثيرة للـ Watchlist
        public List<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
    }
}
