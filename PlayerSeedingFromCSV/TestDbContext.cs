using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScoreBoardEx
{

    public class PlayerData
    {
        public Guid playerid;
        public string FirstName;    
        public string SecondName;
        public string GamerTag;
        public int score;

        public string GamerTagScore { get { return GamerTag + " ==> "+ score.ToString(); } }

        public PlayerData()
        {
        }
        public static PlayerData FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            PlayerData dailyValues = new PlayerData();
            dailyValues.playerid = Guid.NewGuid();
            dailyValues.FirstName = values[0];
            dailyValues.SecondName = values[1];
            dailyValues.GamerTag = values[2];
            dailyValues.score = Int32.Parse(values[3]);

            return dailyValues;
        }
    }
    // Implement IDisposable to allow using 
    public class TestDbContext :IDisposable
    {
        private bool disposed = false;
        public List<PlayerData> ScoreBoard = new List<PlayerData>();

        public TestDbContext()
        {
              ScoreBoard = File.ReadAllLines(@"Content\random Names with scores.csv")
                                           //.Skip(1) // Only needed if the first row contains the Field names
                                           .Select(v => PlayerData.FromCsv(v))
                                           .OrderByDescending(s => s.score)
                                           .ToList();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposed)
            {
                if (disposing)
                {
                    // Manual release of managed resources.
                }
                // Release unmanaged resources.
                disposed = true;
            }
        }

        public List<PlayerData> getTop(int count)
        {
            return ScoreBoard.Take(count).ToList();
        }

    }

    public class XMLContext
    {
        public static List<Game> BoardGames = new List<Game>();

        public XMLContext(string filename) {
            XDocument document = XDocument.Load(@"Content\" + filename);
            var docs = document.Descendants("boardgame");
           BoardGames = (from r in document.Descendants("boardgame")
                        select new Game
                        {
                            Id = r.Attribute("objectid").Value,
                            name = r.Element("name").Value,
                            year = (r.Element("yearpublished") != null)?Convert.ToInt32(r.Element("yearpublished").Value) : 0
                        }).ToList();

            
                }

    }

    public class Game
    {
        public string Id;
        public string name;
        public int year;

        public string GameData { get { return Id + " " + name + " ==> " + year.ToString(); } }
    }
}
