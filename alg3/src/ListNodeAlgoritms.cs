using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alg3.src
{
    internal static class ListNodeAlgoritms
    {
        public static ListNode FindLeader(this ListNode listNode)
        {
            if (listNode.parent == null)
            {
                return listNode;
            }
            else
            {
                return listNode = FindLeader(listNode.parent);
            }
        }

        public static ListNode Merge(this ListNode firstList, ListNode secondList)
        {
            FindLeader(firstList).parent = secondList;
            return firstList;
        }
    }
}
