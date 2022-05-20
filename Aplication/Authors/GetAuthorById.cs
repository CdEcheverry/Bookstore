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
using Aplication.Services;

namespace Aplication.Authors
{
   public class GetAuthorById
    {
        public class GetAuthorId : IRequest<AuthorDTO>
        {
            public int Id { get; set;}

            public class Handler : IRequestHandler<GetAuthorId , AuthorDTO>
            {
                private readonly BookStoreContext _context;
                private readonly IMapper _mapper;
                private readonly IValidationsService _validationsService;
                public Handler(BookStoreContext context, IMapper mapper, IValidationsService validationsService)
                {
                    _context = context;
                    _mapper = mapper;
                    _validationsService = validationsService;
                }

                public async Task<AuthorDTO> Handle(GetAuthorId request, CancellationToken cancellationToken)
                {           
                    var author = await _validationsService.ExistsAuthor(request.Id);
                    var authorDTO = _mapper.Map<Author,AuthorDTO>(author);

                    return authorDTO;                 
                } 
            }
        }
   }
}
