using MediatR;
using System.Collections.Generic;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.DTO;
using AutoMapper;

namespace Aplication.Authors
{
    public class GetAllAuthors
    {
        public class ListAuthors : IRequest<List<AuthorDTO>> { }

        public class Handler : IRequestHandler<ListAuthors, List<AuthorDTO>>
        {
            private readonly BookStoreContext _context;
            private readonly IMapper _mapper;
            public Handler(BookStoreContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;

            }

            public async Task<List<AuthorDTO>> Handle(ListAuthors request, CancellationToken cancellationToken)
            {
                List<Author> authors = await _context.Author.ToListAsync();

                List<AuthorDTO> authorsDTO = _mapper.Map<List<Author>, List<AuthorDTO>>(authors);
                return authorsDTO;
            }
        }
    }
}
