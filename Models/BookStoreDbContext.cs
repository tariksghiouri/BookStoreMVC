﻿using bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.Models
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext( DbContextOptions<BookStoreDbContext> options):base(options)
        {

        }
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> books { get; set; }
    }
}
