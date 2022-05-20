using MediatR;
using Persistence;
using Aplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using Aplication.HandlerExceptions;
using System.Net;
using Aplication.Services;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Books
{
    public class CreateBook
    { 
        public class Implement : IRequest
        {
            public string Tittle { get; set; }
            public string Gender { get; set; }
            public int NumberPages { get; set; }
            public int IdEditorial { get; set; }
            public int IdAuthor { get; set; }
            public string YearPublication { get; set; }
        }
        public class ImplementValidator : AbstractValidator<Implement>
        {
            public ImplementValidator()
            {
                RuleFor(x => x.Tittle).NotEmpty();
                RuleFor(x => x.Gender).NotEmpty();
                RuleFor(x => x.NumberPages).NotEmpty().Must(IsOverZero).WithMessage("El número de páginas debe ser mayor a 0");
                RuleFor(x => x.IdEditorial).NotEmpty();
                RuleFor(x => x.IdAuthor).NotEmpty();
                RuleFor(x => x.YearPublication).NotEmpty();
            }
            private bool IsOverZero(int numberPages)
            {
                return numberPages > 0;
            }
        }
        public class Handler : IRequestHandler<Implement>
         {
            private readonly BookStoreContext _context;
            private readonly IValidationsService _validationsService;
            public Handler(BookStoreContext context, IValidationsService validationsService)
            {
                _context = context;
                _validationsService = validationsService;
            }
            public async Task<Unit> Handle(Implement request, CancellationToken cancellationToken)
            {
                Mapping mapper = new Mapping(_context);
                var editorial = await _validationsService.ExistsEditorial(request.IdEditorial);
                var author = await _validationsService.ExistsAuthor(request.IdAuthor);
                var books = await _context.Book.Where(x => x.Editorial.IdEditorial == request.IdEditorial).ToListAsync();
                if (_validationsService.CanSave(books.Count(), editorial.MaximumRegisteredBooks))
                {
                    Book newBook = mapper.NewBook(request.Tittle, request.Gender, request.NumberPages, request.IdEditorial, request.IdAuthor, request.YearPublication);
                    _context.Book.Add(newBook);
                }
                var value = await _context.SaveChangesAsync();             
                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo crear el libro" });
            }
        }
    }
}
