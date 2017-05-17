using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.SignalR;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetConnectionContext<MyConnection1>();
            context.Connection.Broadcast("将服务端的数据广播到所有客户端!");
        }
    }
}