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


        public ActionResult Performer(int performerId = 1)//, PerformersSortingTypes performersSortingType = PerformersSortingTypes.ById, SongsSortingTypes songSortingType = SongsSortingTypes.ById, int performersPageNumber = 1, int songPageNumber = 1)
        {
            SortAndPageData sortAndPageData = new SortAndPageData
            {                
                SongsPageNumber = 1,
                LasPageNumber = Convert.ToInt32(Math.Ceiling(Logic.GetPerformersCount() / 10.0)),                
                SongsSortingType = SongsSortingTypes.ByName
            };

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
            Logic.GetPageOfSongList(performerId, sortAndPageData.SongsSortingType, sortAndPageData.SongsPageNumber, Const.PageSize)
                .ForEach(x => performer.Songs
                    .Add(new SongInPerformeModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Views = x.Views,
                        ChordsAmount = x.Chords.Count
                    })
                );
            
            PerformerPageModel performerPageModel = new PerformerPageModel
            {
                Performer = performer,
                PageSize = Const.PageSize,
                SortAndPageData = sortAndPageData
            };
            return View(performerPageModel);
        }
        [HttpGet]
        public ActionResult SongsPage(int performerId = 1,  SongsSortingTypes songSortingType = SongsSortingTypes.ById,  int songPageNumber = 1)
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
            Logic.GetPageOfSongList(performerId, songSortingType, songPageNumber, Const.PageSize)
                .ForEach(x => performer.Songs
                    .Add(new SongInPerformeModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Views = x.Views,
                        ChordsAmount = x.Chords.Count
                    })
                );
            SortAndPageData sortAndPageData = new SortAndPageData
            {                
                SongsPageNumber = songPageNumber,
                LasPageNumber = Convert.ToInt32(Math.Ceiling(Logic.GetPerformersCount() / 10.0)),                
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