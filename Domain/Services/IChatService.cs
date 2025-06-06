using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IChatService
    {
        Task SendMessageAsync(string user, string message);
    }
}