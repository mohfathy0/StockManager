using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Utilities
{
    public class LogHelper
    {
        public static string UserName { get; set; } = "Administrator";
        public static int UserId { get; set; } = 1;
        public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }


    }
}
