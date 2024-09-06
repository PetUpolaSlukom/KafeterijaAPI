using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.Messages;
using Application.UseCases.Queries;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public MessageController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search, [FromServices] IGetMessageQuery query)
            => Ok(_useCaseHandler.HandleQuery(query, search));


        // GET api/<MessageControlles>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MessageControlles>
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateMessageDto dto, [FromServices] ICreateMessageCommand cmd)
        {
            try
            {
                _useCaseHandler.HandleCommand(cmd, dto);

                return StatusCode(201);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(x => new
                {
                    Error = x.ErrorMessage,
                    Property = x.PropertyName
                }));
            }
            catch (Exception ex)
            {
                return this.InternalServerError(new { error = "An error has occured..." });
            }
        }

        // PUT api/<MessageControlles>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Message m = _context.Messages.Find(id);
            if (m == null || m.IsActive == false)
            {
                return NotFound();
            }
            m.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
