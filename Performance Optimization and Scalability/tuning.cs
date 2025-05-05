// A developer implements bubble sort, but Copilot detects it as inefficient for large datasets:

void BubbleSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

//Optimized Sorting Algorithm Using QuickSort

void QuickSort(int[] arr, int low, int high)
{
    if (low < high)
    {
        int pi = Partition(arr, low, high);
        QuickSort(arr, low, pi - 1);
        QuickSort(arr, pi + 1, high);
    }
}

int Partition(int[] arr, int low, int high)
{
    int pivot = arr[high];
    int i = (low - 1);

    for (int j = low; j < high; j++)
    {
        if (arr[j] <= pivot)
        {
            i++;
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }

    (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
    return i + 1;
}

//Performance Measurement Snippet

using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int size = 10000;
        int[] data1 = GenerateRandomArray(size);
        int[] data2 = (int[])data1.Clone();

        Stopwatch sw = new Stopwatch();

        // Bubble Sort
        sw.Start();
        BubbleSort(data1);
        sw.Stop();
        Console.WriteLine($"BubbleSort Time: {sw.ElapsedMilliseconds} ms");

        // QuickSort
        sw.Restart();
        QuickSort(data2, 0, data2.Length - 1);
        sw.Stop();
        Console.WriteLine($"QuickSort Time: {sw.ElapsedMilliseconds} ms");
    }

    static int[] GenerateRandomArray(int size)
    {
        Random rand = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < size; i++) arr[i] = rand.Next(0, 100000);
        return arr;
    }

    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        return i + 1;
    }
}


// A developer includes unused dependencies in a C# web project:

using System;
using Newtonsoft.Json; 
// using System.Data; // Unused (Remove this line)


