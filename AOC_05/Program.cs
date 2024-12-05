using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

public class Program
{

    static void Main()
    {
        string filePath = @"C:\Temp\data_aoc5.txt";
        List<List<int>> instList = [];
        List<Tuple<int, int>> pages = [];
        List<int> starOne = [];
        List<int> starTwo = [];
        List<List<int>> reRun = [];
        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (line.Contains('|'))
                {
                    var parts = line.Split('|');
                    int item1 = int.Parse(parts[0]);
                    int item2 = int.Parse(parts[1]);
                    Tuple<int, int> pageFromInput = new Tuple<int, int>(item1, item2);
                    pages.Add(pageFromInput);
                }
                else
                {
                    var parts = line.Split(',');
                    List<int> list = new List<int>();
                    foreach (var part in parts)
                    {
                        list.Add(int.Parse(part));
                    }
                    instList.Add(list);
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the file: " + ex.Message);
        }

        foreach (var inst in instList)
        {
            bool isvalid = true;
            for(int i = 0;  i < inst.Count; i++)    
            {
                var instItem = inst[i];
            var validTuplesForNumber = pages.Where(x => x.Item1 == instItem && inst.Contains(x.Item2)).ToList();
                if (validTuplesForNumber.Count != (inst.Count - i - 1))
                {
                    isvalid = false;
                } 
            }
            if (isvalid)
            {
                starOne.Add(inst[inst.Count / 2]);
            }
            else
            {
                reRun.Add(inst);
            }
        }
        foreach (var inst in reRun)
        {
            bool isvalid = false;
            List<int> shiftedInst = new List<int>(inst);

            while (!isvalid)
            {
                isvalid = true; 

                for (int i = 0; i < shiftedInst.Count; i++)
                {
                    var instItem = shiftedInst[i];
                    var validTuplesForNumber = pages.Where(x => x.Item1 == instItem && shiftedInst.Contains(x.Item2)).ToList();

                    if (validTuplesForNumber.Count != (shiftedInst.Count - i - 1))
                    {

                        isvalid = false;

                        if (i == shiftedInst.Count - 1)
                        {
                            var lastElement = shiftedInst[i];
                            shiftedInst.RemoveAt(i);
                            shiftedInst.Insert(0, lastElement);
                        }
                        else
                        {
                            var firstElement = shiftedInst[i];
                            shiftedInst.RemoveAt(i);
                            shiftedInst.Insert(i + 1, firstElement);
                        }
                    }
                }

                if (isvalid)
                {
                    starTwo.Add(shiftedInst[shiftedInst.Count / 2]);
                }
            }
        }




        Console.WriteLine(starOne.Sum());
        Console.WriteLine(starTwo.Sum());
    }
}