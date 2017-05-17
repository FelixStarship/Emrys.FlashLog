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
   
    public class MyConnection1 : PersistentConnection
    {

        //connectionID就是客户端和服务器建立连接的唯一标识
        protected override Task OnConnected(IRequest request, string connectionId)
        {

            var data = DateTime.Now;
            Debug.WriteLine("客户端与服务端建立连接");
            return Connection.Send(connectionId, "Welcome!");
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var today = DateTime.Now.ToString();
            data = $"数据是:{data}时间时:{today}";
            return Connection.Broadcast(data);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Debug.WriteLine("掉线");
            return base.OnDisconnected(request, connectionId, stopCalled);
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            Debug.WriteLine("重连");
            return base.OnReconnected(request, connectionId);
        }
    }
}