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
        /// <summary>
        /// Get all Editorials
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<EditorialDTO>>> Get()
        {
            return await _mediator.Send(new GetAllEditorial.ListEditorials());
        }
        /// <summary>
        /// Get the Editorial given its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EditorialDTO>> GetByID(int id)
        {
            return await _mediator.Send(new GetEditorialById.GetEditorialId{ Id = id });
        }
        /// <summary>
        /// Create Editorial
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateEditorial.Implement data)
        {
            return await _mediator.Send(data);
        }
        /// <summary>
        /// update the Editorial given its Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int id, UpdateEditorial.Implement data)
        {
            data.IdEditorial = id;
            return await _mediator.Send(data);
        }
        /// <summary>
        /// Delete the Editorial given its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new DeleteEditorial.Implement { Id = id });
        }
    }
}
