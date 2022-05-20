using Aplication.HandlerExceptions;
using Aplication.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Editorials
{
    public class DeleteEditorial
    {
        public class Implement : IRequest
        {
            public int Id { get; set; }
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
                var books = await _context.Book.Where(x => x.Editorial.IdEditorial == request.Id).ToListAsync();
                foreach (var book in books)
                {
                    _context.Book.Remove(book);
                }

                var Editorial = await _validationsService.ExistsEditorial(request.Id);

                _context.Remove(Editorial);
                var result=await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo eliminar la editorial" });
            }
        }
    }
}
