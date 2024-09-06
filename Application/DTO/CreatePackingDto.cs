using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreatePackingDto
    {
        public int ProductId { get; set; }
        public int ContainerId { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdatePackingDto : CreatePackingDto
    {
        public int Id { get; set; }
        public bool InFocus { get; set; }
    }

    public class PackingDto
    {
        public int Id { get; set; }
        public Container Container { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Price { get; set; }
    }
}
