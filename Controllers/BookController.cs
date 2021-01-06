﻿using bookstore.Models;
using bookstore.Models.repositories;
using bookstore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IbookStoreRepository<Book> bookRepository;
        private readonly IbookStoreRepository<Author> authorRepository;
        public BookController(IbookStoreRepository<Book> bookRepository,IbookStoreRepository<Author> authorRepository)
        {
            this.bookRepository = bookRepository; 
            this.authorRepository = authorRepository; 
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
            try
            {
                if (model.Authorid == -1)
                {
                    ViewBag.Message = "Please select an author !";
                    var vmodel = new BookAuthorViewModel
                    {
                        authors = FillSelectList()

                    };
                    return View(vmodel);
                }

                Book book = new Book
                {
                    title = model.BookTitle,
                    id = model.BookId,
                    description = model.Description,
                    author = authorRepository.Find(model.Authorid)
                };
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
                authors = authorRepository.List().ToList()

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
                Book book = new Book
                {
                    id=model.BookId, 
                    title = model.BookTitle,
                    description = model.Description,
                    author = authorRepository.Find(model.Authorid)
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
        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { id = -1, name = "--- Select an author ---" });

            return authors;
        }

    }
    
}
