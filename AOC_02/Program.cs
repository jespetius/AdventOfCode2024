using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Temp\data_aoc2.csv";
        List<DataRow> data = new List<DataRow>();

        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {

                string[] dataValues = line.Split(';');
                DataRow csvdata = new DataRow();
                foreach (var value in dataValues)
                {
                    if (int.TryParse(value, out int parsedValue))
                    {
                        csvdata.Values.Add(parsedValue);
                    }
                }
                data.Add(csvdata);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the file: " + ex.Message);
        }
        var starOne = new List<string>();
        var starTwo = new List<string>();
        foreach (DataRow row in data)
        {
            bool isAsc = true;
            bool isDesc = true;
            bool isvalid=true;

            for (int i = 1; i < row.Values.Count; i++)
            {
                int diff = Math.Abs(row.Values[i] - row.Values[i - 1]);

                if (diff < 1 || diff > 3)
                {
                    isvalid = false;
                }

                if (row.Values[i] < row.Values[i - 1])
                {
                    isAsc= false;
                }
                if (row.Values[i] > row.Values[i - 1])
                {
                    isDesc =false;
                }
            }
            if (isAsc && isvalid)
            {
                starOne.Add("safe");
                starTwo.Add("safe");

            }
            else if (isDesc && isvalid)
            {
                starOne.Add("safe");
                starTwo.Add("safe");

            }
            else
            {
                var isokay = TryRemoveBadNumber(row.Values);
                if (isokay) starTwo.Add("safe");
            }



        }
        Console.WriteLine(starOne.Where(x => x == "safe").Count());
        Console.WriteLine(starTwo.Where(x => x == "safe").Count());


    }
    static bool TryRemoveBadNumber(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            List<int> newNumbers = new List<int>(numbers);
            newNumbers.RemoveAt(i);

            bool isAsc = true;
            bool isDesc = true;
            bool isValid = true;

            for (int j = 1; j < newNumbers.Count; j++)
            {
                int diff = Math.Abs(newNumbers[j] - newNumbers[j - 1]);

                if (diff < 1 || diff > 3)
                {
                    isValid = false;
                    break;
                }

                if (newNumbers[j] < newNumbers[j - 1])
                {
                    isAsc = false;
                }
                if (newNumbers[j] > newNumbers[j - 1])
                {
                    isDesc = false;
                }
            }
            if (isValid && (isAsc || isDesc))
            {
                return true;
            }
        }
        return false;
    }

    public class DataRow
    {

        public List<int> Values { get; set; }

        public DataRow()
        {
            Values = new List<int>();   
        }
    }
}
