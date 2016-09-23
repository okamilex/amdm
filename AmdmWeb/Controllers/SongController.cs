using AmdmData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmdmLogic;

namespace AmdmWeb.Controllers
{
    public class SongController : Controller
    {
        
        public ActionResult Song(int songNumber = 1, PerformersSortingTypes performersSortingType = PerformersSortingTypes.ById, SongsSortingTypes songSortingType = SongsSortingTypes.ById, int performersPageNumber = 1, int songPageNumber = 1)
        {
            
            return View(Logic.GetPageOfSongList(1,songSortingType,songNumber,1).First());
        }
    }
}