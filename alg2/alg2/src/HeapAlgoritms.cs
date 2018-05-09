using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alg2.src
{
    internal static class HeapAlgoritms
    {
        public static int GetMin(int value1, int value2)
        {
            if (value1 < value2)
                return value1;
            else
                return value2;
        }

        public static int GetIndexFirstChild(int arrayLength, int d, int thisIndex)
        {
            int result = thisIndex * d + 1;
            if (result > arrayLength - 1)
            {
                result = -1;
            }
            return result;
        }

        public static int GetIndexLastChild(int arrayLength, int d, int thisIndex)
        {
            int result = GetIndexFirstChild(arrayLength, d, thisIndex);
            if (result != -1)
            {
                result = GetMin(arrayLength - 1, result + d - 1);
            }
            return result;
        }

        public static int GetIndexFather(int thisIndex, int d)
        {
            return (thisIndex - 1) / d;
        }

        public static int GetIndexMinChild(this HeapD heap, int thisIndex)
        {
            int indexFisrtChild = GetIndexFirstChild(heap.countNotVisit, heap.d, thisIndex);
            int result = indexFisrtChild;
            if (result != -1)
            {
                int indexLastChild = GetIndexLastChild(heap.countNotVisit, heap.d, thisIndex);
                int minKey = heap.keys[indexFisrtChild];
                for (int i = indexFisrtChild + 1; i <= indexLastChild; i++)
                {
                    if (heap.keys[i] < minKey)
                    {
                        minKey = heap.keys[i];
                        result = i;
                    }
                }
            }
            else
            {
                result = thisIndex;
            }
            return result;
        }

        //всплытие
        public static void Emersion(this HeapD heap, int thisIndex)
        {
            int indexFather = GetIndexFather(thisIndex, heap.d);
            int thisKey = heap.keys[thisIndex];
            int thisName = heap.names[thisIndex];
            int i = thisIndex;
            while (i != 0 && heap.keys[indexFather] > thisKey)
            {
                heap.keys[i] = heap.keys[indexFather];
                heap.names[i] = heap.names[indexFather];
                heap.index[heap.names[i]] = i;
                i = indexFather;
                indexFather = GetIndexFather(i, heap.d);
            }
            heap.keys[i] = thisKey;
            heap.names[i] = thisName;
            heap.index[heap.names[i]] = i;
        }

        //погружение
        public static void Dive(this HeapD heap, int thisIndex)
        {
            int indexChild = heap.GetIndexMinChild(thisIndex);
            int thisKey = heap.keys[thisIndex];
            int thisName = heap.names[thisIndex];
            int i = thisIndex;
            while (indexChild != i && thisKey > heap.keys[indexChild])
            {
                heap.keys[i] = heap.keys[indexChild];
                heap.names[i] = heap.names[indexChild];
                heap.index[heap.names[i]] = i;
                i = indexChild;
                indexChild = heap.GetIndexMinChild(i);
            }
            heap.keys[i] = thisKey;
            heap.names[i] = thisName;
            heap.index[heap.names[i]] = i;
        }

        public static void DeliteMin(this HeapD heap)
        {
            int name0 = heap.names[0];
            int key0 = heap.keys[0];
            heap.names[0] = heap.names[heap.countNotVisit - 1];
            heap.keys[0] = heap.keys[heap.countNotVisit - 1];
            heap.names[heap.countNotVisit - 1] = name0;
            heap.keys[heap.countNotVisit - 1] = key0;
            heap.countNotVisit--;
            if (heap.countNotVisit > 0)
                heap.Dive(0);
        }
    }
}
