using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int n = 150;
        int k = 3;

        // Cau 1
        List<int> A = new List<int>();
        Random random = new Random();
        for (int i = 0; i < n; i++)
        {
            A.Add(random.Next(0, 500));
        }
        A[A.Count - 1] = 1000;

        // Cau 2
        
        List<Thread> threads = new List<Thread>();

        //for (int i = 0; i < k; i++)
        //{
        //    Thread t = new Thread(() =>
        //    {
        //        int result = A.Max();
        //        Console.WriteLine(result);
        //    });
        //    threads.Add(t);
        //}
        //foreach(Thread t in threads)
        //{
        //    t.Start();
        //}    

        // Cau 3

        int size = A.Count / k;

        List<int> results = new List<int>();
        for (int i = 0; i < k; i++)
        {
            int start = i * size;
            int end = (i == k - 1) ? A.Count : start + size;

            int stt = i + 1;
            Thread t = new Thread(() =>
            {
                int max = TimLonNhat(A, start, end);
                results.Add(max);

                DateTime now = DateTime.Now;
                Console.WriteLine($"T{stt} : Max = {max} : {now.Hour}h{now.Minute}:{now.Second},{now.Millisecond}");
            });
            threads.Add(t);
            t.Start();
        }

        //Cau 4
        foreach (var t in threads) t.Join();

        Console.WriteLine("Ket qua cuoi cung: " + results.Max());

    }
    static int TimLonNhat(List<int> a, int x, int y)
    {
        int result = a[x];
        for (int i = x; i < y; i++)
        {
            if (a[i] > result)
            {
                result = a[i];
            }
        }
        return result;
    }
}