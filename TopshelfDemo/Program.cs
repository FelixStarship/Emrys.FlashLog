using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace TopshelfDemo
{
    class Program
    {   
        //Topshelf是一个开源的宿主服务器
        static void Main(string[] args)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);

            HostFactory.Run(x =>
            {
                x.Service<TownCrier>();
                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf Host服务的描述");
                x.SetDisplayName("Stuff显示名称");
                x.SetServiceName("Stuff服务名称");
            });
        }
    }

    public class TownCrier:ServiceControl
    {
        readonly Timer _timer;
        readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(TownCrier));
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, EventArgs) => Console.WriteLine("当前时间:" + DateTime.Now);
        }

        public bool Start(HostControl hostControl)
        {
            _log.Info("TopshelfDemo is Started");
            _timer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            throw new NotImplementedException();
        }
    } 
}
