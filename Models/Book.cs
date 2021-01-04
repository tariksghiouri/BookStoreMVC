using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Models
{
    public class Book
    {
        public int id { get; set; }
        public String title { get; set; }
        public String description { get; set; }
        public Author author { get; set; }
    }
}
