using System;

class Program
{
    static void Main()
    {
        Console.Write("Nhap a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Nhap b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Nhap phep toan: ");
        char op = char.Parse(Console.ReadLine());

        try
        {
            double kq = op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                '%' => a % b,
                _ => 0
            };

            if ((op == '/' || op == '%') && b == 0)
                throw new DivideByZeroException();

            Console.WriteLine($"Ket qua: {kq:F2}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Loi: Khong the chia cho 0!");
        }
    }
}