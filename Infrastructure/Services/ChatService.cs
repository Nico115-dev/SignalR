using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services;

namespace Infrastructure.Services
{
    public class ChatService : IChatService
    {
        public async Task SendMessageAsync(string user, string message)
        {
            Console.WriteLine($"{user}: {message}");
            await Task.CompletedTask;
        }
    }
}