using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Description { get; set; }
        public int Authorid { get; set; }
        public List<Author> authors { get; set; }
    }
}
