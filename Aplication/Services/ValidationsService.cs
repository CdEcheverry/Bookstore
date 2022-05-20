using Aplication.HandlerExceptions;
using Aplication.Utility;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public interface IValidationsService
    {
        /// <summary>
        /// Helper method to determine if an author exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Author> ExistsAuthor(int id);
        /// <summary>
        /// Helper method to determine if an Editorial exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Editorial> ExistsEditorial(int id);
        /// <summary>
        /// Helper method to determine if an Book exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book> ExistsBook(int id);
        /// <summary>
        /// Valid if the number is greater than zero
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        bool IsOverZero(int? number);

        /// <summary>
        /// Valid if the number is valid
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        bool IsNumberValid(int? number);

        /// <summary>
        /// get list book for parameters
        /// </summary>
        /// <param name="param"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<List<Book>> GetBookParameters(List<Book> books,string param, string value);
        /// <summary>
        /// method that determines if a workbook can be saved
        /// </summary>
        /// <param name="dataSave"></param>
        /// <param name="MaximumRegisteredBooks"></param>
        /// <returns></returns>
        bool CanSave(int dataSave, int? MaximumRegisteredBooks);
    }
    public class ValidationsService : IValidationsService
    {
        private readonly BookStoreContext _context;  
        public ValidationsService(BookStoreContext context)
        {
            _context = context;       
        }
        public async Task<Author> ExistsAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                throw new CustomHandlerException(HttpStatusCode.NotFound, new { messages = "El autor no está registrado" });
            }
            return author;
        }
        public async Task<Editorial> ExistsEditorial(int id)
        {
            var editorial = await _context.Editorial.FindAsync(id);
            if (editorial == null)
            {
                throw new CustomHandlerException(HttpStatusCode.NotFound, new { messages = "La editorial no está registrada" });
            }
            return editorial;
        }
        public async Task<Book> ExistsBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                throw new CustomHandlerException(HttpStatusCode.NotFound, new { messages = "El libro no está registrado" });
            }
            return book;
        }
        public bool IsOverZero(int? number)
        {
            return number > 0;
        }
        public bool IsNumberValid(int? number)
        {
            if (number != null)
            {
                if (!IsOverZero(number))
                {
                    throw new CustomHandlerException(HttpStatusCode.BadRequest, new { messages = "El numero maximo de libros registrados debe ser mayor a 0" });
                }
            }
            return true;
        }
        public async Task<List<Book>> GetBookParameters(List<Book> books,string param, string value)
        {    
            switch (param.ToUpper())
            {
                case SearchType.Tittle:
                    books = await _context.Book.Where(x => x.Tittle.Contains(value)).Include(x => x.Author).Include(y => y.Editorial).ToListAsync();
                    break;
                case SearchType.YearPublication:
                    books = await _context.Book.Where(x => x.YearPublication.Contains(value)).Include(x => x.Author).Include(y => y.Editorial).ToListAsync();
                    break;
                case SearchType.AuthorFullName:
                    books = await _context.Book.Where(x => x.Author.FullName.Contains(value)).Include(x => x.Author).Include(y => y.Editorial).ToListAsync();
                    break;
                default:
                    throw new CustomHandlerException(HttpStatusCode.BadRequest, new { messages = "No se puede realizar la busqueda por ese parametro" });
            }

            if(books.Count()==0)
            {
                throw new CustomHandlerException(HttpStatusCode.NotFound, new { messages = "No se encontraron libros con los datos suministrados" });
            }
            return books;
        }
        public  bool CanSave(int dataSave, int? MaximumRegisteredBooks)
        {
            if(MaximumRegisteredBooks != -1)
            {
                if (dataSave >= MaximumRegisteredBooks)
                {
                    throw new CustomHandlerException(HttpStatusCode.BadRequest, new { messages = "No es posible registrar el libro, se alcanzo el máximo permitido." });
                }
            }
            return true;
        }
    }
}
