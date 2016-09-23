using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmdmData.Enums;

namespace AmdmWeb.Models
{
    public class SortAndPageData
    {
        public int PerformersPageNumber { get; set; }
        public int SongsPageNumber { get; set; }
        public int LasPageNumber { get; set; }
        public PerformersSortingTypes PerformersSortingType { get; set; }
        public SongsSortingTypes SongsSortingType { get; set; }
    }
}