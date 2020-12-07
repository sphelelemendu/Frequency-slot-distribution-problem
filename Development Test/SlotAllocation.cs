using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development_Test
{
    public class SlotAllocation
    {
        private readonly List<List<int>> _list;
        private Dictionary<int, Double> dict;
        List<Dictionary<int, Double>> containerList = new List<Dictionary<int, Double>>();
        private string _filename;
        int[] frequencyLists;
        //CONSTRUCTOR
        public SlotAllocation(string filename)
        {
            _filename = filename;
            _list = Helper.ReadExcel(_filename);
        }
        //CALCULATE DISTANCES FOR EACH AND EVERY POINT
        public void CalculateDistances()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                dict = new Dictionary<int, double>();
                var reff = _list[i];//the reference point

                var x2 = reff[0];
                var y2 = reff[1];
                for (int j = 0; j < _list.Count; j++)
                {

                    var currentPoint = _list[j];
                    var x1 = currentPoint[0];
                    var y1 = currentPoint[1];
                    var distance = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

                    dict.Add(j, distance);
                }
                dict = dict.OrderBy(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                containerList.Add(dict);
            }
        }
        //ASSIGN INITIAL FREQUENCES
        public void AssignInitialFriquencies()
        {
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
            

            Console.WriteLine("Initial Frequency Allocation");
            for (int i = 0; i < frequencyLists.Count(); i++)
            {
                Console.WriteLine(Helper.ConvertCellID(i) + " : " + frequencyLists[i]);
            }
        }
        //OPTIMIZE THE INITIAL FREQUENCY ALLOCATIONS
        public void Optimizer()
        {
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
                    var tuple = Helper.isDuplicate(closestFive);
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
        }
        //PRINT RESULTS/OUTPUT ON THE CONSOLE
        public void Print()
        {
            Console.WriteLine("Final Frequency Allocation After Optimization");
            for (int i=0;i<frequencyLists.Count();i++)
            {
                Console.WriteLine(Helper.ConvertCellID(i)+" : "+frequencyLists[i]);
            }
        
            foreach (Dictionary<int, Double> currentList in containerList)
            {
                Console.WriteLine();
                Console.WriteLine("[");
                for (int i = 0; i < currentList.Count; i++)
                {
                    Console.Write(currentList.ElementAt(i).Key + " : " + currentList.ElementAt(i).Value + ", ");
                }

            
                Console.WriteLine("]");
               
            }
            Helper.CreateSpreadSheet(frequencyLists, _list);

            Console.ReadLine();
        }
    }
}