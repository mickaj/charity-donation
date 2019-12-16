using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public abstract class ViewModelBase
    {
        public MessageData MessageData { get; set; }

        public ViewModelBase(MessageData messageData)
        {
            MessageData = messageData;
        }
    }
}
