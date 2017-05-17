using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrys.FlashLog;


namespace ConsoleApplication1
{
    class Program
    {   
        //http://www.cnblogs.com/emrys5/p/6693454.html
        static void Main(string[] args)
        {
            FlashLogger.Instance().Register();

            FlashLogger.Debug("Debug");
            FlashLogger.Debug("Debug", new Exception("testexception"));
            FlashLogger.Info("Info");
            FlashLogger.Fatal("Fatal");
            FlashLogger.Error("Error");
            FlashLogger.Warn("Warn", new Exception("testexception"));
        }
    }
}
