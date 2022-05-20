using Aplication.Authors;
using Domain;
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
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get()
        {
            return await _mediator.Send(new GetAllAuthors.ListAuthors());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetByID(int id)
        {
            return await _mediator.Send(new GetAuthorById.GetAuthorId{Id=id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateAuthor.Implement data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int id, UpdateAuthor.Implement data)
        {
            data.IdAuthor = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new DeleteAuthor.Implement{ Id = id });
        }
    }
}
