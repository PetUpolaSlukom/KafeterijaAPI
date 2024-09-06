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
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.CartItems
{
    public class EfUpdateCartItemCommand : IUpdateCartItemCommand
    {
        public int Id => 17;

        public string Name => "CreateCartItem";

        private Context _context;
        private UpdateCartItemValidator _validator;
        private readonly IApplicationActor _actor;

        public EfUpdateCartItemCommand(Context context, UpdateCartItemValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }
        public void Execute(UpdateCartItemDto data)
        {
            _validator.ValidateAndThrow(data);

            CartItem cartItem = _context.CartItems.FirstOrDefault(c => c.Id == data.Id);

            cartItem.UserId = _actor.Id;
            cartItem.PackingId = data.PackingId;
            cartItem.Quantity = data.Quantity;

            _context.SaveChanges();
        }
    }
}
