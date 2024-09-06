using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.CartItems;
using Application.UseCases.Commands.Orders;
using Application.UseCases.Commands.Packing;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public OrderController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto dto, [FromServices] ICreateOrderCommand cmd)
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

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateOrderDto dto, [FromServices] IUpdateOrderCommand command)
        {
            try
            {
                dto.Id = id;
                Order o = _context.Orders.FirstOrDefault(o => o.Id == id);
                if (o == null)
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

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
