using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmdmLogic;
using AmdmData.Enums;
using AmdmData;
using AmdmWeb.Models;
using AmdmWeb.Models.Performer;

namespace AmdmWeb.Controllers
{
    public class PerformerController : Controller
    {


        public ActionResult Performer(int performerId = 1)
        {
            var per = Logic.GetPerformerById(performerId);
            PerformerModel performer = new PerformerModel
            {
                Id = per.Id,
                BiographyText = per.BiographyText,
                ImageLink = per.ImageLink,
                Name = per.Name,
                PerformerPageLink = per.PerformerPageLink
            };

            PerformerPageModel performerPageModel = new PerformerPageModel
            {
                Performer = performer
            };
            return View(performerPageModel);
        }
        [HttpGet]
        public ActionResult SongsPage(int performerId = 1, SongsSortingTypes songSortingType = SongsSortingTypes.ByName, int songPageNumber = 1)
        {
            var per = Logic.GetPerformerById(performerId);
            PerformerModel performer = new PerformerModel
            {
                Id = per.Id,
                BiographyText = per.BiographyText,
                ImageLink = per.ImageLink,
                Name = per.Name,
                PerformerPageLink = per.PerformerPageLink,
                Songs = new List<SongInPerformeModel>()
            };
            int n = 1;
            Logic.GetPageOfSongList(performerId, songSortingType, songPageNumber, Const.PageSize)
                .ForEach(x => performer.Songs
                    .Add(new SongInPerformeModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Views = x.Views,
                        ChordsAmount = x.Chords.Count,
                        NumberOnThePage = n++
                    })
                );
            SortAndPageData sortAndPageData = new SortAndPageData
            {
                SongsPageNumber = songPageNumber,
                LasPageNumber = Convert.ToInt32(Math.Ceiling(Logic.GetSongsCount(performerId) / 10.0)),
                SongsSortingType = songSortingType
            };
            PerformerPageModel performerPageModel = new PerformerPageModel
            {
                Performer = performer,
                PageSize = Const.PageSize,
                SortAndPageData = sortAndPageData
            };
            return PartialView(performerPageModel);
        }
    }
}