using System;
using System.Collections.Generic;
using System.Text;

namespace PetConsoleAzureResources
{
    public interface ILogger
    {
        void WriteLog(string msg);
    }

    public class ConsoleLogger : ILogger
    {
        public void WriteLog(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
