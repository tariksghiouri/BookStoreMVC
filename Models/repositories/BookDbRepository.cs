using bookstore.Models;
using bookstore.Models.repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.Models.repositories
{
    public class BookDbRepository: IbookStoreRepository<Book>
    {
        BookStoreDbContext database;
        public BookDbRepository(BookStoreDbContext db)
        {
            database = db;

        }
        public void Add(Book entity)
        {
            database.books.Add(entity);
            database.SaveChanges();

        }

        public void Delete(int id)
        {
            var book = Find(id);
            database.books.Remove(book);
            database.SaveChanges();



        }

        public Book Find(int id)
        { 
            var book = database.books.SingleOrDefault(b => b.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return database.books.Include(a=>a.author).ToList();
        }

        public void Update(int id, Book newbook)
        {
            database.Update(newbook);
            database.SaveChanges();

        }

        public  List<Book> Search( string keyword)
        {
            var result = database.books.Include(a => a.author)
                .Where(b => b.title.Contains(keyword) ||
                            b.description.Contains(keyword) ||
                            b.author.name.Contains(keyword));
            return result.ToList();

        }
    }
}
