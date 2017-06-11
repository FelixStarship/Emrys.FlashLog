using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace QuartzDemo
{
    class Program
    {   



        static void Main(string[] args)
        {
            //CodeMethod();
            ConfigMethod();
        }


        private static void CodeMethod()
        {   

            Console.WriteLine(DateTime.Now.ToString());
            //1.创建一个作业调度池
            ISchedulerFactory scheduler = new StdSchedulerFactory();
            IScheduler sched = scheduler.GetScheduler();
            //2.创建一个具体的作业
            IJobDetail job = JobBuilder.Create<JobDemo>().Build();
            //3.创建并配置一个触发器
            #region   每隔5秒执行一次Execute方法，无休止
            //ISimpleTrigger trigger=(ISimpleTrigger)TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(int.MaxValue)).Build();
            #endregion
            
            
            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(DateTime.Now.AddSeconds(1), 2);
            DateTimeOffset endTime= DateBuilder.NextGivenSecondDate(DateTime.Now.AddHours(1), 5);
            #region  每隔5秒执行一次，一共执行50次，开始时间设定在当前时间，结束时间设定在1小时后，不管50次有没有执行完，1小时候程序都不在继续执行
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().StartAt(startTime).EndAt(endTime).WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(50)).Build();
            #endregion

            #region 使用cron-like维度调用
            //ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create().StartAt(startTime).EndAt(endTime).WithCronSchedule("11,27 10,20,30 * * * ? ").Build();
            #endregion
            //4.加入作业调度池中
            sched.ScheduleJob(job, trigger);
            sched.Start();
            Console.ReadKey();
        }

        private static void ConfigMethod()
        {
            var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
            var path = $"{currentDomain}log4net.xml";
            try
            {
                ISchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = factory.GetScheduler();
                scheduler.GetJobGroupNames();
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
                scheduler.Start();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class JobDemo : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString());
        }
    }
}
