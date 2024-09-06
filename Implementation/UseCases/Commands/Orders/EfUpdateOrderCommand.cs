using Application;
using Application.DTO;
using Application.UseCases.Commands.Orders;
using DataAccess;
using Domain;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Orders
{
    public class EfUpdateOrderCommand : IUpdateOrderCommand
    {
        public int Id => 19;

        public string Name => "Update order";

        private Context _context;
        private readonly IApplicationActor _actor;

        public EfUpdateOrderCommand(Context context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public void Execute(UpdateOrderDto data)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == data.Id);

            order.FullName = data.FullName;
            order.Email = data.Email;
            order.Address = data.Address;
            order.City = data.City;
            order.FullPrice = data.FullPrice;
            order.OrderDetails = data.OrderDetails;

            _context.SaveChanges();
        }
    }
}
