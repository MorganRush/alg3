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
            graph.Test();
            return;
            List<int> list1 = new List<int> { 1, 2, 3 };
            List<int> list2 = list1;
            list2.RemoveAt(0);
            foreach(int a in list2)
            {
                Console.WriteLine(a);
            }
        }
    }
}
