using System;
using System.Collections.Generic;
using System.Linq;

namespace Development_Test
{
    class Program
    {
        static Tuple<Dictionary<int, int>, bool> isDuplicate(Dictionary<int, int> frequencyMap)
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
            List<List<int>> list = new List<List<int>>();
            Dictionary<int, Double> dict;
            List<Dictionary<int, Double>> containerList = new List<Dictionary<int, Double>>();
            int[] frequencyLists;

            list.Add(new List<int>() {//point 0
               111660,800
            });
            list.Add(new List<int>() { //pont 1
               537032, 184006
            });
            list.Add(new List<int>() {//point 2.
              5371,1838843
            });
            list.Add(new List<int>() {
             1,3
            });
            list.Add(new List<int>() {
               206,664685
            });
            list.Add(new List<int>() {
               537248,185016
            });
            list.Add(new List<int>() {
               537250,185020
            });
            list.Add(new List<int>() {
               537267,18
            });
            list.Add(new List<int>() {
               69,183451
            });
            list.Add(new List<int>() {
               0,184140

            });
            list.Add(new List<int>() {
              56,184927

            });
            list.Add(new List<int>() {
               537380,18

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

                frequencyLists[containerList[9].ElementAt(i).Key] = temp;

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

            Console.WriteLine("Frequency2 " + String.Join("; ", frequencyLists));
            int controlvar = 0;
            bool go = true;


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
                        Console.WriteLine("Were fixed Something!");
                        for (int i = 0; i < tuple.Item1.Count; i++)
                        {
                            frequencyLists[(tuple.Item1).ElementAt(i).Key] = (tuple.Item1).ElementAt(i).Value;
                        }
                    }
                    else
                    {


                    }


         
                    controlvar++;
                }
                Console.WriteLine("frequencyList " + String.Join("; ", frequencyLists));
              
                Console.WriteLine("freqlist2 " + String.Join("; ", freqlist2));
            }
            while (!frequencyLists.SequenceEqual(freqlist2));


            //now let print out container list
            Console.WriteLine("Frequency2 " + String.Join("; ", frequencyLists));
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
