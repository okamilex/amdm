using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AmdmLogic;
using AmdmWeb.Models.Home;
using AmdmWeb.Models;
using AmdmData.Enums;
using AmdmData;

namespace AmdmWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<PerformerInHomeModel> performers = new List<PerformerInHomeModel>();
            SortAndPageData sortAndPageData = new SortAndPageData { PerformersPageNumber = 1, LasPageNumber = Convert.ToInt32(Math.Ceiling(Logic.GetPerformersCount() / 10.0)), PerformersSortingType = PerformersSortingTypes.ByName };
            Logic.GetPageOfPerformerList(sortAndPageData.PerformersSortingType, sortAndPageData.PerformersPageNumber, Const.PageSize).ForEach(x => performers.Add(new PerformerInHomeModel { Name = x.Name, Id = x.Id, SongCount = x.Songs.Count, ImageLink = x.ImageLink }));
            HomePageModel homePageModel = new HomePageModel { Performers = performers, SortAndPageData = sortAndPageData };           
            return View(homePageModel);        
        }


        [HttpGet]
        public ActionResult PerformersPage(PerformersSortingTypes performersSortingType, int performersPageNumber)
        {
            List<PerformerInHomeModel> performers = new List<PerformerInHomeModel>();
            Logic.GetPageOfPerformerList(performersSortingType, performersPageNumber, Const.PageSize).ForEach(x => performers.Add(new PerformerInHomeModel { Name = x.Name, Id = x.Id, SongCount = x.Songs.Count, ImageLink = x.ImageLink }));
            SortAndPageData sortAndPageData = new SortAndPageData { PerformersPageNumber = performersPageNumber, LasPageNumber = Convert.ToInt32(Math.Ceiling(Logic.GetPerformersCount() / 10.0)), PerformersSortingType = performersSortingType };
            HomePageModel homePageModel = new HomePageModel { Performers = performers, SortAndPageData = sortAndPageData };

            return PartialView(homePageModel);
        }


    }
}