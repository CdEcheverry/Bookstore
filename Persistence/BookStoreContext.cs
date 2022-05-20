using Microsoft.EntityFrameworkCore;
using System;
using Domain;

namespace Persistence
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions options) : base(options){

        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
        
        public DbSet <Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Editorial> Editorial { get; set; }

    }
}
