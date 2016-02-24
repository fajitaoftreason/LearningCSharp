using System;
using System.Text;
using LearningLib;

namespace learningcsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            LLStackQueueTestSuite();
        }

        static void LLStackQueueTestSuite()
        {
            Console.WriteLine("Hello, say something.");
            String input = Console.ReadLine();
            MyLinkedList<char> testlist = new MyLinkedList<char>();
            Stack<char> teststack = new Stack<char>();
            Queue<char> testqueue = new Queue<char>();
            foreach (char c in input)
            {
                testlist.Append(c);
                teststack.push(c);
                testqueue.enqueue(c);
            }
            Console.WriteLine("LL elements: ");
            testlist.PrintAllNodes();
            testlist.ReverseRecur();
            Console.Write("Reversed: ");

            while (teststack.isEmpty() == false)
            {
                Console.Write(teststack.pop());
            }
            {
                StringBuilder teststring = new StringBuilder();
                while (testqueue.isEmpty() == false)
                {
                    teststring.Append(testqueue.dequeue());
                }
                Console.Write("\nInput was: {0}", teststring);
            }
            Console.ReadLine();
        }


    }
}
