using System;
using System.IO;
using System.Linq;
using System.Threading;

class Program
{
    static int[] numbers = new int[10000];
    static int maxNumber;
    static int minNumber;
    static double averageNumber;

    static void Main(string[] args)
    {
        Random random = new Random();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(1, 10001);
        }

        Thread maxThread = new Thread(FindMax);
        Thread minThread = new Thread(FindMin);
        Thread avgThread = new Thread(FindAverage);

        maxThread.Start();
        minThread.Start();
        avgThread.Start();

        maxThread.Join();
        minThread.Join();
        avgThread.Join();

        Thread writeToFileThread = new Thread(WriteResultsToFile);
        writeToFileThread.Start();
        writeToFileThread.Join();

        Console.WriteLine("All calculations are complete and results are written to the file.");
    }

    static void FindMax()
    {
        maxNumber = numbers.Max();
        Console.WriteLine("Maximum: " + maxNumber);
    }

    static void FindMin()
    {
        minNumber = numbers.Min();
        Console.WriteLine("Minimum: " + minNumber);
    }

    static void FindAverage()
    {
        averageNumber = numbers.Average();
        Console.WriteLine("Average: " + averageNumber);
    }

    static void WriteResultsToFile()
    {
        string filePath = "results.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Set of numbers:");
            foreach (var number in numbers)
            {
                writer.WriteLine(number);
            }
            writer.WriteLine("\nCalculation results:");
            writer.WriteLine("Maximum: " + maxNumber);
            writer.WriteLine("Minimum: " + minNumber);
            writer.WriteLine("Average: " + averageNumber);
        }
        Console.WriteLine("Results written to file: " + filePath);
    }
}