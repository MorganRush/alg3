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
        //private int[,] edge;
        //private int[] weight;
        //private int[,] ostovTree;
        private int countEdge;
        private int countNode;
        private Edge[] edges;
        private Edge[] ostovTree;

        private class Edge
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
            edges = new Edge[countEdge];
            edges[0] = new Edge(0, 1, 7);
            edges[1] = new Edge(1, 2, 5);
            edges[2] = new Edge(2, 3, 6);
            edges[3] = new Edge(0, 3, 4);
            edges[4] = new Edge(1, 3, 9);
            edges[5] = new Edge(3, 4, 10);
            edges[6] = new Edge(2, 4, 13);
            edges[7] = new Edge(4, 5, 11);
            #region old
            //edge = new int[countEdge, 2];
            //weight = new int[countEdge];
            //ostovTree = new int[countNode - 1, 2];

            //edge[0, 0] = 0;
            //edge[0, 1] = 1;
            //weight[0] = 7;

            //edge[1, 0] = 1;
            //edge[1, 1] = 2;
            //weight[1] = 5;

            //edge[2, 0] = 2;
            //edge[2, 1] = 3;
            //weight[2] = 6;

            //edge[3, 0] = 0;
            //edge[3, 1] = 3;
            //weight[3] = 4;

            //edge[4, 0] = 1;
            //edge[4, 1] = 3;
            //weight[4] = 9;

            //edge[5, 0] = 3;
            //edge[5, 1] = 4;
            //weight[5] = 10;

            //edge[6, 0] = 2;
            //edge[6, 1] = 4;
            //weight[6] = 13;

            //edge[7, 0] = 4;
            //edge[7, 1] = 5;
            //weight[7] = 11;
            #endregion
        }

        public void Test()
        {
            Boruvki();

            for (int i = 0; i < ostovTree.GetLength(0); i++)
            {
                Console.WriteLine(ostovTree[i, 0] + " " + ostovTree[i, 1]);
            }
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
            List<List<int>> collectionTrees = new List<List<int>>();
            List<int> rootNodes = new List<int>();
            //индексы ребер
            int [] minEdgeForEveryCollection = new int[countNode];
            int indexOstovTree = 0;

            for (int i = 0; i < countNode; i++)
            {
                collectionTrees.Add(new List<int> { i });
                rootNodes.Add(i);
                minEdgeForEveryCollection[i] = -1;
            }

            int firstNode;
            int secondNode;
            int nameFirstCollection;
            int nameSecondCollection;

            while (FindMinEdgeForEveryTree(collectionTrees, ref minEdgeForEveryCollection))
            {
                for (int s = 0; s < countNode; s++)
                {
                    if (minEdgeForEveryCollection[s] > -1)
                    {
                        firstNode = edge[minEdgeForEveryCollection[s], 0];
                        secondNode = edge[minEdgeForEveryCollection[s], 1];
                        nameFirstCollection = FindNameTree(collectionTrees, firstNode);
                        nameSecondCollection = FindNameTree(collectionTrees, secondNode);
                        if (nameFirstCollection != nameSecondCollection)
                        {
                            ostovTree[indexOstovTree, 0] = edge[minEdgeForEveryCollection[s], 0];
                            ostovTree[indexOstovTree, 1] = edge[minEdgeForEveryCollection[s], 1];
                            collectionTrees = MergeTwoCollections(collectionTrees,
                                nameFirstCollection, nameSecondCollection);
                            indexOstovTree++;
                        }
                        minEdgeForEveryCollection[s] = -1;
                    }
                }
            }
        }

        private bool FindMinEdgeForEveryTree(List<List<int>> collectionTrees, ref int[] result) 
        {
            bool isFindMinEdge = false;
            int firstNode;
            int secondNode;
            int nameFirstTree;
            int nameSecondTree;

            for (int i = 0; i < edge.GetLength(0); i++) //проходим по ребрам
            {
                firstNode = edge[i, 0];
                secondNode = edge[i, 1];
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
                    else if (weight[result[nameFirstTree]] > weight[i])
                    {
                        result[nameFirstTree] = i;
                    }
                    if (result[nameSecondTree] == -1)
                    {
                        result[nameSecondTree] = i;
                        isFindMinEdge = true;
                    }
                    else if (weight[result[nameSecondTree]] > weight[i])
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
    }
}
