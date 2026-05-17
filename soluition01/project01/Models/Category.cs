using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Models
{
    public class Category
    {
       
        public int Id { get; set; }

       
        public string Name { get; set; }

        // التصنيف الواحد يحتوي على عدة أفلام
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
