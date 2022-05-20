using MediatR;
using System.Collections.Generic;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.DTO;
using AutoMapper;

namespace Aplication.Editorials
{
    public class GetAllEditorial
    {
        public class ListEditorials : IRequest<List<EditorialDTO>> { }
        public class Handler : IRequestHandler<ListEditorials, List<EditorialDTO>>
        {
            private readonly BookStoreContext _context;
            private readonly IMapper _mapper;
            public Handler(BookStoreContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<EditorialDTO>> Handle(ListEditorials request, CancellationToken cancellationToken)
            {
                List<Editorial> editorials = await _context.Editorial.ToListAsync();

                List<EditorialDTO> editorialsDTO = _mapper.Map<List<Editorial>, List<EditorialDTO>>(editorials);
                return editorialsDTO;
            }
        }
    }
}
