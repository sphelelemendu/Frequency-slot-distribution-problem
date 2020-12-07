using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development_Test
{
    public static class Helper
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
            string spreadSheetPath = @"..\..\Resources\BestFrequencyAllocation.xlsx";
            
            
            FileInfo spreadsheeinfor = new FileInfo(spreadSheetPath);
            if (spreadsheeinfor.Exists)
            {
                File.Delete(spreadSheetPath);
            }
            ExcelPackage pack = new ExcelPackage(spreadsheeinfor);
            var slots = pack.Workbook.Worksheets.Add("distributionSlots");
            slots.Cells["A1"].Value = "Cell ID";
            slots.Cells["B1"].Value = "Northing";
            slots.Cells["C1"].Value = "Easting";
            slots.Cells["D1"].Value = "Frequency";
            int x = 0;
            for (int i = 2; i < list.Count + 2; i++)
            {
                slots.Cells["A" + (i).ToString()].Value = ConvertCellID(x);
                slots.Cells["B" + (i).ToString()].Value = list[x][0];
                slots.Cells["C" + (i).ToString()].Value = list[x][1];
                slots.Cells["D" + (i).ToString()].Value = freqList[x];
                x++;
            }
            pack.Save();

        }
        public static string ConvertCellID(int index)
        {
            switch (index)
            {
                case 0:
                    return "A";
                    break;
                case 1:
                    return "B";
                    break;
                case 2:
                    return "C";
                    break;
                case 3:
                    return "D";
                    break;
                case 4:
                    return "E";
                    break;
                case 5:
                    return "F";
                    break;
                case 6:
                    return "G";
                    break;
                case 7:
                    return "H";
                    break;
                case 8:
                    return "I";
                    break;
                case 9:
                    return "J";
                    break;
                case 10:
                    return "K";
                    break;
                case 11:
                    return "L";
                    break;
                case 12:
                    return "M";
                    break;
                case 13:
                    return "N";
                    break;
                case 14:
                    return "O";
                    break;
                case 15:
                    return "P";
                    break;
                case 16:
                    return "Q";
                    break;
                case 17:
                    return "R";
                    break;
                case 18:
                    return "S";
                    break;
                case 19:
                    return "T";
                    break;
                case 20:
                    return "U";
                    break;
                case 21:
                    return "V";
                    break;
                case 22:
                    return "W";
                    break;
                case 23:
                    return "X";
                    break;
                case 24:
                    return "Y";
                    break;
                case 25:
                    return "Z";
                    break;
                default:
                    return "Unknown Cell ID";
                    break;
            }
         
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
                
                for (int i = 2; i <=rowCount; i++)
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
    }
}
