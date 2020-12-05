using System;
using System.Collections.Generic;
using System.Linq;

namespace Development_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> list = new List<List<int>>();
            Dictionary<int, Double> dict;
            List<Dictionary<int, Double>> containerList = new List<Dictionary<int, Double>>();
            int[] frequencyLists;

            list.Add(new List<int>() {//point 0
               536660,183800
            });
            list.Add(new List<int>() { //pont 1
               537032,184006
            });
            list.Add(new List<int>() {//point 2
               537112,181106
            });
            list.Add(new List<int>() {
               544532,133006
            });
            list.Add(new List<int>() {
               34,177706
            });
            list.Add(new List<int>() {
               4,177706
            });
            list.Add(new List<int>() {
               5532,5706
            });
            list.Add(new List<int>() {
               592,222706
            });
            list.Add(new List<int>() {
               532,16
            });
            list.Add(new List<int>() {
               502,866666

            });
            Console.WriteLine("These are all the points and their distances!");
            for (int i = 0; i < list.Count; i++)
            {
                dict = new Dictionary<int, double>();
                var reff = list[i];// our reference
                var x2 = reff[0];
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
            for (int i = 0; i < containerList[9].Count; i++)
            {

                frequencyLists[containerList[9].ElementAt(i).Key]= temp;

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
            int controlvar = 0;
            while (controlvar<containerList.Count)
            {
                var currentDictionary = containerList[controlvar];
                controlvar++;
            }
            //now let print out container list
            Console.WriteLine(String.Join("; ", frequencyLists));
            foreach (Dictionary<int, Double> currentList in containerList)
            {
                for (int i = 0; i < currentList.Count; i++)
                {
                    Console.Write(currentList.ElementAt(i).Key + " : " + currentList.ElementAt(i).Value + ", ");
                }

                //Console.WriteLine(String.Join("; ", currentList));
                Console.WriteLine();
            }

            

            Console.ReadLine();
        }
    }
}
