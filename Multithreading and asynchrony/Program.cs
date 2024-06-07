using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введіть початок діапазону:");
        int startRange = int.Parse(Console.ReadLine());

        Console.WriteLine("Введіть кінець діапазону:");
        int endRange = int.Parse(Console.ReadLine());

        Console.WriteLine("Введіть кількість потоків:");
        int threadCount = int.Parse(Console.ReadLine());

        int rangePerThread = (endRange - startRange + 1) / threadCount;

        Thread[] threads = new Thread[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            int start = startRange + i * rangePerThread;
            int end = (i == threadCount - 1) ? endRange : start + rangePerThread - 1;

            threads[i] = new Thread(() => PrintNumbers(start, end));
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("Усі потоки завершено.");
    }

    static void PrintNumbers(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            Console.WriteLine(i);
        }
    }
}
