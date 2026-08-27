using System;

class Program
{
    static void Main()
    {
        double a, b, c;

        Console.Write("Nhập a: ");
        a = double.Parse(Console.ReadLine());

        Console.Write("Nhập b: ");
        b = double.Parse(Console.ReadLine());

        Console.Write("Nhập c: ");
        c = double.Parse(Console.ReadLine());
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
    }
}