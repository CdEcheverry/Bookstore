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
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mediator"></param>
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a list of books given a filter 
        /// </summary>
        /// <remarks> allowed param:AuthorFullName , YearPublication , Tittle</remarks>
        /// <param name="param"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet("{param}/{value}")]
        public async Task<ActionResult<List<BookDTO>>> Get(string param, string value)
        {
            return await _mediator.Send(new GetBookbyParam.ListBooksbyParam {Param = param, Value=value});
        }

        /// <summary>
        /// Get all books 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> Get()
        {
            return await _mediator.Send(new GetAllBook.ListBooks());
        }

        /// <summary>
        /// Get book by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetByID(int id)
        {
            return await _mediator.Send(new GetBookById.GetBookId { Id = id });
        }

        /// <summary>
        /// Create book
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateBook.Implement data)
        {
            return await _mediator.Send(data);
        }

        /// <summary>
        /// Update the book given its Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int id, UpdateBook.Implement data)
        {
            data.IdBook = id;
            return await _mediator.Send(data);
        }

        /// <summary>
        /// delete the book given its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new DeleteBook.Implement { Id = id });
        }
    }
}
