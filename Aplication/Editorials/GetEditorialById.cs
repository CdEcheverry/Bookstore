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

namespace Aplication.Editorials
{
   public class GetEditorialById
   {
        public class GetEditorialId : IRequest<EditorialDTO>
        {
            public int Id { get; set;}
            public class Handler : IRequestHandler<GetEditorialId, EditorialDTO>
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
                public async Task<EditorialDTO> Handle(GetEditorialId request, CancellationToken cancellationToken)
                {
                    var editorial = await _validationsService.ExistsEditorial(request.Id);
                    var editorialDTO = _mapper.Map<Editorial,EditorialDTO>(editorial);
                    return editorialDTO;                 
                } 
            }
        }
   }
}
