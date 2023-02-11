namespace DocumentPrint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filter = new Printer();
            var count = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < count; i++)
                Console.WriteLine(filter.Inverter(int.Parse(Console.ReadLine() ?? "0"), Console.ReadLine()));
        }

        internal class Printer
        {
            public string Inverter(int count, string pattern)
            {
                var notPrinted = new List<Interval>();
                var pages = Enumerable.Range(1, count).ToDictionary(i => i, i => false);
                foreach (var interval in pattern.Split(','))
                {
                    var numbers = interval.Split('-');
                    if (numbers.Length == 2)
                    {
                        for (int i = int.Parse(numbers[0]); i <= int.Parse(numbers[1]); i++)
                        {
                            pages[i] = true;
                        }
                    }
                    else
                    {
                        pages[int.Parse(numbers[0])] = true;
                    }
                }
                
                var newInterval = new Interval(1);
                for (int i = 1; i <= count; i++)
                {
                    bool currentIsPrinted = pages[i];
                    bool prevIsPrinted = false;
                    bool nextIsPrinted = false;
                    bool hasNext = false;
                    bool hasPrev = false;

                    if (i > 1)
                    {
                        hasPrev = true;
                        prevIsPrinted = pages[i - 1];
                    }

                    if (i < count)
                    {
                        hasNext = true;
                        nextIsPrinted = pages[i + 1];
                    }

                    if(currentIsPrinted)
                        continue;

                    bool isLeftBorder = (!hasPrev || prevIsPrinted) && !currentIsPrinted;
                    bool isRightBorder = (!hasNext || nextIsPrinted) && !currentIsPrinted;

                    if (isLeftBorder)
                    {
                        newInterval = new Interval(i);
                        notPrinted.Add(newInterval);
                    }
                    else if (isRightBorder)
                    {
                        newInterval.End = i;
                    }
                }

                return string.Join(",", notPrinted);
            }
        }

        internal class Interval
        {
            public Interval(int start)
            {
                this.Start = start;
            }

            public int Start { get; set; }

            public int End { get; set; }

            public override string ToString()
            {
                return End > 0
                    ? $"{this.Start}-{this.End}"
                    : $"{Start}";
            }
        }
    }
}