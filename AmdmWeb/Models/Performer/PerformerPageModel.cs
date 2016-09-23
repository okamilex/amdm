using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmdmData;
using AmdmWeb.Models;
using AmdmWeb.Models.Performer;

namespace AmdmWeb.Models.Performer
{
    public class PerformerPageModel
    {
        public PerformerModel Performer { get; set; }
        public int PageSize { get; set; }
        public SortAndPageData SortAndPageData { get; set; }
    }
}