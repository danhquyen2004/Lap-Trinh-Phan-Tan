using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    const int n = 10;
    static Queue<int> A = new Queue<int>();

    static Semaphore empty = new Semaphore(n, n);
    static Semaphore full = new Semaphore(0, n);

    static object bufferLock = new object();

    static Random random = new Random();

    static void Main()
    {
        int k = 3;
        int h = 2;

        for (int i = 1; i <= k; i++)
        {
            int id = i;
            new Thread(() => Producer(id)).Start();
        }

        for (int i = 1; i <= h; i++)
        {
            int id = i;
            new Thread(() => Consumer(id)).Start();
        }
    }

    static void Producer(int id)
    {
        while (true)
        {
            int value = random.Next(1000);

            empty.WaitOne();

            lock (bufferLock)
            {
                A.Enqueue(value);
                Console.WriteLine($"P{id}: {value} - {DateTime.Now:HH:mm:ss.fff}");
            }

            full.Release();

            Thread.Sleep(random.Next(300, 1000));
        }
    }

    static void Consumer(int id)
    {
        while (true)
        {
            full.WaitOne();

            int value;
            lock (bufferLock)
            {
                value = A.Dequeue();
            }

            empty.Release();

            int result = value * 2;
            Console.WriteLine($"C{id}: {value} - {result} - {DateTime.Now:HH:mm:ss.fff}");

            Thread.Sleep(random.Next(500, 1200));
        }
    }
}
