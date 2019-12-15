using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;
using WebUI.Data.Services.Interfaces;

namespace WebUI.Data.Services
{
    public class MessagesService : ServiceBase, IMessagesService
    {
        public MessagesService(CharityDbContext context)
            : base(context)
        {
        }

        public void AddMessage(string name, string lastName, string message)
        {
            _context.Messages.Add(new Message() { Name = name, LastName = lastName, MessageBody = message });
            _context.SaveChanges();
        }
    }
}
