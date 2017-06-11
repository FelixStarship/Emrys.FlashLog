using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using log4net;

namespace QuartzDemo
{
   public sealed  class SampleJob:IJob
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(SampleJob));
        public void Execute(IJobExecutionContext context)
        {
            _logger.Info("SampleJob测试");
            Console.WriteLine("执行任务"+DateTime.Now.ToString());
        }
    }
}
