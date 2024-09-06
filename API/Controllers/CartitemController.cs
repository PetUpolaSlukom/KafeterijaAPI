using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.CartItems;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartitemController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public CartitemController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }
        // GET: api/<CartitemController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CartitemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCartItemDto dto, [FromServices] ICreateCartItemCommand cmd)
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

        // PUT api/<CartitemController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCartItemDto dto, [FromServices] IUpdateCartItemCommand command)
        {
            try
            {
                dto.Id = id;
                CartItem c = _context.CartItems.FirstOrDefault(x => x.Id == id);
                if (c == null)
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

        // DELETE api/<CartitemController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CartItem c = _context.CartItems.Find(id);
            if (c == null || c.IsActive == false)
            {
                return NotFound();
            }
            c.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
