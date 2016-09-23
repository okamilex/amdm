using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmdmWeb.Models.Performer
{
    public class SongInPerformeModel
    {
        public int Id { get; set; }
        public int NumberOnThePage { get; set; }
        public string Name { get; set; }
        public int ChordsAmount { get; set; }
        public int Views { get; set; }
    }
}