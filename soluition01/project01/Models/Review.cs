using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Models
{
    public class Review
    {
     
        public int Id { get; set; }

        public string Comment { get; set; }

        // التقييم من 1 إلى 5
        public int Rating { get; set; }

        // Foreign Key للمستخدم
        public int UserId { get; set; }

        // Navigation Property
        public User User { get; set; }

        // Foreign Key للفيلم
        public int MovieId { get; set; }

        // Navigation Property
        public Movie Movie { get; set; }
    }
}
