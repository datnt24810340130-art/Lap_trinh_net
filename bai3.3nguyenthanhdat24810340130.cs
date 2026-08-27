using System;

class Program
{
    static bool IsPrime(int n)
    {
        if (n < 2)
            return false;

        int i = 2;

        while (i <= Math.Sqrt(n))
        {
            if (n % i == 0)
                return false;

            i++;
        }

        return true;
    }
    static bool IsPerfectNumber(int n)
    {
        if (n <= 1)
            return false;

        int sum = 0;

        for (int i = 1; i <= n / 2; i++)
        {
            if (n % i == 0)
                sum += i;
        }

        return sum == n;
    }

    static void Main()
    {
        Console.Write("Nhập số nguyên dương N: ");
        int N = int.Parse(Console.ReadLine());
        if (IsPrime(N))
            Console.WriteLine($"{N} là Số nguyên tố!");
        else
            Console.WriteLine($"{N} KHÔNG là Số nguyên tố.");
        if (IsPerfectNumber(N))
            Console.WriteLine($"{N} là Số hoàn hảo!");
        else
            Console.WriteLine($"{N} KHÔNG là Số hoàn hảo.");
        Console.Write($"Dãy Fibonacci {N} số: ");

        int f1 = 0;
        int f2 = 1;

        for (int i = 0; i < N; i++)
        {
            Console.Write(f1);

            if (i < N - 1)
                Console.Write(", ");

            int next = f1 + f2;
            f1 = f2;
            f2 = next;
        }

        Console.WriteLine();
    }
}
