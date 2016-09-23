using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmdmWeb.Models.Home
{
    public class PerformerInHomeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public int SongCount { get; set; }
    }
}