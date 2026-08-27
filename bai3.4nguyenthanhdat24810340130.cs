using System;

class Program
{
    static void Bai1()
    {
        Console.Clear();

        Console.Write("Nhap so thu nhat a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Nhập so thu hai b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Nhap phep toan (+, -, *, /, %): ");
        char op = char.Parse(Console.ReadLine());

        try
        {
            double result = op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' when b != 0 => a / b,
                '%' when b != 0 => a % b,
                '/' or '%' when b == 0 => throw new DivideByZeroException(),
                _ => throw new InvalidOperationException("Phep toan khong hop le!")
            };

            Console.WriteLine($"ket qua: {result:F2}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Loi: Khong the chia cho 0!");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Loi: {ex.Message}");
        }

        Console.WriteLine("\nNhan phim bat ky đe quay lai Menu...");
        Console.ReadKey();
    }

    static void Bai2()
    {
        Console.Clear();

        Console.Write("Nhap a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Nhap b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Nhap c: ");
        double c = double.Parse(Console.ReadLine());

        if (a == 0)
        {
            if (b == 0)
            {
                if (c == 0)
                    Console.WriteLine("Phuong trinh co vo so nghiem.");
                else
                    Console.WriteLine("Phuong trinh vo nghiem.");
            }
            else
            {
                double x = -c / b;
                Console.WriteLine($"Nghiem x = {x:F2}");
            }
        }
        else
        {
            double delta = b * b - 4 * a * c;

            if (delta > 0)
            {
                double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b - Math.Sqrt(delta)) / (2 * a);

                Console.WriteLine($"x1 = {x1:F2}");
                Console.WriteLine($"x2 = {x2:F2}");
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                Console.WriteLine($"Nghiem kep x = {x:F2}");
            }
            else
            {
                Console.WriteLine("Vo nghiem.");
            }
        }

        Console.WriteLine("\nNhan phim bat ky de quay lai Menu...");
        Console.ReadKey();
    }

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

    static void Bai3()
    {
        Console.Clear();

        Console.Write("Nhap so nguyen duonh N: ");
        int N = int.Parse(Console.ReadLine());

        if (IsPrime(N))
            Console.WriteLine($"{N} la so nguyen to");
        else
            Console.WriteLine($"{N} Khong la so nguyen to");

        if (IsPerfectNumber(N))
            Console.WriteLine($"{N} la so hoan hao");
        else
            Console.WriteLine($"{N} Khong la so hoan hao");

        Console.Write($"Dãy Fibonacci {N} so: ");

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

        Console.WriteLine("\nNhan phim bat ki de quay lai menu");
        Console.ReadKey();
    }

    static void Main()
    {
        int choice;

        do
        {
            Console.Clear();

            Console.WriteLine("========== MENU CHÍNH ==========");
            Console.WriteLine("1. Chay bai 1 (Calculator)");
            Console.WriteLine("2. Chạy Bai 2 (Phương trình bậc 2)");
            Console.WriteLine("3. Chạy Bai  3 (Số nguyên tố & Fibonacci)");
            Console.WriteLine("0. Thoat ct");
            

            Console.Write("Nhap lua chon: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Bai1();
                    break;

                case 2:
                    Bai2();
                    break;

                case 3:
                    Bai3();
                    break;

                case 0:
                    Console.Clear();
                    Console.WriteLine("Da thoat ct");
                    break;

                default:
                    Console.WriteLine("Lua chon ko hop le");
                    Console.WriteLine("Nhan phim bat ki de tiep tuc");
                    Console.ReadKey();
                    break;
            }

        } while (choice != 0);
    }
}
S