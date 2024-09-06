using Application;
using Application.DTO;
using Application.UseCases.Commands.Orders;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Orders
{
    public class EfCreateOrderCommand : ICreateOrderCommand
    {
        public int Id => 18;

        public string Name => "Create order";

        private Context _context;
        private CreateOrderValidation _validator;
        private readonly IApplicationActor _actor;

        public EfCreateOrderCommand(Context context, CreateOrderValidation validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public void Execute(CreateOrderDto data)
        {
            _validator.ValidateAndThrow(data);

            Order order = new Order
            {
                UserId = _actor.Id,
                FullName = data.FullName,
                Email = data.Email,
                Address = data.Address,
                City = data.City,
                FullPrice = data.FullPrice,
                OrderDetails = data.OrderDetails,
            };

            _context.Orders.Add(order);

            var cartItems = _context.CartItems.Where(x => x.UserId == _actor.Id).ToList();

            cartItems.ForEach(x => x.IsActive = false);

            _context.SaveChanges();
        }
    }
}
