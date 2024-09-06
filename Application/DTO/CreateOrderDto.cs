using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateOrderDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal FullPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public class UpdateOrderDto : CreateOrderDto
    {
        public int Id { get; set; }
    }
}
