using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcollection;

namespace ThreadedCollectionTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rand = new Random();
            Console.WriteLine("test");
            ThreadedCollection<AllocationObject> tcol = new ThreadedCollection<AllocationObject>();

            bool showresults = false;

            //populate the ThreadCollection
            for(int i = 0; i< 20000; ++i)
            {
                tcol.Add(new AllocationObject(i, rand.Next(35)));
            }
            tcol.LogDuration = true; //log how long it takes to process these

            tcol.Process();

            if (showresults)
            {
                foreach (AllocationObject x in tcol)
                {
                    Console.WriteLine($"{x.Id}: qty {x.qtyNeeded} : picked {x.pickedStore}");
                }
            }
            Console.WriteLine($"Async Took {tcol.Duration.TotalMilliseconds}ms");

            tcol.ProcessNoThreading();

            if (showresults)
            {
                foreach (AllocationObject x in tcol)
                {
                    Console.WriteLine($"{x.Id}: qty {x.qtyNeeded} : picked {x.pickedStore}");
                }
            }
            Console.WriteLine($"Non Async Took {tcol.Duration.TotalMilliseconds}ms");
        }
    }
}
