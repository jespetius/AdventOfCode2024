using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

public class Program
{
    public int WordCounter(List<string> lines, string word)
    {
        int counter = 0;
        foreach (string line in lines)
        {
            int index = 0;
            while ((index = line.IndexOf(word, index)) != -1)
            {
                counter++;
                index++;
            }
        }
        return counter;
    }
    static int FindDiagonalWord(char[,] matrix, string word)
    {
        int count = 0;
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (x + word.Length <= cols && y + word.Length <= rows)
                {
                    bool found = true;
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (matrix[x + i, y + i] != word[i])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    static int FindReverseDiagonalWord(char[,] matrix, string word)
    {
        int count = 0;
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = cols - 1; x >= 0; x--)
            {
                if (x - word.Length + 1 >= 0 && y + word.Length <= rows)
                {
                    bool found = true;
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (matrix[x - i, y + i] != word[i])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }


    static void Main()
    {
        var funcs = new Program();
        string filePath = @"C:\Temp\data_aoc4.txt";
        List<string> horizontal = new List<string>();
        List<string> vertical = new List<string>();
        List<string> diagonal = new List<string>();
        List<string> diagonalReverse = new List<string>();
        string xmas = "XMAS";
        string samx = "SAMX";

        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                horizontal.Add(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the file: " + ex.Message);
        }
              var horizontalLength = horizontal[0].Length;
                for (int i = 0; i < horizontalLength; i++) 
                {
                    string verticalLine = "";
                    foreach (var line in horizontal)
                    {
                        if(i < line.Length)
                        {
                            verticalLine += line[i];
                        }
                    }
                    vertical.Add(verticalLine);
                }


        List<Tuple<int, int, char>> coordinates = new List<Tuple<int, int, char>>();
        for (int y = 0; y < horizontal.Count; y++)
        {
            for (int x = 0; x < horizontal[y].Length; x++) 
            {
                coordinates.Add(new Tuple<int, int, char>(x, y, horizontal[y][x]));
            }
        }
        Console.WriteLine(coordinates);

        int maxX = 0;
        int maxY = 0;

        foreach (var coordinate in coordinates)
        {
            maxX = Math.Max(maxX, coordinate.Item1);
            maxY = Math.Max(maxY, coordinate.Item2);
        }

        maxX++;
        maxY++;

        char[,] matrix = new char[maxX, maxY];

        for (int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                matrix[i, j] = '.'; 
            }
        }

      
        foreach (var coordinate in coordinates)
        {
            int y = coordinate.Item1 ; 
            int x = coordinate.Item2 ; 
            char c = coordinate.Item3;
            matrix[x, y] = c;
        }

       
        for (int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                Console.Write(matrix[i, j]);
            }
            Console.WriteLine();
        }




        int starOne;
        starOne = funcs.WordCounter(horizontal, xmas);
        starOne += funcs.WordCounter(horizontal, samx);
        starOne += funcs.WordCounter(vertical, xmas);
        starOne += funcs.WordCounter(vertical, samx);
        starOne += FindDiagonalWord(matrix, xmas);
        starOne += FindDiagonalWord(matrix, samx);
        starOne += FindReverseDiagonalWord(matrix, xmas);
        starOne += FindReverseDiagonalWord(matrix, samx);



        Console.WriteLine(starOne);

         HashSet<Tuple<string, string, string, string, string>> Patterns = new HashSet<Tuple<string, string, string, string, string>>()
        {
        Tuple.Create("M", "M", "A", "S", "S"),
        Tuple.Create("S", "S", "A", "M", "M"),
        Tuple.Create("M", "S", "A", "M", "S"),
        Tuple.Create("S", "M", "A", "S", "M")
        };

        // Define the offsets
       List<Tuple<int, int>> Offsets = new List<Tuple<int, int>>()
       {
        Tuple.Create(0, 0),
        Tuple.Create(0, 2),
        Tuple.Create(1, 1),
        Tuple.Create(2, 0),
        Tuple.Create(2, 2)
        };


         Tuple<string, string, string, string, string> GetPattern(int x, int y, Tuple<int, int> offset)
        {
            List<string> pattern = new List<string>();

            foreach (var o in Offsets)
            {
                int dx = o.Item1;
                int dy = o.Item2;

                // Ensure the indices are within the matrix bounds
                if (x + dx >= 0 && x + dx < matrix.GetLength(0) && y + dy >= 0 && y + dy < matrix.GetLength(1))
                {
                    pattern.Add(matrix[x + dx, y + dy].ToString());
                }
                else
                {
                    pattern.Add(""); // Empty string if out of bounds
                }
            }

            return Tuple.Create(pattern[0], pattern[1], pattern[2], pattern[3], pattern[4]);
        }

        int patternCount = 0;
        int matrixRows = matrix.GetLength(0);
        int matrixCols = matrix.GetLength(1);

        // Iterate through all positions in the matrix
        for (int x = 0; x < matrixRows - 2; x++)  // We need at least 3 rows and 3 columns for the pattern
        {
            for (int y = 0; y < matrixCols - 2; y++) // Same for columns
            {
                foreach (var offset in Offsets)
                {
                    // Get the pattern from the current (x, y) position using offsets
                    var extractedPattern = GetPattern(x, y, offset);

                    // Check if the extracted pattern matches any of the predefined patterns
                    if (Patterns.Contains(extractedPattern))
                    {
                        patternCount++;
                    }
                }
            }
        }

        Console.WriteLine("Total amount of pattern: " + patternCount/5);





    }
}