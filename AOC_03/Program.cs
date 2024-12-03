using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Temp\data_aoc3.txt";

        List<int> starOne = new List<int>();
        List<int> starTwo = new List<int>();

        // Define the regex pattern to match mul(x, y) where x and y are between 1 and 1000
        string pattern = @"mul\((?:[1-9]|[1-9][0-9]{1,2}|1000),\s*(?:[1-9]|[1-9][0-9]{1,2}|1000)\)";


        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                foreach (Match match in Regex.Matches(line, pattern))
                {
                    var numbers = match.Value.Replace("mul(", "").Replace(")", "");
                    string[] parts = numbers.Split(",");
                     int x = Convert.ToInt32(parts[0]);
                    int y = Convert.ToInt32(parts[1]);
                    starOne.Add(x * y);


                }
            }
        }
        string dontDopattern = @"don't\(\)(.*?)do\(\)"; ;
        string replacement = "";

        try
        {
            string content = File.ReadAllText(filePath);
            string removeDonts = Regex.Replace(content, dontDopattern,replacement,RegexOptions.Singleline);
            int lastDontIndex = removeDonts.LastIndexOf("don't()");
            while (lastDontIndex != -1)
            { 
                    removeDonts = removeDonts.Substring(0, lastDontIndex);
                    lastDontIndex = removeDonts.LastIndexOf("don't()", lastDontIndex - 1);    
            }

            

            Console.WriteLine(removeDonts);

            foreach (Match match in Regex.Matches(removeDonts, pattern))
            {
                var numbers = match.Value.Replace("mul(", "").Replace(")", "");
                string[] parts = numbers.Split(",");
                int x = Convert.ToInt32(parts[0]);
                int y = Convert.ToInt32(parts[1]);
                starTwo.Add(x * y);
            }

        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex);
        }

        Console.WriteLine(starOne.Sum());
        Console.WriteLine(starTwo.Sum());
    }
}
