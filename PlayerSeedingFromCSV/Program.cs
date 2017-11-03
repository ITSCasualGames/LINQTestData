using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreBoardEx;

namespace PlayerSeedingFromCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDbContext db = new ScoreBoardEx.TestDbContext();
            foreach (var item in db.getTop(5))
                Console.WriteLine("{0}", item.GamerTagScore);

            XMLContext games = new XMLContext("BoardGames.xml");
            foreach (var item in XMLContext.BoardGames)
            {
                Console.WriteLine("{0}", item.GameData);
            }
            Console.ReadKey();
        }
    }
}
