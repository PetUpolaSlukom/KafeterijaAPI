using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.Categories;
using Application.UseCases.Commands.Containers;
using Application.UseCases.Commands.Packing;
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
    public class PackingController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public PackingController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }
        // GET: api/<PackingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PackingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreatePackingDto dto, [FromServices] ICreatePackingCommand cmd)
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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(new { error = "An error has occured..." });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePackingDto dto, [FromServices] IUpdatePackingCommand command)
        {
            try
            {
                dto.Id = id;
                Packing p = _context.Packings.FirstOrDefault(p => p.Id == id);
                if (p == null)
                {
                    return NotFound();
                }
                _useCaseHandler.HandleCommand(command, dto);
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
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Packing p = _context.Packings.Find(id);
            if (p == null || p.IsActive == false)
            {
                return NotFound();
            }
            p.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
