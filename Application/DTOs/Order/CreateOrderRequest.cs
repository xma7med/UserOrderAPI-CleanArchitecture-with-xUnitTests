using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
