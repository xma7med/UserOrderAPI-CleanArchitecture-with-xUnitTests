using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuditLog:BaseEntity
    {
        public string Action { get; set; } = default!;      // CreateOrder, RegisterUser, ...
        public string EntityName { get; set; } = default!;  // "Order", "User"
        public int? EntityId { get; set; }
        public string? Details { get; set; }
    }
}
