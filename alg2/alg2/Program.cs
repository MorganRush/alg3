using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alg2.src;

namespace alg2
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.GetResult();
            //graph.FirstExperimentForDijkstraDHeap();
            //graph.FirstExperimentForFordBellman();
            //graph.SecondExperimentForDijkstraDHeap();
            graph.SecondExperimentForFordBellman();
        }
    }
}
