using AmdmData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Hosting;
using System.Runtime.Caching;
using System.IO;

namespace AmdmData
{
    public class PerformerData
    {
        private static ObjectCache cache = MemoryCache.Default;
        public static List<Performers> GetPageOfPerformerList(PerformersSortingTypes performersSortingType, int performersPageNumber, int pageSize)
        {
            var count = GetPerformersCount();
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
                    performersContext = performersContext.OrderBy(x => x.Songs.Count * count + x.Id)
                        .Skip((performersPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case PerformersSortingTypes.BySongCountBack:
                    performersContext = performersContext.OrderByDescending(x => x.Songs.Count * count + x.Id);

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
            NullInt count = cache["performerCount"] as NullInt;

            if (count == null)
            {
                count = new NullInt
                {
                    Value = new AmdmContext().Performers.Count()
                };
                cache.Set("performerCount", count, null);
            }
            return count.Value;
        }

        /////////////////////////////////////////////////////////

        public static Performers GetPerformerById(int performerId)
        {
            Performers performer = cache["performer" + performerId] as Performers;
            if (performer == null)
            {
                performer = new AmdmContext().Performers.Find(performerId);
                cache.Set("performer" + performerId, performer, null);
            }
            return performer;
        }
        public static List<Songs> GetPageOfSongList(int performerId, SongsSortingTypes songsSortingType, int songPageNumber, int pageSize)
        {
            var count = GetAllSongsCount();
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
                    songs = songs.OrderBy(x => x.Chords.Count*count + x.Id)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByChordCountBack:
                    songs = songs.OrderByDescending(x => x.Chords.Count * count + x.Id)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByViews:
                    songs = songs.OrderBy(x => x.Views * count + x.Id)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
                case SongsSortingTypes.ByViewsBack:
                    songs = songs.OrderByDescending(x => x.Views * count + x.Id)
                        .Skip((songPageNumber - 1) * pageSize)
                        .Take(pageSize);
                    break;
            }
            return songs.ToList();
        }

        public static Songs GetSongById(int songId)
        {
            Songs song = cache["song" + songId] as Songs;
            if (song == null)
            {
                song = new AmdmContext().Songs.Find(songId);
                cache.Set("song" + songId, song, null);
            }
            return song;
        }
        public static int GetSongsCount(int performerId)
        {
            NullInt count = cache["songsOf" + performerId] as NullInt;

            if (count == null)
            {
                count = new NullInt
                {
                    Value = new AmdmContext().Songs.Where(x => x.PerformerId == performerId).Count()
                };
                cache.Set("songsOf" + performerId, count, null);
            }
            return count.Value;
        }
        public static int GetAllSongsCount()
        {
            NullInt count = cache["songsCount"] as NullInt;

            if (count == null)
            {
                count = new NullInt
                {
                    Value = new AmdmContext().Songs.Count()
                };
                cache.Set("songsCount", count, null);
            }
            return count.Value;
        }

        /////////////////////////////////////////////////////////

        public static bool EditSong(int songId, string name, string text, string chords)
        {
            Songs song;
            using (var context = new AmdmContext())
            {
                context.SaveChanges();
                song = context.Songs.Find(songId);
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
            cache.Set("song" + songId, song, null);

            return true;
        }
        public static List<string> GetChordsAsStrings(int songId)
        {
            List<string> listOfIds = cache["ChordsAsStrings" + songId] as List<string>;
            if (listOfIds == null)
            {
                listOfIds = new AmdmContext().Songs.Find(songId).Chords.Select(x => x.Name).ToList();
                cache.Set("ChordsAsStrings" + songId, listOfIds, null);
            }
            return listOfIds;
        }

        /////////////////////////////////////////////////////////

        public static List<int> GetPerformersId()
        {
            List<int> listOfIds = cache["PerformersId"] as List<int>;
            if (listOfIds == null)
            {
                listOfIds = new AmdmContext().Performers.AsQueryable().Select(x => x.Id).ToList();
                cache.Set("PerformersId", listOfIds, null);
            }
            return listOfIds;

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


        public static int GetPerformerIdBySongId(int songId)
        {
            return (int)GetSongById(songId).PerformerId;
        }
    }
}
