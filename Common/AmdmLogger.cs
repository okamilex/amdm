using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Common
{
    public class AmdmLogger
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Trace(string log)
        {
            logger.Trace(log);
        }
    }
}
