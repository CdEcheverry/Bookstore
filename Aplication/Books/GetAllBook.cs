using MediatR;
using System.Collections.Generic;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.DTO;
using AutoMapper;

namespace Aplication.Books
{
    public class GetAllBook
    {
        public class ListBooks : IRequest<List<BookDTO>> { }
        public class Handler : IRequestHandler<ListBooks, List<BookDTO>>
        {
            private readonly BookStoreContext _context;
            private readonly IMapper _mapper;
            public Handler(BookStoreContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;

            }
            public async Task<List<BookDTO>> Handle(ListBooks request, CancellationToken cancellationToken)
            {
                List<Book> books = await _context.Book.Include(x=> x.Author).Include(y=>y.Editorial).ToListAsync();

                List<BookDTO> booksDTO = _mapper.Map<List<Book>, List<BookDTO>>(books);
                return booksDTO;
            }
        }
    }
}
