using Aplication.HandlerExceptions;
using Aplication.Services;
using AutoMapper;
using Domain;
using Domain.DTO;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Books
{
    public class GetBookbyParam
    {
        public class ListBooksbyParam : IRequest<List<BookDTO>>
        {
            public string Param { get; set; }
            public string Value { get; set; }
        }
        public class ImplementValidator : AbstractValidator<ListBooksbyParam>
        {
            public ImplementValidator()
            {
                RuleFor(x => x.Param).NotEmpty();
                RuleFor(x => x.Value).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<ListBooksbyParam, List<BookDTO>>
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

            public async Task<List<BookDTO>> Handle(ListBooksbyParam request, CancellationToken cancellationToken)
            {
                var books = new List<Book>();
                books = await _validationsService.GetBookParameters(books, request.Param, request.Value);
                List<BookDTO> booksDTO = _mapper.Map<List<Book>, List<BookDTO>>(books);
                return booksDTO;
            }
        }
    }
}
