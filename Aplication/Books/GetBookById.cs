using MediatR;
using System.Collections.Generic;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Aplication.HandlerExceptions;
using System.Net;
using Domain.DTO;
using AutoMapper;

namespace Aplication.Books
{
   public class GetBookById
   {
        public class GetBookId : IRequest<BookDTO>
        {
            public int Id { get; set;}

            public class Handler : IRequestHandler<GetBookId, BookDTO>
            {
                private readonly BookStoreContext _context;
                private readonly IMapper _mapper;
                public Handler(BookStoreContext context, IMapper mapper)
                {
                    _context = context;
                    _mapper = mapper;
                }

                public async Task<BookDTO> Handle(GetBookId request, CancellationToken cancellationToken)
                {
                  var book = await _context.Book.FindAsync(request.Id);
                   
                    if (book == null)
                    {
                        throw new CustomHandlerException(HttpStatusCode.NotFound, new { messages = "No se encontro el libro" });
                    }
                    var bookDTO = _mapper.Map<Book,BookDTO>(book);

                    return bookDTO;                 
                } 
            }
        }
   }
}
