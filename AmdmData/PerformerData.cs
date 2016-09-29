using AmdmData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AmdmData
{
    public class PerformerData
    {
        
        public static List<Performers> GetPageOfPerformerList(PerformersSortingTypes performersSortingType, int performersPageNumber, int pageSize)
        {
            var performersContext = new AmdmContext().Performers.AsQueryable();


            switch (performersSortingType)
            {
                case PerformersSortingTypes.ByName:
                    performersContext = performersContext.OrderBy(x => x.Name)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case PerformersSortingTypes.ByNameBack:
                    performersContext = performersContext.OrderByDescending(x => x.Name)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case PerformersSortingTypes.BySongCount:
                    performersContext = performersContext.OrderBy(x => x.Songs.Count)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case PerformersSortingTypes.BySongCountBack:
                    performersContext = performersContext.OrderByDescending(x => x.Songs.Count);

                    performersContext = performersContext.Skip((performersPageNumber - 1) * pageSize);
                    performersContext = performersContext.Take(pageSize);
                    break;
                default:
                    break;
            }
            return performersContext.ToList();
        }
        public static int GetPerformersCount()
        {
            return new AmdmContext().Performers.Count();
        }

        /////////////////////////////////////////////////////////

        public static Performers GetPerformerById(int performerId)
        {
            return new AmdmContext().Performers.Find(performerId);
        }
        public static List<Songs> GetPageOfSongList(int performerId, SongsSortingTypes songsSortingType, int songPageNumber, int pageSize)
        {
            var songs = new AmdmContext().Songs.Where(x => x.PerformerId == performerId).AsQueryable();
            switch (songsSortingType)
            {
                case SongsSortingTypes.ByName:
                    songs = songs.OrderBy(x => x.Name)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByNameBack:
                    songs = songs.OrderByDescending(x => x.Name)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByChordCount:
                    songs = songs.OrderBy(x => x.Chords.Count)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByChordCountBack:
                    songs = songs.OrderByDescending(x => x.Chords.Count)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByViews:
                    songs = songs.OrderBy(x => x.Views)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByViewsBack:
                    songs = songs.OrderByDescending(x => x.Views)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
            }
            return songs.ToList();
        }

        public static Songs GetSongById(int songId)
        {
            return new AmdmContext().Songs.Find(songId);
        }
        public static int GetSongsCount(int performerId)
        {
            return new AmdmContext().Songs.Where(x => x.PerformerId == performerId).Count();
        }

        /////////////////////////////////////////////////////////

        public static bool EditSong(int id, string name, string text, string chords)
        {
            using (var context = new AmdmContext())
            {
                context.SaveChanges();

                var song = context.Songs.Find(id);
                song.Name = name;
                song.Text = text;

                song.Chords = new List<Chords>();
                var chordsNamesList = chords.Trim().Split(',').ToList();
                chordsNamesList = chordsNamesList.Select(x => x.Trim()).ToList();
                var chordsList = context.Chords.AsEnumerable().ToList();
                var chordsListString = chordsList.Select(x => x.Name).ToList();
                chordsNamesList = chordsListString.Intersect(chordsNamesList).ToList();
                chordsNamesList.ForEach(x => song.Chords.Add(context.Chords.SingleOrDefault(y => y.Name.Equals(x))));


                context.SaveChanges();
            }
            var ch = new AmdmContext().Songs.Find(id);
            return true;
        }
        public static bool Check(string name, List<string> chords)
        {
            bool r = false;
            chords.ForEach(x =>
            {
                if (x.Equals(name))
                {
                    r = true;
                }
            });
            return r;
        }
        public static List<string> GetChordsAsStrings(int songId)
        {

            return new AmdmContext().Songs.Find(songId).Chords.Select(x => x.Name).ToList();

        }

        /////////////////////////////////////////////////////////

        public static List<int> GetPerformersId()
        {
            return new AmdmContext().Performers.Select(x => x.Id).ToList();
        }




        public static string GetPerformerNameById(int performerId)
        {
            return new AmdmContext().Performers.Find(performerId).Name;
        }
        public static string GetAllChords()
        {
            string s = "[";
            new AmdmContext().Chords.ToList().ForEach(x => s = s + ", { value: '" + x.Name + "'}");
            s = s + "],";
            return s;
        }
        public static Chords GetChord(string name)
        {
            return new AmdmContext().Chords.FirstOrDefault(x => x.Name == name);
        }
        public static List<Chords> GetChords(string s)
        {
            var chordsNamesList = s.Split(',').ToList();
            var si = chordsNamesList.First();
            var cho = chordsNamesList.Select(x => x = x.Substring(1, x.Length - 1)).ToList();
            cho.Add(si);
            var chordsList = new AmdmContext().Chords.AsEnumerable().ToList();
            var samName = chordsList.Select(x => x.Name).ToList();
            var samName2 = samName.Intersect(cho).ToList();
            var chList = chordsList.Where(x =>
                Check(x.Name, samName2));


            return chList.ToList();
        }


        public static int GetPerformerIdBySongId(int songId)
        {
            return (int)GetSongById(songId).PerformerId;
        }
    }
}
