using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace WebApplication2
{   
    [HubName("getMessage")]
    public class MyHub3 : Hub
    {

        public void SendMessage(string message)
        {
            Clients.All.broadcastMessage(message + DateTime.Now);
        }

        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name,message);

            
        }

        public override Task OnConnected()
        {
            Trace.WriteLine("客户端连接成功!");
            return base.OnConnected();
        }

        public void SendMessag(string message)
        {
            Clients.Others.talk(message);
        }
    }
}