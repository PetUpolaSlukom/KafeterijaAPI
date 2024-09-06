using Application.DTO;
using Application.UseCases.Commands.Containers;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.packings
{
    public class EfCreatePackingCommand : ICreatePackingCommand
    {
        public int Id => 13;

        public string Name => "Create packing";

        private Context _context;
        private CreatePackingValidator _validator;

        public EfCreatePackingCommand(Context context, CreatePackingValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public void Execute(CreatePackingDto data)
        {
            _validator.ValidateAndThrow(data);

            Packing packing = new Packing
            {
                ProductId = data.ProductId,
                ContainerId = data.ContainerId,
                Quantity = data.Quantity,
                UnitOfMeasurement = data.UnitOfMeasurement,
                Price = data.Price
            };

            _context.Packings.Add(packing);
            _context.SaveChanges();

        }
    }
}
