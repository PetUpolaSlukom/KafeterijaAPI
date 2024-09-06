using Application;
using Application.DTO;
using Application.UseCases.Commands.CartItems;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.CartItems
{
    public class EfCreateCartItemCommand : ICreateCartItemCommand
    {
        public int Id => 16;

        public string Name => "CreateCartItem";

        private Context _context;
        private CreateCartItemValidator _validator;
        private readonly IApplicationActor _actor;

        public EfCreateCartItemCommand(Context context, CreateCartItemValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }
        public void Execute(CreateCartItemDto data)
        {
            _validator.ValidateAndThrow(data);

            CartItem cartItem = new CartItem
            {
                Quantity = data.Quantity,
                UserId = _actor.Id,
                PackingId = data.PackingId,
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }
    }
}
