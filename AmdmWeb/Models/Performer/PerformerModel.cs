using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmdmWeb.Models.Performer
{
    public class PerformerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string PerformerPageLink { get; set; }
        public string BiographyText { get; set; }
        public List<SongInPerformeModel> Songs { get; set; }
    }
}