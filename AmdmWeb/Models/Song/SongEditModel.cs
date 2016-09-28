using AmdmData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmdmWeb.Models.Song
{
    public class SongEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SongPageLink { get; set; }
        public string Text { get; set; }
        public int Views { get; set; }
        public string VideoLink { get; set; }
        public int PerformerId { get; set; }
        public string Chords { get; set; }
        public int Number { get; set; }
        public SongsSortingTypes SongsSortingType { get; set; }
    }
}