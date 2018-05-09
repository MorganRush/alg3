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
            graph.FirstExperimentForKruskal();
            //graph.FirstExperiment();
            //graph.Test();
            //List<int> timeWork = graph.SecondExperimentForBoruvki();
            return;
        }
    }
}
