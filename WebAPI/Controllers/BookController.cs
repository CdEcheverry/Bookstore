using Aplication.Books;
using Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{param}/{value}")]
        public async Task<ActionResult<List<BookDTO>>> Get(string param, string value)
        {
            return await _mediator.Send(new GetBookbyParam.ListBooksbyParam {Param = param, Value=value});
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> Get()
        {
            return await _mediator.Send(new GetAllBook.ListBooks());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetByID(int id)
        {
            return await _mediator.Send(new GetBookById.GetBookId { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateBook.Implement data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int id, UpdateBook.Implement data)
        {
            data.IdBook = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new DeleteBook.Implement { Id = id });
        }
    }
}
