using MediatR;
using Persistence;
using Aplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using Aplication.HandlerExceptions;
using System.Net;

namespace Aplication.Authors
{
    public class CreateAuthor
    { 
        public class Implement : IRequest
        {
            public string FullName { get; set; }
            public string City { get; set; }
            public DateTime? Birthday { get; set; }
            public string Email { get; set; }
        }

        public class ImplementValidator : AbstractValidator<Implement>
        {
            public ImplementValidator()
            {
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.Birthday).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Implement>
         {
            private readonly BookStoreContext _context;

            public Handler(BookStoreContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Implement request, CancellationToken cancellationToken)
            {
                Mapping mapper = new Mapping(_context);
                
                Author newAuthor = mapper.NewAuthor(request.FullName, request.City, request.Birthday, request.Email);
                
                _context.Author.Add(newAuthor);
                
                var value = await _context.SaveChangesAsync();
               
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo crear el autor" });
            }
        }
    }
}
