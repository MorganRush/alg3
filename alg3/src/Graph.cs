using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alg3.src
{
    internal class Graph
    {
        private int[,] edge;
        private int[] weight;
        private int[,] ostovTree;
        private int countEdge;
        private int countNode;

        private const int infinity = 1000001;

        private class LazyLeftHeap
        {
            int key;
            LazyLeftHeap leftChild;
            LazyLeftHeap rightChild;
        }

        public Graph(int countEdge, int countNode)
        {
            edge = new int[countEdge, 2];
            weight = new int[countEdge];
            ostovTree = new int[countNode, 2];
            this.countNode = countNode;
            this.countEdge = countEdge;
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
                collectionTrees[i] = new List<int> { i };
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
            int length = collectionTrees.Count;
            int firstNode;
            int secondNode;
            int nameFirstTree;
            int nameSecondTree;

            for (int i = 0; i < length; i++) //проходим по ребрам
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
            foreach(List<int> list in collectionTrees)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == node)
                        return i;
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
