using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IAuditService
    {
        Task LogAsync(string action, string entityName, int? entityId, string? details = null);
    }
}
