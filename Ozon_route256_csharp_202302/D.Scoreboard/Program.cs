namespace Scoreboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var scoreboard = new Scoreboard();
            var count = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < count; i++)
                if(i % 2 != 0)
                    Console.WriteLine(scoreboard.Score(Console.ReadLine() ?? string.Empty));
        }

        internal class Scoreboard
        {
            public string Score(string line)
            {
                var times = line.Split(' ').Select(int.Parse).Where(t => t > 0).ToList();
                if (times.Count == 1)
                    return "1";

                var players = times
                    .Select((t, i) => new Player(t, i))
                    .OrderBy(p => p.Time)
                    .ToList();
                
                for (int i = 0; i < times.Count; i++)
                {
                    var player = players[i];

                    if (i == 0)
                        player.Place = 1;
                    else if(player.Place == 0)
                        player.Place = i + 1;

                    if (i < times.Count - 1)
                    {
                        var nextPlayer = players[i + 1];
                        if (player.Time + 1 == nextPlayer.Time 
                            || player.Time == nextPlayer.Time)
                        {
                            nextPlayer.Place = player.Place;
                            continue;
                        }
                    }
                }

                return string.Join(" ", players.OrderBy(p => p.Id).Select(p => p.Place));
            }
        }

        internal class Player
        {
            public Player(int time, int id)
            {
                this.Time = time;
                this.Id = id;
            }

            public int Time { get; set; }

            public int Id { get; set; }

            public int Place { get; set; }
        }
    }
}