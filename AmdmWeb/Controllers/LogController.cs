using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmdmWeb.Controllers
{
    public class LogController : Controller
    {        
        [HttpGet]
        public void Loged(string time)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<AmdmHub>();
            context.Clients.All.addMessage("Updated! ", time);
        }
        
    }
}