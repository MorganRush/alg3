using System;
using System.Collections.Generic;
using System.IO;

namespace alg5.logical
{
    public class Graph
    {
        #region consts
        internal const int sizeHeap = 7;

        internal const int infinity = 10000000;

        internal const int minWeightOfEdge = 1;

        internal const int countNode = 100;

        internal const int countEdge = 100;

        internal const int maxWeightOfEdge = 1000;
        #endregion

        internal class Node
        {
            public int name;
            public int weight;
            public Node next;
            public Node(int name, int periodRoot, int periodThis, Node next)
            {
                this.name = name;
                weight = GetMinGeneralMultiple(periodRoot, periodThis);
                this.next = next;
            }

            //нок
            private int GetMinGeneralMultiple(int first, int second)
            {
                return first * second / GetGeneralDivider(first, second);
            }

            //нод
            private int GetGeneralDivider(int first, int second)
            {
                while (first != second)
                {
                    if (first > second)
                    {
                        first -= second;
                    }
                    else
                    {
                        second -= first;
                    }
                }
                return first;
            }
        }

        internal class Result
        {
            public int[] dist;

            public int[] up;

            public Result(int length)
            {
                dist = new int[length];
                up = new int[length];
            }
        }

        internal Node[] graph;

        internal Result result;

        internal HeapD heap;

        private void DijkstraDHeap(int indexNode)
        {
            heap.countNotVisit = heap.names.Length;
            for (int i = 0; i < heap.countNotVisit; i++)
            {
                result.up[i] = 0;
                result.dist[i] = infinity;
                heap.names[i] = i;
                heap.keys[i] = infinity;
                heap.index[i] = i;
            }
            if (indexNode >= result.dist.Length)
            {
                return;
            }
            heap.keys[indexNode] = 0;
            heap.Emersion(indexNode);
            while (heap.countNotVisit > 0)
            {
                int i = heap.names[0];
                result.dist[i] = heap.keys[0];
                heap.DeliteMin();
                Node node = graph[i];
                while (node != null)
                {
                    int j = node.name;
                    int index = heap.index[j];
                    if (result.dist[j] == infinity)
                    {
                        int newTime;
                        if (result.dist[i] == 0 || result.dist[i] % node.weight == 0)
                        {
                            newTime = result.dist[i] + node.weight;
                        }
                        else 
                        {
                            newTime = result.dist[i] + node.weight - 
                                (result.dist[i] % node.weight);
                        }
                        if (heap.keys[index] > newTime)
                        {
                            heap.keys[index] = newTime;
                            heap.Emersion(index);
                            result.up[j] = i;
                        }
                    }
                    node = node.next;
                }
            }
        }

        public void GetResult()
        {
            int length = graph.Length;
            DijkstraDHeap(0);
            Console.WriteLine("Результат:");
            double time = result.dist[length - 1] + 0.5;
            Console.WriteLine("Оптимальное время: " + time);
            int index = length - 1;
            Console.WriteLine("Искомая последовательность: ");
            while (index != 0)
            {
                Console.WriteLine(index + 1);
                index = result.up[index];
            }
        }

        public void WriteResultToFile(string path)
        {
            int length = graph.Length;
            DijkstraDHeap(0);
            double time = result.dist[length - 1] + 0.5;
            string content =  "Оптимальное время: " + time + "; Искомая последовательность:";
            int index = length - 1;
            List<int> sequence = new List<int>();
            while (index != 0)
            {
                sequence.Add(index + 1);
                index = result.up[index];
            }
            for(int i = sequence.Count - 1; i >= 0; i--)
            {
                content += " " + sequence[i];
            }
            File.WriteAllText(path, content);
        }

        public void Test1()
        {        
            int length = 4;
            heap = new HeapD(sizeHeap, length);
            result = new Result(length);
            graph = new Node[length];
            graph[0] = new Node(2, 5, 2,
                new Node(1, 1, 1, null));
            graph[1] = new Node(0, 1, 1, 
                new Node(2, 5, 5, null));
            graph[2] = new Node(0, 2, 5,
                new Node(1, 5, 5,
                new Node(3, 2, 4, 
                new Node(3, 5, 5, null))));
            graph[3] = new Node(2, 4, 2, 
                new Node(2, 5, 5, null));
        }
    }
}
