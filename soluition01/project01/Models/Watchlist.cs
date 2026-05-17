using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Models
{
    public class Watchlist
    {
        // Foreign Key للمستخدم
        public int UserId { get; set; }

        // Navigation Property
        public User User { get; set; }

        // Foreign Key للفيلم
        public int MovieId { get; set; }

        // Navigation Property
        public Movie Movie { get; set; }

        // تاريخ إضافة الفيلم
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}
