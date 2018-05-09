using System;
using System.Collections.Generic;
using System.Linq;

namespace alg1.src
{
    public class BinaryTree
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

        private BinaryNode root;

        public bool IsEmpty()
        {
            return root == null;
        }

        public bool AddElement(string value)
        {
            try
            {
                countCompare = 0;
                countConflict = 0;
                root = AddElement(value, value.HashFunction(), root);
                countCompares.Add(countCompare);
                countConflicts.Add(countConflict);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private BinaryNode AddElement(string value, int key, BinaryNode element)
        {
            countCompare++;
            if (element == null)
            {
                element = new BinaryNode(key, value);
                return element;
            }
            else if (key < element.Key)
            {
                element.left = AddElement(value, key, element.left);
            }
            else if (key > element.Key)
            {
                element.right = AddElement(value, key, element.right);
            }
            else
            {
                if (String.Compare(element.Value, value) == 0)
                {
                    throw new Exception("This element already exists");
                }
                else if (value.GetHashCode() < element.Value.GetHashCode())
                {
                    element.left = AddElement(value, key, element.left);
                    countConflict++;
                }
                else if (value.GetHashCode() > element.Value.GetHashCode())
                {
                    element.right = AddElement(value, key, element.right);
                    countConflict++;
                }
                else
                {
                    throw new Exception("lol");
                }
            }
            return element;
        }

        public bool FindElement(string value)
        {
            countCompare = 0;
            countConflict = 0;
            BinaryNode node = FindElement(value, value.HashFunction(), root);
            if (node == null)
                return false;
            else
                return true;
        }

        private BinaryNode FindElement(string value, int key, BinaryNode element)
        {
            if (element == null)
            {
                countCompare++;
                return null;
            }
            else if (element.Key == key && String.Compare(element.Value, value) == 0)
            {
                countCompare++;
                return element;
            }
            else if (key < element.Key)
            {
                countCompare++;
                element = FindElement(value, key, element.left);
            }
            else if (key > element.Key)
            {
                countCompare++;
                element = FindElement(value, key, element.left);
            }
            else
            {
                if (value.GetHashCode() < element.Value.GetHashCode())
                {
                    countCompare++;
                    countConflict++;
                    element = FindElement(value, key, element.left);
                }
                else if (value.GetHashCode() > element.Value.GetHashCode())
                {
                    countCompare++;
                    countConflict++;
                    element = FindElement(value, key, element.right);
                }
                else
                    throw (new Exception("lol"));
            }
            return element;
        }

        public int GetLastCountCompare()
        {
            return countCompares[countCompares.Count - 1];
        }

        public int GetLastCountConflict()
        {
            return countConflicts[countConflicts.Count - 1];
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
