using Application.DTO;
using Application.UseCases.Commands.Packing;
using DataAccess;
using DataAccess.Migrations;
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
    public class EfUpdatePackingCommand : IUpdatePackingCommand
    {
        public int Id => 14;

        public string Name => "Update packing";

        private Context _context;
        private UpdatePackingValidator _validator;

        public EfUpdatePackingCommand(Context context, UpdatePackingValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(UpdatePackingDto data)
        {
            _validator.ValidateAndThrow(data);

            Packing packing = _context.Packings.FirstOrDefault(x => x.Id == data.Id);

            packing.ProductId = data.ProductId;
            packing.ContainerId = data.ContainerId;
            packing.Quantity = data.Quantity;
            packing.UnitOfMeasurement = data.UnitOfMeasurement;
            packing.Price = data.Price;
            packing.InFocus = data.InFocus;

            _context.SaveChanges();
        }
    }
}
