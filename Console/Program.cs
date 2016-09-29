using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmdmLogic;
using AmdmData;
using Common;
using System.Net;

namespace AmdmConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var idList = Logic.GetPerformersId();
            idList.ForEach(id => {
                var s = Logic.GetSongsCount(id);
                AmdmLogger.Trace("Performer " + id + " : " + s + " songs");
            });
            var request = WebRequest.CreateHttp("http://localhost:49992/Log/Loged?time=" + DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString());
            request.GetResponse();



        }
    }
}
