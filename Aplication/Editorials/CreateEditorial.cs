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
using Aplication.Services;

namespace Aplication.Editorials
{
    public class CreateEditorial
    { 
        public class Implement : IRequest
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public DateTime? CreationDate { get; set; }
            public int? MaximumRegisteredBooks { get; set; }

        }

        public class ImplementValidator : AbstractValidator<Implement>
        {          
            public ImplementValidator( )
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Phone).NotEmpty().MinimumLength(10).WithMessage("El número de telefono no puede tener menos de 10 caracteres");
                RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Se requiere un email valido");
                RuleFor(x => x.CreationDate).NotEmpty();            
            }
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
                _validationsService.IsNumberValid(request.MaximumRegisteredBooks);

                Editorial newEditorial = mapper.NewEditorial(request.Name, request.Phone, request.Email, request.CreationDate, request.MaximumRegisteredBooks);               
                _context.Editorial.Add(newEditorial);
                
                var value = await _context.SaveChangesAsync();
               
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new CustomHandlerException(HttpStatusCode.InternalServerError, new { messages = "No se pudo crear la editorial" });
            }
        }
    }
}
