using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alg5.logical
{
    public static class InitializationGraph
    {
        public static void GenerateFile(this string path)
        {
            Random random = new Random();
            StreamWriter delete = new StreamWriter(path, false);
            delete.Write("");
            delete.Close();
            StreamWriter print = new StreamWriter(path, true);
            print.WriteLine(Graph.countNode);
            print.WriteLine(Graph.countEdge);
            String content;
            int count = 10000;
            while (count != 0)
            {
                for (int i = 0; i < Graph.countNode; i++)
                {
                    int firstNode = random.Next(1, Graph.countNode + 1);
                    int firstPeriod = random.Next(Graph.minWeightOfEdge, Graph.maxWeightOfEdge);
                    int secondNode;
                    do
                    {
                        secondNode = random.Next(1, Graph.countNode + 1);
                    } while (firstNode == secondNode);
                    int secondPeriod = random.Next(Graph.minWeightOfEdge, Graph.maxWeightOfEdge);
                    content = firstNode + " " + firstPeriod + " " + secondNode + " " + secondPeriod + "\n";
                    print.WriteLine(content);
                }                
                count--;
            }
            print.Close();
        }

        private static Graph.Node AddEdge(this Graph.Node node, int name, int periodRoot, int periodThis)
        {
            if (node != null)
            {
                node.next = AddEdge(node.next, name, periodRoot, periodThis);
            }
            else
            {
                node = new Graph.Node(name, periodRoot, periodThis, null);
                return node;
            }
            return node;
        }

        public static Graph ReadFromFile(this Graph graph, string filePath)
        {
            string content = File.ReadAllText(filePath);
            string[] elements = content.Split(new string[] { "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            int countNode = Int32.Parse(elements[0]);
            int countEdge = Int32.Parse(elements[1]);
            graph.heap = new HeapD(Graph.sizeHeap, countNode);
            graph.result = new Graph.Result(countNode);
            graph.graph = new Graph.Node[countNode];

            for (int i = 0; i < countEdge; i++)
            {
                string[] edgeInformation = elements[i + 2].Split(new string[] { " " },
                    StringSplitOptions.RemoveEmptyEntries);
                if (edgeInformation.Length < 4)
                    continue;
                graph.graph[Int32.Parse(edgeInformation[0]) - 1] =
                    graph.graph[Int32.Parse(edgeInformation[0]) - 1].AddEdge(
                        Int32.Parse(edgeInformation[2]) - 1,
                        Int32.Parse(edgeInformation[1]), 
                        Int32.Parse(edgeInformation[3]));

                graph.graph[Int32.Parse(edgeInformation[2]) - 1] =
                    graph.graph[Int32.Parse(edgeInformation[2]) - 1].AddEdge(
                        Int32.Parse(edgeInformation[0]) - 1,
                        Int32.Parse(edgeInformation[3]),
                        Int32.Parse(edgeInformation[1]));
            }
            return graph;
        }
    }
}
