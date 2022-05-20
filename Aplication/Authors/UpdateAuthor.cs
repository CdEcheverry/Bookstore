using Aplication.HandlerExceptions;
using Aplication.Services;
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

namespace Aplication.Authors
{
    public class UpdateAuthor
    {
        public class Implement : IRequest
        {
            public int IdAuthor { get; set; }
            public string FullName { get; set; }
            public string City { get; set; }
            public DateTime? Birthday { get; set; }
            public string Email { get; set; }
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
                var author =await _validationsService.ExistsAuthor(request.IdAuthor);            
                int result =await mapper.UpdateAuthor(author, request.FullName, request.City, request.Birthday, request.Email);
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo actualizar el autor" });
            }
        }
    }
}
