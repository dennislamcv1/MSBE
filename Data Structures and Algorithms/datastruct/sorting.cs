using System;
using System.Threading.Tasks;

public class Sorting
{
    private const int PARALLEL_THRESHOLD = 100_000;

    public static void QuickSort(int[] arr)
    {
        if (arr.Length >= PARALLEL_THRESHOLD)
        {
            ParallelQuickSort(arr, 0, arr.Length - 1);
        }
        else
        {
            QuickSort(arr, 0, arr.Length - 1);
        }
    }

    private static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);
            QuickSort(arr, low, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, high);
        }
    }

    private static void ParallelQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            Parallel.Invoke(
                () => ParallelQuickSort(arr, low, pivotIndex - 1),
                () => ParallelQuickSort(arr, pivotIndex + 1, high)
            );
        }
    }

    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    private static void Swap(int[] arr, int i, int j)
    {
        if (i == j) return;
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    public static void PrintArray(int[] arr)
    {
        Console.WriteLine(string.Join(", ", arr));
    }

    public static void Main()
    {
        int[] dataset = { 100, 20, 35, 2, 1, 99, 40 };

        Console.WriteLine("Before Sorting:");
        PrintArray(dataset);

        QuickSort(dataset);

        Console.WriteLine("After Sorting:");
        PrintArray(dataset);
    }
}
