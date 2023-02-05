namespace CarNumbers
{
    public class Program
    {
        private const string testinput = @"R48FAO00OOO0OOA99OKA99OK
R48FAO00OOO0OOA99OKA99O
A9PQ
A9PQA
A99AAA99AAA99AAA99AA
AP9QA";

        public static void Main(string[] args)
        {
            var pattern1 = new Pattern(Type.Letter, Type.Numeric, Type.Letter, Type.Letter);
            var pattern2 = new Pattern(Type.Letter, Type.Numeric, Type.Numeric, Type.Letter, Type.Letter);

            var parser = new Parser(pattern1, pattern2);

            var a = testinput.Split("\r\n");
            foreach (var line in a)
            {
                Console.WriteLine(parser.Parse(line));
            }

            var count = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < count; i++)
                Console.WriteLine(parser.Parse(Console.ReadLine() ?? string.Empty));
        }
    }

    internal class Parser
    {
        private const string EmptyResult = "-";
        private const char Space = ' ';

        private readonly List<Pattern> _patterns; 
        
        public Parser(params Pattern[] patterns)
        {
            _patterns = patterns.OrderBy(p => p.Length).ToList();
        }

        public string Parse(string line)
        {
            var result = new List<char>();

            for (int i = 0; i < line.Length;)
            {
                var oldSize = result.Count;

                foreach (var pattern in _patterns)
                {
                    if (i + pattern.Length > line.Length)
                    {
                        return EmptyResult;
                    }
                    
                    if (pattern.IsMatch(line, i))
                    {
                        result.AddRange(line[i..(i + pattern.Length)]);
                        result.Add(Space);

                        i += pattern.Length;
                        break;
                    }
                }

                if (oldSize == result.Count)
                    return EmptyResult;
            }

            return string.Join(string.Empty, result);
        }
    }

    internal static class Extension
    {
        public static bool IsLetter(this char symbol)
        {
            return (int)symbol is >= 65 and <= 90 or >= 97 and <= 122;
        }

        public static bool IsNumeric(this char symbol)
        {
            return (int)symbol is >= 48 and <= 57;
        }
    }

    internal enum Type
    {
        Letter,
        Numeric
    }

    internal class Pattern
    {
        private readonly Type[] _pattern;

        public Pattern(params Type[] pattern)
        {
            _pattern = pattern;
            this.Length = pattern.Length;
        }

        public int Length { get; }

        public bool IsMatch(string data, int start)
        {
            if(data.Length < start + this.Length)
                return false;

            for (int i = 0; i < this.Length; i++)
            {
                if (!IsMatch(data[start + i], i))
                    return false;
            }

            return true;
        }

        private bool IsMatch(char c, int position)
        {
            return _pattern[position] switch
            {
                Type.Letter => c.IsLetter(),
                Type.Numeric => c.IsNumeric(),
                _ => false
            };
        }
    }
}