using Aplication.HandlerExceptions;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Books
{
    public class UpdateBook
    {
        public class Implement : IRequest
        {
            public int IdBook { get; set; }
            public string Tittle { get; set; }
            public string Gender { get; set; }
            public int NumberPages { get; set; }
            public int IdEditorial { get; set; }
            public int IdAuthor { get; set; }
            public string YearPublication { get; set; }
        }

        public class Handler : IRequestHandler<Implement>
        {
            private readonly BookStoreContext _context;

            public Handler(BookStoreContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Implement request, CancellationToken cancellationToken)
            {
                Mapping mapper = new Mapping(_context);
                var book = await _context.Book.FindAsync(request.IdBook);

                if(book == null)
                {
                    throw new CustomHandlerException(HttpStatusCode.NotFound, new { messages = "No se encontro el libro" });
                }

                int result =await mapper.UpdateBook(book, request.Tittle, request.Gender, request.NumberPages, request.IdEditorial, request.IdAuthor, request.YearPublication);

                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo actualizar el libro" });
            }
        }
    }
}
