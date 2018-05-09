using System;
using alg1.logical;

namespace alg1
{
    class Program
    {
        static void Main(string[] args)
        {
            int select = 0;
            HashTable hashTable = new HashTable();
            bool isNext = true;
            Console.WriteLine("lol".CompareTo("lol"));
            while (isNext)
            {
                Console.WriteLine("Please, select and print number:");
                Console.WriteLine("1) Read data from file");
                Console.WriteLine("2) Input element from keyboard");
                Console.WriteLine("3) Find elemnt");
                Console.WriteLine("4) Get middle time");
                Console.WriteLine("5) Exit");
                select = select.InputFromKeyBoard();
                switch (select)
                {
                    case 1:
                        {
                            Console.WriteLine("Input file name");
                            bool statusRead = hashTable.AddElementsFromFile(Console.ReadLine());
                            if (statusRead)
                            {
                                Console.WriteLine("Data was sucсessfully loaded");
                                Console.WriteLine("Middle count compares: "
                                     + Math.Round(hashTable.GetMiddleCountCompares(), 3).ToString());
                                Console.WriteLine("Middle count conflicts: "
                                    + Math.Round(hashTable.GetMiddleCountConflicts(), 3).ToString());
                            }
                            else
                            {
                                Console.WriteLine("This file does not exist");
                                Console.WriteLine("Repeat, please");
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Input value");
                            bool statusAdd = hashTable.AddElement(Console.ReadLine());
                            if (statusAdd)
                            {
                                Console.WriteLine("this element was successfully added");
                                Console.WriteLine("Count compares: " 
                                    + hashTable.GetLastCountCompare().ToString());
                                Console.WriteLine("Count conflicts: "
                                    + hashTable.GetLastCountConflict().ToString());
                            }
                            else
                            {
                                Console.WriteLine("this element already exist");
                                Console.WriteLine("Repeat, please");
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Input value");
                            bool statusFind = hashTable.FindElement(Console.ReadLine());
                            if (statusFind)
                            {
                                Console.WriteLine("This element was successefully found");
                                Console.WriteLine("Count compares: "
                                    + hashTable.GetLastCountCompare().ToString());
                                Console.WriteLine("Count conflicts: "
                                    + hashTable.GetLastCountConflict().ToString());
                            }
                            else
                            {
                                Console.WriteLine("This element does not exists");
                                Console.WriteLine("Repeat, please");
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Middle count compares: "
                                + Math.Round(hashTable.GetMiddleCountCompares(), 3).ToString());
                            Console.WriteLine("Middle count conflicts: "
                                + Math.Round(hashTable.GetMiddleCountConflicts(), 3).ToString());
                            break;
                        }
                    case 5:
                        {
                            isNext = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Repeat, please");
                            break;
                        }
                }
            }
        }
    }
}
