using AmdmData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmdmWeb.Models.Song
{
    public class SongPageModel
    {
        public SongModel Song { get; set; }
        public string PerfoemerName { get; set; }
        public int BackPage { get; set; }
        public SortAndPageData SortAndPageData { get; set; }
    }
}