using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmdmWeb.Models;

namespace AmdmWeb.Models.Home
{
    public class HomePageModel
    {
        public List<PerformerInHomeModel> Performers { get; set; }
        public SortAndPageData SortAndPageData { get; set; }
    }
}