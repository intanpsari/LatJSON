using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace LatJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory(); //call the path
            
            var fileName = Path.Combine(currentDirectory, "players.json");
            
            var fileContents = DeserializedPlayer(fileName);

           foreach (var line in fileContents)
           Console.WriteLine(line.FirstName);

        }
        
        public static List<GameResult> ReadFootballResults(string fileName)
        {
            var soccerResult = new List<GameResult>();
            using (var reader = new StreamReader(fileName))
            {
                var line = "";
                reader.ReadLine();
                while((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
           
                    GameResult gr = new GameResult();
                    DateTime gameDate;
                    HomeOrAway homeOrAway;

                    if (DateTime.TryParse(values[0], out gameDate))
                        gr.GameDate = gameDate;

                    gr.TeamName = values[1];

                    if (Enum.TryParse(values[2], out homeOrAway))
                        gr.HomeOrAway = homeOrAway;

                    int parseInt;
                    if (int.TryParse(values[3], out parseInt))
                    {
                        gr.Goals = parseInt;
                    }
                    if (int.TryParse(values[4], out parseInt))
                    {
                        gr.GoalAttempts = parseInt;
                    }
                    if (int.TryParse(values[5], out parseInt))
                    {
                        gr.ShotsOnGoal = parseInt;
                    }
                    if (int.TryParse(values[6], out parseInt))
                    {
                        gr.ShotsOffGoal = parseInt;
                    }

                    Double PossessionPercent;
                    if (Double.TryParse(values[7], out PossessionPercent))
                    {
                        gr.PossessionPercent = PossessionPercent;
                    }

                    soccerResult.Add(gr);

                }
            }
            return soccerResult;
        }

        public static List<Player> DeserializedPlayer(string fileName)
        {
            var players = new List<Player>();
            var serializer = new JsonSerializer();

            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }

            return players;
        }

    }


}
