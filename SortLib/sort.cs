using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortLib
{
    public static class sort
    {
        public static void quickSort(int[] data, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = doPartition(data, left, right);
                quickSort(data, left, pivotIndex - 1);
                quickSort(data, pivotIndex + 1, right);
            }
        }

        private static int doPartition(int[] data, int left, int right)
        {
            int pivot = data[left];
            do
            {
                while (data[left] < pivot)
                {
                    left++;
                }

                while (data[right] > pivot)
                {
                    right--;
                }

                if (left >= right) return right;
                else swap(data, left, right);
            } while (true);

        }

        private static void swap(int[] data, int left, int right)
        {
            int temp = data[left];
            data[left] = data[right];
            data[right] = temp;
        }

        public static void MergeSort(int[] data, int left, int right)
        {
            int mid = (left + right) / 2;
            if (left >= right)
            {
                return;
            }
            else
            {
                MergeSort(data, left, mid);
                MergeSort(data, mid + 1, right);
            }
            doMerge(data, left, right);
        }

        private static void doMerge(int[] data, int left, int right)
        {
            int mid = (left + right) / 2;
            int numElements = right - left + 1;
            int[] temp = new int[numElements];
            int leftCursor = left;
            int rightCursor = mid + 1;
            int tempCursor = 0;

            while (leftCursor <= mid && rightCursor <= right)
            {
                if (data[leftCursor] <= data[rightCursor])
                {
                    temp[tempCursor++] = data[leftCursor++];
                }
                else
                {
                    temp[tempCursor++] = data[rightCursor++];
                }
            }

            while (leftCursor <= mid)
                temp[tempCursor++] = data[leftCursor++];

            while (rightCursor <= right)
                temp[tempCursor++] = data[rightCursor++];

            for (int i = 0; i < numElements; i++)
                data[left++] = temp[i];

        }
    }
}
