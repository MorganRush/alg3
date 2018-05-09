using System;
using System.Collections.Generic;
using System.Linq;

namespace alg1.logical
{
    public class HashTable
    {
        private List<int> countCompares = new List<int>();
        private List<int> countConflicts = new List<int>();
        private int countCompare = 0;
        private int countConflict = 0;

        private class BinaryNode
        {
            public int Key { get; private set; }

            public string Value { get; private set; }

            public BinaryNode left;

            public BinaryNode right;

            public BinaryNode(int key, string value)
            {
                Key = key;
                Value = value;
                left = null;
                right = null;
            }
        }

        private BinaryNode[] table = new BinaryNode[766];

        public bool AddElement(string value)
        {
            try
            {
                countCompare = 0;
                countConflict = 0;
                int index = value.HashFunction();
                table[index] = AddElement(value, index, table[index]);
                countCompares.Add(countCompare);
                if (countConflict != 0)
                    countConflicts.Add(1);
                else
                    countConflicts.Add(0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private BinaryNode AddElement(string value, int key, BinaryNode element)
        {
            if (element == null)
            {
                countCompare++;
                element = new BinaryNode(key, value);
                return element;
            }
            else
            {
                int resultCompare = String.Compare(value, element.Value);
                countConflict++;
                if (resultCompare == -1)
                    element.left = AddElement(value, key, element.left);
                else if (resultCompare == 0)
                    throw new Exception("This element already exist");
                element.right = AddElement(value, key, element.right);
            }
            return element;
        }

        public bool FindElement(string value)
        {
            countCompare = 0;
            countConflict = 0;
            int index = value.HashFunction();
            BinaryNode node = FindElement(value, index, table[index]);
            if (node == null)
                return false;
            else
                return true;
        }

        private BinaryNode FindElement(string value, int key, BinaryNode element)
        {
            BinaryNode result = null;
            countCompare++;
            if (element == null)
            {
                result = null;
                return result;
            }
            else
            {
                int resultCompare = String.Compare(value, element.Value);
                if (resultCompare == -1)
                {
                    countConflict++;
                    result = FindElement(value, key, element.left);
                }
                else if (resultCompare == 0)
                {
                    result = new BinaryNode(value.HashFunction(), value);
                    return result;
                }
                countConflict++;
                result = FindElement(value, key, element.right);
            }
            return result;
        }

        public int GetLastCountCompare()
        {
            return countCompare;
        }

        public int GetLastCountConflict()
        {
            return countConflict;
        }

        public double GetMiddleCountCompares()
        {
            return (double)countCompares.Sum() / countCompares.Count;
        }

        public double GetMiddleCountConflicts()
        {
            return (double)countConflicts.Sum() / countConflicts.Count;
        }
    }
}
