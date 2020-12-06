using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Development_Test
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Please Enter file name :");
            string filename = Console.ReadLine();

            SlotAllocation slotAllocation = new SlotAllocation(filename);

            slotAllocation.CalculateDistances();

            slotAllocation.AssignInitialFriquencies();

            slotAllocation.Optimizer();

            slotAllocation.Print();
        }
    }
}
