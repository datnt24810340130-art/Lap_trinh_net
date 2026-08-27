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
        Console.Write("Nhap so nguyen duong: ");
        int N = int.Parse(Console.ReadLine());
        if (IsPrime(N))
            Console.WriteLine($"{N} là so nguyen to");
        else
            Console.WriteLine($"{N} Khong la so nguyen to.");
        if (IsPerfectNumber(N))
            Console.WriteLine($"{N} la so hoan hao");
        else
            Console.WriteLine($"{N} Khong la so hoan hao");
        Console.Write($"Day Fibonacci {N} so: ");

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
