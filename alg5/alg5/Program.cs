using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alg5.logical;

namespace alg5
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph = graph.ReadFromFile("data.txt");
            graph.WriteResultToFile("result.txt");
            "data1.txt".GenerateFile();
            graph = graph.ReadFromFile("data1.txt");
            graph.WriteResultToFile("result1.txt");
        }
    }
}
