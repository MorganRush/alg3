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

        private enum AlgoritmMethods { Boruvki, Kruskal }

        private class ListNode
        {
            public ListNode parent;
            public int indexNodeGrap;

            public ListNode(ListNode node, int index)
            {
                parent = node;
                indexNodeGrap = index;
            }
        }

        private ListNode FindLeader(int indexListNode)
        {
            if (collectionTrees[indexListNode].parent == null)
            {
                return collectionTrees[indexListNode];
            }
            else
            {
                return collectionTrees[indexListNode] = 
                    FindLeader(collectionTrees[indexListNode].parent.indexNodeGrap);
            }
        }

        private ListNode Merge(int indexFirstList, int indexSecondList)
        {
            FindLeader(indexFirstList).parent = collectionTrees[indexSecondList];
            return collectionTrees[indexFirstList];
        }

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

        public Graph()
        {

        }

        public void Test()
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

            Boruvki();

            //Kruskal();

            int sum = 0;
            for (int i = 0; i < ostovTree.Count; i++)
            {
                Console.WriteLine(ostovTree[i].firstNode + " " + ostovTree[i].secondNode);
                sum += ostovTree[i].weight;
            }
            Console.WriteLine("Summa: " + sum);
        }

        ListNode[] collectionTrees;

        private void Boruvki()
        {
            ostovTree = new List<Edge>();
            collectionTrees = new ListNode[countNode];
            int[] sizeTrees = new int[countNode];
            int[] minEdgeForEveryCollection = new int[countNode];

            for (int i = 0; i < countNode; i++)
            {
                collectionTrees[i] = new ListNode(null, i);
                sizeTrees[i] = 1;
                minEdgeForEveryCollection[i] = -1;
            }

            int firstNode;
            int secondNode;
            int nameLiderFirstCollection;
            int nameLiderSecondCollection;

            while (FindIndexMinEdgeForEveryTree(ref minEdgeForEveryCollection))
            {
                for (int s = 0; s < countNode; s++)
                {
                    if (minEdgeForEveryCollection[s] > -1)
                    {
                        firstNode = edges[minEdgeForEveryCollection[s]].firstNode;
                        secondNode = edges[minEdgeForEveryCollection[s]].secondNode;
                        nameLiderFirstCollection = FindLeader(collectionTrees[firstNode].indexNodeGrap).indexNodeGrap;
                        nameLiderSecondCollection = FindLeader(collectionTrees[secondNode].indexNodeGrap).indexNodeGrap;
                        if (nameLiderFirstCollection != nameLiderSecondCollection)
                        {
                            ostovTree.Add(edges[minEdgeForEveryCollection[s]]);
                            if (ostovTree.Count == countNode - 1)
                                return;
                            if (sizeTrees[firstNode] < sizeTrees[secondNode])
                            {
                                collectionTrees[firstNode] = Merge(
                                    collectionTrees[firstNode].indexNodeGrap, collectionTrees[secondNode].indexNodeGrap);
                                sizeTrees[firstNode] += sizeTrees[secondNode];
                            }
                            else
                            {
                                collectionTrees[secondNode] = Merge(
                                    collectionTrees[secondNode].indexNodeGrap, collectionTrees[firstNode].indexNodeGrap);
                                sizeTrees[secondNode] += sizeTrees[firstNode];
                            }
                        }
                        minEdgeForEveryCollection[s] = -1;
                    }
                }
            }
        }

        private bool FindIndexMinEdgeForEveryTree(ref int[] result) 
        {
            bool isFindMinEdge = false;
            int firstNode;
            int secondNode;
            int nameLiderFirstCollection;
            int nameLiderSecondCollection;

            for (int i = 0; i < edges.Count; i++) //проходим по ребрам
            {
                firstNode = edges[i].firstNode;
                secondNode = edges[i].secondNode;
                nameLiderFirstCollection = FindLeader(collectionTrees[firstNode].indexNodeGrap).indexNodeGrap;
                nameLiderSecondCollection = FindLeader(collectionTrees[secondNode].indexNodeGrap).indexNodeGrap;
                //if (nameLiderFirstCollection == -1 && nameLiderSecondCollection == -1)
                //    continue;
                if (nameLiderFirstCollection != nameLiderSecondCollection)
                {
                    if (result[nameLiderFirstCollection] == -1)
                    {
                        result[nameLiderFirstCollection] = i;
                        isFindMinEdge = true;
                    }
                    else if (edges[result[nameLiderFirstCollection]].weight > edges[i].weight)
                    {
                        result[nameLiderFirstCollection] = i;
                    }
                    if (result[nameLiderSecondCollection] == -1)
                    {
                        result[nameLiderSecondCollection] = i;
                        isFindMinEdge = true;
                    }
                    else if (edges[result[nameLiderSecondCollection]].weight > edges[i].weight)
                    {
                        result[nameLiderSecondCollection] = i;
                    }
                }
            }
            return isFindMinEdge;
        }

        private void Kruskal()
        {
            ostovTree = new List<Edge>();
            collectionTrees = new ListNode[countNode];
            int[] sizeTrees = new int[countNode];
            edges.Sort();

            for (int i = 0; i < countNode; i++)
            {
                collectionTrees[i] = new ListNode(null, i);
                sizeTrees[i] = 1;
            }

            int firstNode;
            int secondNode;
            int nameLiderFirstCollection;
            int nameLiderSecondCollection;
            for (int i = 0; i < edges.Count; i++)
            {
                firstNode = edges[i].firstNode;
                secondNode = edges[i].secondNode;
                nameLiderFirstCollection = FindLeader(collectionTrees[firstNode].indexNodeGrap).indexNodeGrap;
                nameLiderSecondCollection = FindLeader(collectionTrees[secondNode].indexNodeGrap).indexNodeGrap;
                if (nameLiderFirstCollection != nameLiderSecondCollection)
                {
                    ostovTree.Add(edges[i]);
                    if (ostovTree.Count == countNode - 1)
                        return;
                    if (sizeTrees[firstNode] < sizeTrees[secondNode])
                    {
                        collectionTrees[firstNode] = Merge(
                            collectionTrees[firstNode].indexNodeGrap, collectionTrees[secondNode].indexNodeGrap);
                        sizeTrees[firstNode] += sizeTrees[secondNode];
                    }
                    else
                    {
                        collectionTrees[secondNode] = Merge(
                            collectionTrees[secondNode].indexNodeGrap, collectionTrees[firstNode].indexNodeGrap);
                        sizeTrees[secondNode] += sizeTrees[firstNode];
                    }
                }
            }
        }

        #region consts
        private const int minWeightOfEdge = 1;

        private const int maxWeightOfEdge = 1000000;

        private const int countNodeForExperiments = 10000;

        private const int firstMinCountEdge = 100000;

        private const int firstMaxCountEdge = 10000000;

        private const int firstStep = 100000;

        private const int secondStep = 1000;

        private const int secondMinCountEdge = 1000;

        private const int secondMaxCountEdge = 100000;
        #endregion

        private Random random = new Random();

        private List<int> FirstExperiment(AlgoritmMethods algoritmMethods)
        {
            edges = new List<Edge>();
            countNode = countNodeForExperiments;
            List<int> timeWork = new List<int>();
            countEdge = 0;
            for (int i = 0; i < 100; i++)
            {
                countEdge += firstStep;
                for (int j = 0; j < firstStep; j++)
                {
                    int firstNode;
                    int secondNode;
                    do
                    {
                        firstNode = random.Next(0, countNode - 1);
                        secondNode = random.Next(0, countNode - 1);
                    } while (firstNode == secondNode);
                    edges.Add(new Edge(firstNode, secondNode,
                        random.Next(minWeightOfEdge, maxWeightOfEdge)));
                }
                DateTime start = DateTime.Now;
                if (algoritmMethods == AlgoritmMethods.Boruvki)
                    Boruvki();
                else
                    Kruskal();
                DateTime end = DateTime.Now;
                timeWork.Add((end - start).Milliseconds + (end - start).Seconds * 1000);
                Console.WriteLine("Эксперимент " + i);
                Console.WriteLine("Время работы " + timeWork[i]);
            }
            return timeWork;
        }

        private List<int> SecondExperiment(AlgoritmMethods algoritmMethods)
        {
            edges = new List<Edge>();
            countNode = countNodeForExperiments;
            List<int> timeWork = new List<int>();
            countEdge = 0;
            for (int i = 0; i < 100; i++)
            {
                countEdge += secondStep;
                for (int j = 0; j < secondStep; j++)
                {
                    int firstNode;
                    int secondNode;
                    do
                    {
                        firstNode = random.Next(0, countNode - 1);
                        secondNode = random.Next(0, countNode - 1);
                    } while (firstNode == secondNode);
                    edges.Add(new Edge(firstNode, secondNode,
                        random.Next(minWeightOfEdge, maxWeightOfEdge)));
                }
                DateTime start = DateTime.Now;
                if (algoritmMethods == AlgoritmMethods.Boruvki)
                    Boruvki();
                else
                    Kruskal();
                DateTime end = DateTime.Now;
                timeWork.Add((end - start).Milliseconds + (end - start).Seconds * 1000);
                Console.WriteLine("Эксперимент " + i);
                Console.WriteLine("Время работы " + timeWork[i]);
            }
            return timeWork;
        }

        public List<int> FirstExperimentForBoruvki()
        {
            return FirstExperiment(AlgoritmMethods.Boruvki);
        }

        public List<int> FirstExperimentForKruskal()
        {
            return FirstExperiment(AlgoritmMethods.Kruskal);
        }

        public List<int> SecondExperimentForBoruvki()
        {
            return SecondExperiment(AlgoritmMethods.Boruvki);
        }

        public List<int> SecondExperimentForKruskal()
        {
            return SecondExperiment(AlgoritmMethods.Kruskal);
        }
    }
}
