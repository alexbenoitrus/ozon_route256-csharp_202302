namespace TradeMarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filter = new Filter();
            var count = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < count; i++)
            {
                var n = int.Parse(Console.ReadLine() ?? "0");
                var lines = new string[n];
                for (int j = 0; j < n; j++)
                    lines[j] = Console.ReadLine();

                Console.WriteLine(filter.Do(lines));
            }
        }

        internal class Filter
        {
            public int Do(string[] lines)
            {
                return lines
                    .Select(line => new string(FindDuplicateLetters(line).ToArray()))
                    .Distinct()
                    .Count();
            }

            private List<char> FindDuplicateLetters(string line)
            {
                var letters = new List<char>();

                for (int i = 0; i < line.Length; i++)
                {
                    char current = line[i];
                    char? prev = null;
                    char? next = null;

                    if (i > 0)
                        prev = line[i - 1];

                    if (i < line.Length - 1)
                        next = line[i + 1];

                    if (prev == current)
                        continue;

                    letters.Add(current);

                    if (current == next)
                        letters.Add('\'');
                }

                return letters;
            }
        }
    }
}