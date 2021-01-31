using bookstore.Models;
using bookstore.Models.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.Models.repositories
{
    public class AuthorDbRepository: IbookStoreRepository<Author>
    {
        BookStoreDbContext database;
        public AuthorDbRepository( BookStoreDbContext db)
        {
            database = db;

        }
        public void Add(Author entity)
        {
            database.authors.Add(entity);
            database.SaveChanges();

        }

        public void Delete(int id)
        {
            var author = Find(id);
           database.authors.Remove(author);
            database.SaveChanges();

        }

        public Author Find(int id)
        {
            var author = database.authors.SingleOrDefault(b => b.id == id);
            return author;

        }

        public IList<Author> List()
        {
            return database.authors.ToList();
        }

        public List<Author> Search(string keyword)
        {
            return database.authors.Where(a => a.name.Contains(keyword)).ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            database.Update(newAuthor);
            database.SaveChanges();

        }
    }
}
