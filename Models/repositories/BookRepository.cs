using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Models.repositories
{
    public class BookRepository : IbookStoreRepository<Book>
    {
        List<Book> books;
        //private readonly IbookStoreRepository<Author> authorRepository;

        public BookRepository(IbookStoreRepository<Author> authorRepository)
        {
            //this.authorRepository = authorRepository;
            books = new List<Book>()
                {
                    new Book
                    {
                        id=1, title="book1",description="description1" ,author=new Author(), ImgUrl="book1.jpg" /*author=authorRepository.Find(1)*/
                    },
                    new Book
                    {
                        id=2, title="book2",description="description2", author=new Author(), ImgUrl="book2.jpg" /*author=authorRepository.Find(2)*/
                    },
                    new Book
                    {
                        id=3, title="book3",description="description3" ,author=new Author(), ImgUrl="book3.jpg"/*author=authorRepository.Find(3)*/
                    },
                };
        }
        public void Add(Book entity)
        {
            entity.id = books.Max(b => b.id)+ 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);


        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id,Book newbook)
        {
            var book = Find(id);
            book.title = newbook.title;
            book.author = newbook.author;
            book.description = newbook.description;
            book.id = newbook.id;
            book.ImgUrl = newbook.ImgUrl;


        }
    }
}
