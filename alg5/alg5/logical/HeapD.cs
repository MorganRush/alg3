using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alg5.logical
{
    internal class HeapD
    {
        public int[] keys;
        public int[] names;
        public int[] index;
        public int d;
        public int countNotVisit;

        public HeapD(int d, int length)
        {
            this.d = d;
            countNotVisit = length;
            keys = new int[length];
            names = new int[length];
            index = new int[length];
        }
    }
}
