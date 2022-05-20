using Aplication.Editorials;
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
    public class EditorialController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EditorialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<EditorialDTO>>> Get()
        {
            return await _mediator.Send(new GetAllEditorial.ListEditorials());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EditorialDTO>> GetByID(int id)
        {
            return await _mediator.Send(new GetEditorialById.GetEditorialId{ Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateEditorial.Implement data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int id, UpdateEditorial.Implement data)
        {
            data.IdEditorial = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new DeleteEditorial.Implement { Id = id });
        }
    }
}
