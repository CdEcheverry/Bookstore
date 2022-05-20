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

namespace Aplication.Editorials
{
    public class UpdateEditorial
    {
        public class Implement : IRequest
        {
            public int IdEditorial { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public DateTime? CreationDate { get; set; }
            public int? MaximumRegisteredBooks { get; set; }
        }
        public class Handler : IRequestHandler<Implement>
        {
            private readonly BookStoreContext _context;
            private readonly IValidationsService _validationsService;
            public Handler(BookStoreContext context, IValidationsService validationsService)
            {
                _context = context;
                _validationsService=validationsService;
            }
            public async Task<Unit> Handle(Implement request, CancellationToken cancellationToken)
            {
                Mapping mapper = new Mapping(_context);
                _validationsService.IsNumberValid(request.MaximumRegisteredBooks);
                var editorial = await _validationsService.ExistsEditorial(request.IdEditorial);
                int result =await mapper.UpdateEditorial(editorial,request.Name, request.Phone, request.Email, request.CreationDate, request.MaximumRegisteredBooks);
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo actualizar la Editorial" });
            }
        }
    }
}
