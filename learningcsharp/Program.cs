using System;
using System.Text;

using LinkedListLib;
using TreeLib;
using SortLib;
using GraphLib;
using StringLib;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace learningcsharp
{
    class Program
    {

        static void Main(string[] args)
        {
            graphTestSuite();

            Console.ReadLine();

        }

        static void stringTestSuite()
        {
            Console.WriteLine(stringfunctions.compare("AA", ""));
            Console.WriteLine(stringfunctions.indexOf("AAB", "B"));
            Console.WriteLine(stringfunctions.substring("AAB", 2,2));

            string haystack = "aaaaZaaaaX";
            string needle = "aaaaX";
            Console.WriteLine("searching {0} for {1}", haystack, needle);

            Console.Write("KMP Table for \n ", needle);
            int[] test = stringfunctions.kmpTable(needle.ToCharArray());
            foreach (char c in needle)
                Console.Write(c + " ");
            Console.WriteLine();
            foreach (int i in test)
                Console.Write(i + " ");
            Console.WriteLine();




            Console.WriteLine("index of string: {0}", stringfunctions.kmpSearch(haystack.ToCharArray(), needle.ToCharArray()));

        }


        static void graphTestSuite()
        {
            GraphNode<int> startNode = new GraphNode<int>(0);
            BasicGraph<int> testGraph = new BasicGraph<int>(startNode);

            var temp = new GraphNode<int>(1);
            temp.addConnection(testGraph.AllNodes[0], 10);

            temp = new GraphNode<int>(2);
            temp.addConnection(testGraph.AllNodes[0], 8);
            temp.addConnection(testGraph.AllNodes[1], 10);

            temp = new GraphNode<int>(3);
            temp.addConnection(testGraph.AllNodes[0], 9);
            temp.addConnection(testGraph.AllNodes[1], 5);
            temp.addConnection(testGraph.AllNodes[2], 8);

            temp = new GraphNode<int>(4);
            temp.addConnection(testGraph.AllNodes[0], 7);
            temp.addConnection(testGraph.AllNodes[1], 6);
            temp.addConnection(testGraph.AllNodes[2], 9);
            temp.addConnection(testGraph.AllNodes[3], 6);

            Solution bestPath = TSP.branchandBound(testGraph);

            Console.WriteLine("shortest distance is {0} via route {1}", bestPath.minCost, String.Join("-", bestPath.path));
        }

        static void reversestring()
        {
            Console.WriteLine("Enter string: ");
            String rawinput = Console.ReadLine();
            char[] input = rawinput.ToCharArray();

            Action<int, int> reversebyletter = delegate(int start, int end) 
            {
                while (start < end)
                {
                    char temp = input[start];
                    input[start] = input[end];
                    input[end] = temp;
                    start++;
                    end--;
                }
            };


            reversebyletter(0, input.Length - 1);
            int wordStart = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    input = extReverse(input, wordStart, i - 1);
                    wordStart = i + 1;
                }
            }
            input = extReverse(input , wordStart, input.Length - 1);

            Console.WriteLine(input);
            Console.ReadLine();

 

        }

        static char[] extReverse(char[] input, int start, int end)
        {
            while (start < end)
            {
                char temp = input[start];
                input[start] = input[end];
                input[end] = temp;
                start++;
                end--;
            }
            return input;
        }


        static void deletedLetters()
        {
            //get our strings
            Console.Write("Enter a string: ");
            String input = Console.ReadLine();
            Console.Write("Enter characters to delete: ");
            String todelete = Console.ReadLine();
            Dictionary<char, Boolean> deletions = new Dictionary<char, bool>();
            //process the chars to delete
            foreach (char c in todelete)
            {
                deletions[c] = true;
            }
            char[] manip = input.ToCharArray();
            int dest = 0;
            foreach (char c in manip)
            {
                if (!deletions.ContainsKey(c))
                {
                    manip[dest] = c;
                    dest++;
                }
            }
            String output = new string(manip, 0, dest);
            Console.WriteLine("output string: " + output);
            Console.ReadLine();
        }

        static void RepeatedLetter()
        {
            Console.WriteLine("Hello, say something.");
            String input = Console.ReadLine();
            int count = 0;
            Dictionary<char, int> lettercounts = new Dictionary<char, int>();
            foreach (char c in input)
            {
                count++;
                if (!lettercounts.ContainsKey(c))
                {
                    lettercounts[c] = 1;
                }
                else
                {
                    lettercounts[c]++;
                    Console.WriteLine("at position " + count + " we encountered instance #" + lettercounts[c] + " of the letter \'" + c + "\'"); 
                }

            }
            count = 0;
            foreach (char c in input)
            {
                count++;
                if (lettercounts[c] == 1)
                {
                    Console.WriteLine("first nonrepeated letter is \'" + c + "\'" + " at position " + count);
                    break;
                }
            }


            Console.ReadLine();

        }


        static void TreeTestSuite()
        {
            MyAVLTree<int> testtree = new MyAVLTree<int>();
            
            testtree.insert(10);
            testtree.insert(1010);
            testtree.insert(50);
            testtree.insert(5);
            testtree.insert(7);
            testtree.insert(0);
            testtree.insert(2);
            testtree.insert(3);
            testtree.insert(5);
            testtree.insert(6);
            Console.WriteLine("Is 6 in tree? {0}", (testtree.search(6) != null));

            Console.WriteLine("Is 5 in tree? {0}", (testtree.search(5) != null));

            Console.WriteLine("Is 17 in tree? {0}", (testtree.search(17) != null));

            Console.WriteLine("height of tree:  {0}", MyBST<int>.getHeight(testtree.head));
            Console.WriteLine("height of right subtree:  {0}", MyBST<int>.getHeight(testtree.head.right));
            Console.WriteLine("height of left subtree:  {0}", MyBST<int>.getHeight(testtree.head.left));

            Console.WriteLine("Performing rebalance.");
            testtree.reBalance();

            Console.WriteLine("height of tree:  {0}", MyBST<int>.getHeight(testtree.head));

            Console.WriteLine("height of right subtree:  {0}", MyBST<int>.getHeight(testtree.head.right));
            Console.WriteLine("height of left subtree:  {0}", MyBST<int>.getHeight(testtree.head.left));
        }

        static void LLStackQueueTestSuite()
        {
            Console.WriteLine("Hello, say something.");
            String input = Console.ReadLine();
            MyLinkedList<char> testlist = new MyLinkedList<char>();
            MyStack<char> teststack = new MyStack<char>();
            MyQueue<char> testqueue = new MyQueue<char>();
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

        static void SortTest()
        {
            int[] temp = { 0, 4, 3, 2, 1 };
            sort.MergeSort(temp, 0, 4);
            foreach (int i in temp)
            {
                Console.WriteLine(i);
            }
        }
    }
}
