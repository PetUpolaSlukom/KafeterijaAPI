using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateCartItemDto
    {
        public int PackingId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartItemDto : CreateCartItemDto
    {
        public int Id { get; set; }
    }
    public class CartItemDto
    {
        public int Id { get; set; }
        public Packing Packing { get; set; }
        public int Quantity { get; set; }
    }
}
