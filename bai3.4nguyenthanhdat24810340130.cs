using System;

class Program
{
    static void Bai1()
    {
        Console.Clear();

        Console.Write("Nhập số thứ nhất a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Nhập số thứ hai b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Nhập phép toán (+, -, *, /, %): ");
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
                _ => throw new InvalidOperationException("Phép toán không hợp lệ!")
            };

            Console.WriteLine($"Kết quả: {result:F2}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Lỗi: Không thể chia cho 0!");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }

        Console.WriteLine("\nNhấn phím bất kỳ để quay lại Menu...");
        Console.ReadKey();
    }

    static void Bai2()
    {
        Console.Clear();

        Console.Write("Nhập a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Nhập b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Nhập c: ");
        double c = double.Parse(Console.ReadLine());

        if (a == 0)
        {
            if (b == 0)
            {
                if (c == 0)
                    Console.WriteLine("Phương trình có vô số nghiệm.");
                else
                    Console.WriteLine("Phương trình vô nghiệm.");
            }
            else
            {
                double x = -c / b;
                Console.WriteLine($"Nghiệm x = {x:F2}");
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
                Console.WriteLine($"Nghiệm kép x = {x:F2}");
            }
            else
            {
                Console.WriteLine("Vô nghiệm.");
            }
        }

        Console.WriteLine("\nNhấn phím bất kỳ để quay lại Menu...");
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

        Console.WriteLine("\nNhấn phím bất kỳ để quay lại Menu...");
        Console.ReadKey();
    }

    static void Main()
    {
        int choice;

        do
        {
            Console.Clear();

            Console.WriteLine("========== MENU CHÍNH ==========");
            Console.WriteLine("1. Chạy Bài tập 1 (Calculator)");
            Console.WriteLine("2. Chạy Bài tập 2 (Phương trình bậc 2)");
            Console.WriteLine("3. Chạy Bài tập 3 (Số nguyên tố & Fibonacci)");
            Console.WriteLine("0. Thoát chương trình");
            Console.WriteLine("================================");

            Console.Write("Nhập lựa chọn: ");
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
                    Console.WriteLine("Đã thoát chương trình!");
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                    break;
            }

        } while (choice != 0);
    }
}
S