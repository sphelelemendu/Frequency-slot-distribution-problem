using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Development_Test
{
    class Program
    {
        public static void CreateFile()
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string spreadSheetPath = "test2.xlsx";
            File.Delete(spreadSheetPath);
            FileInfo spreadsheeinfor = new FileInfo(spreadSheetPath);
            ExcelPackage pack = new ExcelPackage(spreadsheeinfor);
            var slots = pack.Workbook.Worksheets.Add("distributionSlots");
            slots.Cells["A1"].Value = "Cell ID";
            slots.Cells["B1"].Value = "Northing";
            slots.Cells["C1"].Value = "Easting";
            slots.Cells["D1"].Value = "Frequency";
            for (int i = 0; i < 100; i++)
            {
                var somerandom = new Random();

                slots.Cells["A" + (i).ToString()].Value = i;
                slots.Cells["B" + (i).ToString()].Value = (somerandom.Next() * 50000);
                slots.Cells["C" + (i).ToString()].Value = (somerandom.Next() * 50000);
                slots.Cells["A" + (i).ToString()].Value = 0;
            }
            pack.Save();

        }
        public static void CreateSpreadSheet(int[] freqList, List<List<int>> list)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string spreadSheetPath = "BestFrequencySlotsAllocations.xlsx";
            File.Delete(spreadSheetPath);
            FileInfo spreadsheeinfor = new FileInfo(spreadSheetPath);
            ExcelPackage pack = new ExcelPackage(spreadsheeinfor);
            var slots = pack.Workbook.Worksheets.Add("distributionSlots");
            slots.Cells["A1"].Value = "Cell ID";
            slots.Cells["B1"].Value = "Northing";
            slots.Cells["C1"].Value = "Easting";
            slots.Cells["D1"].Value = "Frequency";
            int x = 0;
            for (int i = 2; i < list.Count + 2; i++)
            {
                slots.Cells["A" + (i).ToString()].Value = x;
                slots.Cells["B" + (i).ToString()].Value = list[x][0];
                slots.Cells["C" + (i).ToString()].Value = list[x][1];
                slots.Cells["D" + (i).ToString()].Value = freqList[x];


                x++;
            }
            pack.Save();

        }
        public static List<List<int>> ReadExcel(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var list = new List<List<int>>();

            FileInfo existingFile = new FileInfo(path);
            using (ExcelPackage pack = new ExcelPackage(existingFile))
            {
                ExcelWorksheet work = pack.Workbook.Worksheets[0];
                int colCount = work.Dimension.End.Column;
                int rowCount = work.Dimension.End.Row;
                for (int i = 2; i <= rowCount; i++)
                {
                    List<int> temp = new List<int>();
                    for (int j = 2; j <= 3; j++)
                    {
                        try
                        {

                            temp.Add(Int32.Parse(work.Cells[i, j].Value.ToString().Trim()));
                        }

                        catch (FormatException e)
                        {
                            Console.WriteLine($"Could not parse'{work.Cells[i, j].Value.ToString()}'");
                        }
                    }

                    list.Add(temp);


                }
            }
            return list;
        }
        public static Tuple<Dictionary<int, int>, bool> isDuplicate(Dictionary<int, int> frequencyMap)
        {
            var needsUpdate = false;
            var completeList = new List<int> { 110, 111, 112, 113, 114, 115 };
            var duplicates = new List<int>();

            var distinctList = frequencyMap.Values.Distinct().ToList();
            var difference = completeList.Except(distinctList).ToList();

            if (difference.Count() == 0)//Come back to it for when checking efficiency
            {
                //ther are no duplicates
            }
            else
            {
                needsUpdate = true;
                var tempList = new List<int>();
                for (int i = 0; i < frequencyMap.Count; i++)
                {

                    if (tempList.Contains(frequencyMap.ElementAt(i).Value))
                    {
                        //the we have a duplicate

                        Console.WriteLine();

                        var tempVar = difference.First();
                        frequencyMap[frequencyMap.ElementAt(i).Key] = tempVar;
                        difference.RemoveAt(0);
                    }
                    else
                    {

                        tempList.Add(frequencyMap.ElementAt(i).Value);
                    }
                }

            }
            return Tuple.Create(frequencyMap, needsUpdate);
        }
        static void Main(string[] args)
        {

            List<List<int>> list = null;
            Dictionary<int, Double> dict;
            List<Dictionary<int, Double>> containerList = new List<Dictionary<int, Double>>();
            string filename = "";
            Console.WriteLine("Please Enter file name :");
            filename = Console.ReadLine();

            int[] frequencyLists;
            list = ReadExcel(filename);
            for (int i = 0; i < list.Count; i++)
            {
                dict = new Dictionary<int, double>();
                var reff = list[i];// our reference

                var x2 = reff[0];//index out of range
                var y2 = reff[1];
                for (int j = 0; j < list.Count; j++)
                {

                    var currentPoint = list[j];
                    var x1 = currentPoint[0];
                    var y1 = currentPoint[1];
                    var distance = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

                    dict.Add(j, distance);
                }
                dict = dict.OrderBy(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                containerList.Add(dict);
                //Console.WriteLine(distancesMap);
            }
            //
            frequencyLists = new int[containerList[0].Count];
            var temp = 110;
            for (int i = 0; i < containerList[0].Count; i++)
            {

                frequencyLists[containerList[0].ElementAt(i).Key] = temp;

                if (temp == 115)
                {
                    temp = 110;
                }
                else
                {
                    temp++;
                }
            }
            //initializations

            Console.WriteLine("initial frequency list ! " + String.Join("; ", frequencyLists));
            int controlvar = 0;



            int[] freqlist2;
            do
            {
                freqlist2 = (int[])frequencyLists.Clone();


                while (controlvar < containerList.Count)
                {
                    var currentDictionary = containerList[controlvar];
                    var closestFive = new Dictionary<int, int>();
                    for (var dictController = 0; dictController < 5; dictController++)
                    {
                        closestFive.Add(currentDictionary.ElementAt(dictController).Key, frequencyLists[currentDictionary.ElementAt(dictController).Key]);

                    }
                    var tuple = isDuplicate(closestFive);
                    if (tuple.Item2)
                    {

                        for (int i = 0; i < tuple.Item1.Count; i++)
                        {
                            frequencyLists[(tuple.Item1).ElementAt(i).Key] = (tuple.Item1).ElementAt(i).Value;
                        }
                    }
                    controlvar++;
                }

            }
            while (!frequencyLists.SequenceEqual(freqlist2));


            //now let print out container list
            Console.WriteLine("updated frequency list ! " + String.Join("; ", frequencyLists));
            foreach (Dictionary<int, Double> currentList in containerList)
            {
                for (int i = 0; i < currentList.Count; i++)
                {
                    Console.Write(currentList.ElementAt(i).Key + " : " + currentList.ElementAt(i).Value + ", ");
                }

                //Console.WriteLine(String.Join("; ", currentList));
                Console.WriteLine();
            }
            CreateSpreadSheet(frequencyLists, list);

            Console.ReadLine();
        }
    }
}
