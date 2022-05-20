using Aplication.HandlerExceptions;
using Aplication.Services;
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
    public class DeleteBook
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
                var book = await _validationsService.ExistsBook(request.Id);
               _context.Remove(book);
               var result=await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo eliminar el libro" });
            }
        }
    }
}
