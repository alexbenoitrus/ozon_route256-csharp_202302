namespace SeaWar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var validator = new Validator();
            var count = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < count; i++)
                Console.WriteLine(validator.Validate(Console.ReadLine() ?? string.Empty));
        }
    }

    public enum Result
    {
        Yes,
        No,
    }

    public class Validator
    {
        public Result Validate(string line)
        {
            int one = 0, two = 0, three = 0, four = 0;

            var ships = line.Split(' ');
            foreach (var ship in ships)
            {
                if (ship.Equals("1"))
                    one++;
                else if (ship.Equals("2"))
                    two++;
                else if (ship.Equals("3"))
                    three++;
                else if (ship.Equals("4"))
                    four++;
                else
                    return Result.No;

                if (one > 4 || two > 3 || three > 2 || four > 1)
                    return Result.No;
            }

            return Result.Yes;
        }
    }
}

