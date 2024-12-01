using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Temp\data.csv";

        List<int> leftList = new List<int>();
        List<int> leftList2 = new List<int>();
        List<int> rightList = new List<int>();
        List<int> rightList2 = new List<int>();

        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var columns = line.Split(';');

                if (columns.Length == 2)
                {
                    leftList.Add(int.Parse(columns[0].Trim()));
                    leftList2.Add(int.Parse(columns[0].Trim()));
                    rightList.Add(int.Parse(columns[1].Trim()));
                    rightList2.Add(int.Parse(columns[1].Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the file: " + ex.Message);
        }

        leftList.Sort();
        rightList.Sort();

        List<int> starOne = new List<int>();

        int listLength = Math.Min(leftList.Count, rightList.Count);
        for (int i = 0; i < listLength; i++)
        {
            starOne.Add(Math.Abs(leftList[i] - rightList[i]));
        }
        List<int> starTwo = new List<int>();
        Console.WriteLine(starOne.Sum());

        for (int i = 0; i < listLength; i++)
        {
            var compareInt = leftList2[i];
            var countInRight = rightList2.Where(x => x == compareInt).Count();

            var result = compareInt * countInRight;
            starTwo.Add(result);

        }
        Console.WriteLine(starTwo.Sum());

    }
}
