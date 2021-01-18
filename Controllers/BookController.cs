using bookstore.Models;
using bookstore.Models.repositories;
using bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IbookStoreRepository<Book> bookRepository;
        private readonly IbookStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;
        public BookController(IbookStoreRepository  <Book> bookRepository,
            IbookStoreRepository<Author> authorRepository,
            IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository; 
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book=bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel {
                authors = FillSelectList()
            
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)

            {
                try
                {       if (model.Authorid == -1)
                        {
                        ViewBag.Message = "Please select an author !";
                        
                        return View(GetAllAuthors());
                        }
                    string filename = string.Empty;
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                        filename = model.File.FileName;
                        string fullPath = Path.Combine(uploads, filename);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));

                    }
                   

                    Book book = new Book
                    {
                        title = model.BookTitle,
                        id = model.BookId,
                        description = model.Description,
                        author = authorRepository.Find(model.Authorid),
                        ImgUrl=filename
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
             
                ModelState.AddModelError("", "please fill the required fields");
                return View(GetAllAuthors());
           
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var viewmodel = new BookAuthorViewModel
            {
                BookId = book.id,
                BookTitle = book.title,
                Description = book.description,
                //Authorid = book.author.id,
                authors = authorRepository.List().ToList(),
                Imgurl = book.ImgUrl

            };
            return View(viewmodel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel model)
        {
            try
            {
                string filename = string.Empty;
                if (model.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                     filename = model.File.FileName;
                    string fullPath = Path.Combine(uploads, filename);
                    //deleting the old image
                    string oldFilename = bookRepository.Find(model.BookId).ImgUrl;
                    string oldFilePath = Path.Combine(uploads, oldFilename);
                    if (fullPath != oldFilePath)
                    {
                        System.IO.File.Delete(oldFilePath);
                        //saving the new image

                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                }
                Book book = new Book
                {
                    id=model.BookId, 
                    title = model.BookTitle,
                    description = model.Description,
                    author = authorRepository.Find(model.Authorid),
                    ImgUrl=filename
                };
                bookRepository.Update(model.BookId,book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                authors = FillSelectList()
            };

            return vmodel;
        }
        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { id = -1, name = "--- Select an author ---" });

            return authors;
        }

    }
    
}
