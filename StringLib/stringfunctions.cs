using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringLib
{
    public static class stringfunctions
    {
        public static int compare(string firstStr, string secondStr)
        {
            char[] first = firstStr.ToCharArray();
            char[] second = secondStr.ToCharArray();
            int longer = Math.Max(first.Length, second.Length);
            int shorter = Math.Min(first.Length, second.Length);
            for (int i = 0; i < shorter; i++)
            {
                if (first[i] != second[i])
                    return first[i] - second[i];
            }
            if (first.Length == second.Length)
            {
                return 0;
            }
            if (first.Length == longer)
                return 1;
            else return -1;
        }

        public static bool contains(string dataStr, string containsThisStr)
        {
            return (indexOf(dataStr, containsThisStr) >= 0);
        }
        
        public static int indexOf(string dataStr, string containsThisStr)
        {
            if (dataStr == null || containsThisStr == null)
                return -1;

            char[] data = dataStr.ToCharArray();
            char[] containsThis = containsThisStr.ToCharArray();

            if (containsThis.Length == 0)
                return 0;

            for (int i = 0; i < data.Length - containsThis.Length + 1; i++)
            {    //we cant start a match closer to the end of the string than the length
                if (data[i] == containsThisStr[0])
                {
                    for (int j = 0; j < containsThisStr.Length; j++)
                    {
                        if (data[i + j] != containsThisStr[j])
                            break; // no need to keep comparing if the current doesnt match
                        if (j == containsThisStr.Length - 1)
                            return i; //reached end of string without anything not matching
                    }
                }
            }
            //gotten through entire string without finding anything.
            return -1;
        }

        public static string substring(string dataStr, int start, int end)
        {
            char[] data = dataStr.ToCharArray();
            char[] result = new char[end - start + 1];
            for (int i = start; i <= end; i++)
                result[i - start] = data[i];

            return new string(result);
        }


        public static int[] kmpTable(char[] chars)
        {
            int len = chars.Length;
            int[] table = new int[len];

            if (len >= 1)
                table[0] = -1;
            if (len >= 2)
                table[1] = 0;

            for (int i = 2; i < len; i++)
            {
                table[i] = (chars[i - 1] == chars[table[i - 1]]) ? table[i - 1] + 1 : 0;
            }

            return table;
        }

        public static int kmpSearch(char[] haystack, char[] needle)
        {
            int haystackCursor = 0;
            int needleCursor = 0;

            int[] kmpT = kmpTable(needle);

            while (haystackCursor + needle.Length - 1 < haystack.Length)
            {
                Console.Write("comparing haystack index {0} ({1}) to needle index {2} ({3})", haystackCursor + needleCursor, haystack[haystackCursor + needleCursor], needleCursor, needle[needleCursor]);
                if (haystack[haystackCursor + needleCursor] == needle[needleCursor])
                {
                    Console.WriteLine(": pass");
                    if (needleCursor == needle.Length - 1)
                        return haystackCursor;
                    needleCursor++;
                }
                else
                {
                    Console.WriteLine(": fail");
                    if (kmpT[needleCursor] >= 0)
                    {
                        
                        haystackCursor += needleCursor - kmpT[needleCursor];
                        needleCursor = kmpT[needleCursor];
                        Console.WriteLine("next check is for match starting at {0}, starting at index {1} of needle", haystackCursor, needleCursor);
                    }
                    else
                    {
                        haystackCursor++;
                        needleCursor = 0;
                        Console.WriteLine("next check is for match starting at {0}, starting at index {1} of needle", haystackCursor, needleCursor);
                    }
                }
            }
            Console.WriteLine("search aborted, search term is {0} long, only {1} left in search space", needle.Length, haystack.Length - haystackCursor);
            return -1;
        }
    }
}
