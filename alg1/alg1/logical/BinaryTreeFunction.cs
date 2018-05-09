using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace alg1.logical
{
    public static class BinaryTreeFunction
    {
        public static int HashFunction(this string value)
        {
            string removeValue;
            int result = 0;
            if (value.Length < 4)
                removeValue = value;
            else
                removeValue = value.Remove(3);
            byte[] bytes = ASCIIEncoding.UTF8.GetBytes(removeValue);
            for (int i = 0; i < bytes.Length; i++)
                result += bytes[i];
            return result;
        }

        public static bool AddElementsFromFile(this HashTable hashTable, string filePath)
        {
            try
            {
                if (hashTable == null)
                    hashTable = new HashTable();
                string content = File.ReadAllText(filePath);
                string[] elements = content.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < elements.Length; i++)
                {
                    hashTable.AddElement(elements[i]);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int InputFromKeyBoard(this int result)
        {
            bool isNumber = false;
            while (!isNumber)
            {
                isNumber = Int32.TryParse(Console.ReadLine(), out result);
                if (!isNumber)
                    Console.WriteLine("Repeate, plese");
            }
            return result;
        }
    }
}
