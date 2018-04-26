using System;
using System.Collections.Generic;
using alg3.src;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alg3
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.FirstExperiment();
            //graph.Test();
            return;
            LinkedList<int> list1 = new LinkedList<int>();
            list1.AddLast(1);
            list1.AddLast(2);
            list1.AddLast(3);
            list1.AddFirst(4);
            LinkedList<int> list2 = new LinkedList<int>();
            list2.AddLast(6);
            list2.AddLast(7);
            list2.AddLast(8);

            IEnumerable<int> enumerable = list1.Concat(list2);
            list1 = new LinkedList<int>(enumerable);

            foreach (int a in list1)
            {
                Console.WriteLine(a);
            }
            return;
        }
    }
}
