using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alg3.src
{
    public class Graph
    {
        private int countEdge;
        private int countNode;
        private List<Edge> edges;
        private List<Edge> ostovTree;

        private class Edge: IComparable
        {
            public int firstNode;
            public int secondNode;
            public int weight;

            public Edge(int firstNode, int secondNode, int weight)
            {
                this.firstNode = firstNode;
                this.secondNode = secondNode;
                this.weight = weight;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                    return 1;

                if (obj is Edge otherEdge)
                    return weight.CompareTo(otherEdge.weight);
                else
                    throw new ArgumentException("Object is not Edge");
            }
        }

        private class LazyLeftHeap
        {
            int key;
            LazyLeftHeap leftChild;
            LazyLeftHeap rightChild;
        }

        public Graph()
        {
            countNode = 6;
            countEdge = 8;
            edges = new List<Edge>
            {
                new Edge(0, 1, 7),
                new Edge(1, 2, 5),
                new Edge(2, 3, 6),
                new Edge(0, 3, 4),
                new Edge(1, 3, 9),
                new Edge(3, 4, 10),
                new Edge(2, 4, 13),
                new Edge(4, 5, 11)
            };
        }

        public void Test()
        {
            //Boruvki();

            Kruskal();

            int sum = 0;
            for (int i = 0; i < ostovTree.Count; i++)
            {
                Console.WriteLine(ostovTree[i].firstNode + " " + ostovTree[i].secondNode);
                sum += ostovTree[i].weight;
            }
            Console.WriteLine("Summa: " + sum);
        }

        public Graph(int countEdge, int countNode)
        {
            this.countNode = countNode;
            this.countEdge = countEdge;
            //edge = new int[countEdge, 2];
            //weight = new int[countEdge];
            //ostovTree = new int[countNode - 1, 2];
        }

        private void Boruvki()
        {
            ostovTree = new List<Edge>();
            List<List<int>> collectionTrees = new List<List<int>>();
            //индексы ребер
            int [] minEdgeForEveryCollection = new int[countNode];
            //int indexOstovTree = 0;

            for (int i = 0; i < countNode; i++)
            {
                collectionTrees.Add(new List<int> { i });
                minEdgeForEveryCollection[i] = -1;
            }

            int firstNode;
            int secondNode;
            int nameFirstCollection;
            int nameSecondCollection;

            while (FindIndexMinEdgeForEveryTree(collectionTrees, ref minEdgeForEveryCollection))
            {
                for (int s = 0; s < countNode; s++)
                {
                    if (minEdgeForEveryCollection[s] > -1)
                    {
                        firstNode = edges[minEdgeForEveryCollection[s]].firstNode;
                        secondNode = edges[minEdgeForEveryCollection[s]].secondNode;
                        nameFirstCollection = FindNameTree(collectionTrees, firstNode);
                        nameSecondCollection = FindNameTree(collectionTrees, secondNode);
                        if (nameFirstCollection != nameSecondCollection)
                        {
                            ostovTree.Add(edges[minEdgeForEveryCollection[s]]);
                            collectionTrees = MergeTwoCollections(collectionTrees,
                                nameFirstCollection, nameSecondCollection);
                            //indexOstovTree++;
                        }
                        minEdgeForEveryCollection[s] = -1;
                    }
                }
            }
        }

        private bool FindIndexMinEdgeForEveryTree(List<List<int>> collectionTrees, ref int[] result) 
        {
            bool isFindMinEdge = false;
            int firstNode;
            int secondNode;
            int nameFirstTree;
            int nameSecondTree;

            for (int i = 0; i < edges.Count; i++) //проходим по ребрам
            {
                firstNode = edges[i].firstNode;
                secondNode = edges[i].secondNode;
                nameFirstTree = FindNameTree(collectionTrees, firstNode);
                nameSecondTree = FindNameTree(collectionTrees, secondNode);
                if (nameFirstTree == -1 && nameSecondTree == -1)
                    continue;
                if (nameFirstTree != nameSecondTree)
                {
                    if (result[nameFirstTree] == -1)
                    {
                        result[nameFirstTree] = i;
                        isFindMinEdge = true;
                    }
                    else if (edges[result[nameFirstTree]].weight > edges[i].weight)
                    {
                        result[nameFirstTree] = i;
                    }
                    if (result[nameSecondTree] == -1)
                    {
                        result[nameSecondTree] = i;
                        isFindMinEdge = true;
                    }
                    else if (edges[result[nameSecondTree]].weight > edges[i].weight)
                    {
                        result[nameSecondTree] = i;
                    }
                }
            }
            return isFindMinEdge;
        }

        private int FindNameTree(List<List<int>> collectionTrees, int node)
        {
            for (int k = 0; k < collectionTrees.Count; k++)
            {
                for (int i = 0; i < collectionTrees[k].Count; i++)
                {
                    if (collectionTrees[k][i] == node)
                        return k;
                }
            }
            return -1;
        }

        private List<List<int>> MergeTwoCollections(List<List<int>> collectionTrees, int first, int second)
        {
            foreach(int a in collectionTrees[second])
            {
                collectionTrees[first].Add(a);
            }
            collectionTrees.RemoveAt(second);
            return collectionTrees;
        }

        private void Kruskal()
        {
            ostovTree = new List<Edge>();
            List<List<int>> collectionTrees = new List<List<int>>();
            //индексы ребер
            int[] minEdgeForEveryCollection = new int[countNode];
            edges.Sort();

            for (int i = 0; i < countNode; i++)
            {
                collectionTrees.Add(new List<int> { i });
                minEdgeForEveryCollection[i] = -1;
            }

            int nameFirstCollection;
            int nameSecondCollection;
            for (int i = 0; i < edges.Count; i++)
            {
                nameFirstCollection = FindNameTree(collectionTrees, edges[i].firstNode);
                nameSecondCollection = FindNameTree(collectionTrees, edges[i].secondNode);
                if (nameFirstCollection != nameSecondCollection)
                {
                    collectionTrees = MergeTwoCollections(collectionTrees,
                        nameFirstCollection, nameSecondCollection);
                    ostovTree.Add(edges[i]);
                }
            }
        }

        

        public void FirstExperiment()
        {

        }
    }
}
