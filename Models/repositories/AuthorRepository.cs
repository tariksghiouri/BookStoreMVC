using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Models.repositories
{
    public class AuthorRepository : IbookStoreRepository<Author>

    {
        IList<Author> Authors;
        public AuthorRepository()
        {
            Authors = new List<Author>()
        {
            new Author{ id=1,name="Tarik"},
            new Author{ id=2,name="khalil"},
            new Author{ id=3,name="hamid"},
        };
                
    }
        public void Add(Author entity)
        {
            Authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            Authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = Authors.SingleOrDefault(b => b.id == id);
            return author;

        }

        public IList<Author> List()
        {
            return Authors;
        }

        public void Update(int id, Author newAuthor)
        {
            var author= Find(id);
            //author.id=newAuthor.id;
            author.name=newAuthor.name;

        }
    }
}
