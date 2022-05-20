using Domain;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication
{
    public class Mapping
    {
        private readonly BookStoreContext _context;
        public Mapping(BookStoreContext context)
        {
            _context = context;
        }

        public Author NewAuthor(string fullName, string city, DateTime? birthday, string email)
        {
            Author newAuthor = new Author()
            {
                FullName = fullName,
                City = city,
                Birthday = birthday,
                Email = email
            };

            return newAuthor;
        }

        public async Task<int> UpdateAuthor(Author author,string fullName, string city, DateTime? birthday, string email)
        {
            author.FullName = fullName ?? author.FullName;
            author.City = city ?? author.City;
            author.Birthday = birthday ?? author.Birthday;
            author.Email = email ?? author.Email;
            var value = await _context.SaveChangesAsync();
            return value;
        }

        public Editorial NewEditorial(string name, string phone, string email, DateTime? creationDate, int? maximumRegisteredBooks)
        {
            Editorial newEditorial = new Editorial()
            {
                Name = name,
                Phone = phone,
                Email = email,
                CreationDate = creationDate,
                MaximumRegisteredBooks = maximumRegisteredBooks ?? -1
            };

            return newEditorial;
        }

        public async Task<int> UpdateEditorial(Editorial editorial, string name, string phone,string email, DateTime? creationDate, int? maximumRegisteredBooks)
        {
            editorial.Name = name ?? editorial.Name;
            editorial.Phone = phone ?? editorial.Phone;
            editorial.Email = email ?? editorial.Email;
            editorial.CreationDate = creationDate ?? editorial.CreationDate;
            editorial.MaximumRegisteredBooks = maximumRegisteredBooks ?? editorial.MaximumRegisteredBooks;

            var value= await _context.SaveChangesAsync();
            return value;
        }

        public Book NewBook(string tittle, string gender, int numberPages, int? idEditorial, int idAuthor, string yearPublication)
        {
            Book newBook = new Book()
            {
                Tittle = tittle,
                Gender = gender,
                NumberPages = numberPages,
                IdEditorial = idEditorial,
                IdAuthor = idAuthor,
                YearPublication = yearPublication
            };

            return newBook;
        }

        public async Task<int> UpdateBook(Book book, string tittle, string gender, int? numberPages, int? idEditorial, int? idAuthor, string yearPublication)
        {
            book.Tittle = tittle ?? book.Tittle;
            book.Gender = gender ?? book.Gender;
            book.NumberPages = numberPages ?? book.NumberPages;
            book.IdEditorial = idEditorial ?? book.IdEditorial;
            book.IdAuthor = idAuthor ?? book.IdAuthor;
            book.YearPublication = yearPublication ?? book.YearPublication;

            var value = await _context.SaveChangesAsync();
            return value;
        }
    }
}
