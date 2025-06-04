using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //Client();
        Sever();
    }
    static void Client()
    {
        Console.Write("Số lượng số muốn gửi: ");
        int MAX_SIZE = Convert.ToInt32(Console.ReadLine());
        try
        {
            using (TcpClient client = new TcpClient("172.20.10.2", 8888))
            using (NetworkStream stream = client.GetStream())
            using (StreamWriter writer = new StreamWriter(stream))
            using (StreamReader reader = new StreamReader(stream))
            {
                writer.AutoFlush = true;
                Console.WriteLine("Đã kết nối đến server!");

                Random rand = new Random();
                List<int> A = new List<int>();
                for (int i = 0; i < MAX_SIZE; i++)
                {
                    A.Add(rand.Next(1, 101));
                }

                Console.WriteLine("Gửi dữ liệu cho server...");
                Console.Write("Gửi:");
                foreach (int number in A)
                {
                    writer.WriteLine(number);
                    Console.Write(" "+ number);
                }
                Console.WriteLine();

                // Gửi tín hiệu kết thúc
                writer.WriteLine("END");

                Console.WriteLine("Dữ liệu đã gửi, chờ phản hồi từ server...");
                string response = reader.ReadLine();
                if (response != null)
                {
                    Console.WriteLine("Phản hồi từ server: " + response);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi: " + ex.Message);
        }
    }
    static void Sever()
    {
        TcpListener listener = null;
        try
        {
            listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();
            Console.WriteLine("Server đang đợi kết nối...");

            using (TcpClient client = listener.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.AutoFlush = true;
                Console.WriteLine("Đã kết nối với client!");

                List<int> numbers = new List<int>();
                string line;
                int count = 0;

                Console.Write("Nhận được:");
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim().Equals("END", StringComparison.OrdinalIgnoreCase))
                        break;

                   
                    if (int.TryParse(line.Trim(), out int num))
                    {
                        Console.Write(" "+ num);
                        numbers.Add(num);
                        count++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine($"Đã nhận {count} số. Đang xử lý...");
                int max = FindMax(numbers);
                writer.WriteLine(max);
                Console.WriteLine($"Đã gửi số lớn nhất ({max}) cho client.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi: " + ex.Message);
        }
        finally
        {
            listener?.Stop();
        }
    }



    static int FindMax(List<int> numbers)
    {
        int max = numbers[0];
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > max)
                max = numbers[i];
        }
        return max;
    }
}
