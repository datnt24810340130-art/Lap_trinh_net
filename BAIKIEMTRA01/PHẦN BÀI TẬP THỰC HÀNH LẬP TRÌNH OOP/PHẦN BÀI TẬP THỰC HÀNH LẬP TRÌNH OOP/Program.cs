
using System;
using System.Collections.Generic;
using System.Linq;

abstract class PhuongTien
{
    private string _maPT;
    private string _tenHang;
    private int _namSanXuat;
    private decimal _giaGoc;

    public string MaPT
    {
        get { return _maPT; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                _maPT = "PT000";
            else
                _maPT = value;
        }
    }

    public string TenHang
    {
        get { return _tenHang; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ten hang khong duoc de trong!");

            _tenHang = value;
        }
    }

    public int NamSanXuat
    {
        get { return _namSanXuat; }
        set
        {
            int namHienTai = DateTime.Now.Year;

            if (value < 1900 || value > namHienTai)
                throw new ArgumentException("Nam san xuat khong hop le!");

            _namSanXuat = value;
        }
    }

    public decimal GiaGoc
    {
        get { return _giaGoc; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Gia goc phai lon hon 0!");

            _giaGoc = value;
        }
    }

    public PhuongTien(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc)
    {
        MaPT = maPT;
        TenHang = tenHang;
        NamSanXuat = namSanXuat;
        GiaGoc = giaGoc;
    }

    public abstract decimal TinhGiaLanBanh();

    public virtual string GetInfo()
    {
        return "Ma PT: " + MaPT +
               " | Hang: " + TenHang +
               " | Nam SX: " + NamSanXuat +
               " | Gia goc: " + GiaGoc.ToString("N0") + " VND";
    }
}

class OTo : PhuongTien
{
    private int _soChoNgoi;
    private double _dungTichDongCo;

    public int SoChoNgoi
    {
        get { return _soChoNgoi; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "So cho ngoi phai lon hon 0!");

            _soChoNgoi = value;
        }
    }

    public double DungTichDongCo
    {
        get { return _dungTichDongCo; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Dung tich dong co phai lon hon 0!");

            _dungTichDongCo = value;
        }
    }

    public OTo(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc,
        int soChoNgoi,
        double dungTichDongCo)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        SoChoNgoi = soChoNgoi;
        DungTichDongCo = dungTichDongCo;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (SoChoNgoi <= 9)
        {
            return GiaGoc
                   + GiaGoc * 0.12m
                   + GiaGoc * 0.30m;
        }

        return GiaGoc + GiaGoc * 0.10m;
    }

    public override string GetInfo()
    {
        return base.GetInfo() +
               " | So cho: " + SoChoNgoi +
               " | Dung tich dong co: " +
               DungTichDongCo + " L";
    }
}

class XeMay : PhuongTien
{
    private int _dungTichXylanh;

    public int DungTichXylanh
    {
        get { return _dungTichXylanh; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Dung tich xy-lanh phai lon hon 0!");

            _dungTichXylanh = value;
        }
    }

    public XeMay(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc,
        int dungTichXylanh)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        DungTichXylanh = dungTichXylanh;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (DungTichXylanh < 175)
            return GiaGoc + GiaGoc * 0.02m;

        return GiaGoc + GiaGoc * 0.05m;
    }

    public override string GetInfo()
    {
        return base.GetInfo() +
               " | Dung tich xy-lanh: " +
               DungTichXylanh + " cc";
    }
}

class QuanLyPhuongTien
{
    private List<PhuongTien> danhSach;

    public QuanLyPhuongTien()
    {
        danhSach = new List<PhuongTien>();
    }

    public void AddPhuongTien(PhuongTien pt)
    {
        danhSach.Add(pt);
    }

    public void DisplayAll()
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sach phuong tien trong!");
            return;
        }

        foreach (PhuongTien pt in danhSach)
        {
            Console.WriteLine(pt.GetInfo());

            Console.WriteLine(
                "Gia lan banh: " +
                pt.TinhGiaLanBanh().ToString("N0") +
                " VND");

            Console.WriteLine("--------------------------------");
        }
    }

    public PhuongTien FindMaxGiaLanBanh()
    {
        if (danhSach.Count == 0)
            return null;

        return danhSach
            .OrderByDescending(pt => pt.TinhGiaLanBanh())
            .First();
    }

    public List<PhuongTien> SearchByName(string keyword)
    {
        return danhSach
            .Where(pt => pt.TenHang.Contains(
                keyword,
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
        QuanLyPhuongTien quanLy =
            new QuanLyPhuongTien();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine(" QUAN LY PHUONG TIEN AUTOSPEED");
            Console.WriteLine("================================");
            Console.WriteLine("1. Them o to");
            Console.WriteLine("2. Them xe may");
            Console.WriteLine("3. Hien thi danh sach");
            Console.WriteLine("4. Tim gia lan banh cao nhat");
            Console.WriteLine("5. Tim theo ten hang");
            Console.WriteLine("6. Chay test case");
            Console.WriteLine("0. Thoat");
            Console.WriteLine("================================");

            Console.Write("Nhap lua chon: ");
            string chon = Console.ReadLine();

            try
            {
                switch (chon)
                {
                    case "1":
                        ThemOTo(quanLy);
                        break;

                    case "2":
                        ThemXeMay(quanLy);
                        break;

                    case "3":
                        quanLy.DisplayAll();
                        break;

                    case "4":
                        TimMax(quanLy);
                        break;

                    case "5":
                        TimTheoTen(quanLy);
                        break;

                    case "6":
                        ChayTestCase();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
        }
    }

    static void ThemOTo(QuanLyPhuongTien quanLy)
    {
        Console.Write("Ma PT: ");
        string maPT = Console.ReadLine();

        Console.Write("Ten hang: ");
        string tenHang = Console.ReadLine();

        Console.Write("Nam san xuat: ");
        int nam = int.Parse(Console.ReadLine());

        Console.Write("Gia goc: ");
        decimal gia = decimal.Parse(Console.ReadLine());

        Console.Write("So cho ngoi: ");
        int soCho = int.Parse(Console.ReadLine());

        Console.Write("Dung tich dong co (L): ");
        double dungTich = double.Parse(
            Console.ReadLine());

        OTo oto = new OTo(
            maPT,
            tenHang,
            nam,
            gia,
            soCho,
            dungTich);

        quanLy.AddPhuongTien(oto);

        Console.WriteLine("Them o to thanh cong!");
    }

    static void ThemXeMay(QuanLyPhuongTien quanLy)
    {
        Console.Write("Ma PT: ");
        string maPT = Console.ReadLine();

        Console.Write("Ten hang: ");
        string tenHang = Console.ReadLine();

        Console.Write("Nam san xuat: ");
        int nam = int.Parse(Console.ReadLine());

        Console.Write("Gia goc: ");
        decimal gia = decimal.Parse(Console.ReadLine());

        Console.Write("Dung tich xy-lanh (cc): ");
        int dungTich = int.Parse(
            Console.ReadLine());

        XeMay xeMay = new XeMay(
            maPT,
            tenHang,
            nam,
            gia,
            dungTich);

        quanLy.AddPhuongTien(xeMay);

        Console.WriteLine("Them xe may thanh cong!");
    }

    static void TimMax(QuanLyPhuongTien quanLy)
    {
        PhuongTien pt =
            quanLy.FindMaxGiaLanBanh();

        if (pt == null)
        {
            Console.WriteLine("Danh sach rong!");
            return;
        }

        Console.WriteLine();
        Console.WriteLine(
            "Phuong tien co gia lan banh cao nhat:");

        Console.WriteLine(pt.GetInfo());

        Console.WriteLine(
            "Gia lan banh: " +
            pt.TinhGiaLanBanh().ToString("N0") +
            " VND");
    }

    static void TimTheoTen(QuanLyPhuongTien quanLy)
    {
        Console.Write("Nhap ten hang can tim: ");
        string keyword = Console.ReadLine();

        List<PhuongTien> ketQua =
            quanLy.SearchByName(keyword);

        if (ketQua.Count == 0)
        {
            Console.WriteLine("Khong tim thay!");
            return;
        }

        foreach (PhuongTien pt in ketQua)
        {
            Console.WriteLine(pt.GetInfo());

            Console.WriteLine(
                "Gia lan banh: " +
                pt.TinhGiaLanBanh().ToString("N0") +
                " VND");

            Console.WriteLine("--------------------------------");
        }
    }

    static void ChayTestCase()
    {
        Console.WriteLine();
        Console.WriteLine("======================================");
        Console.WriteLine("         CHAY CAC TEST CASE");
        Console.WriteLine("======================================");

        TestTC01();
        TestTC02();
        TestTC03();
        TestTC04();
        TestTC05();
    }

    static void TestTC01()
    {
        Console.WriteLine("\nTC01 - Validation nam san xuat");

        try
        {
            OTo oto = new OTo(
                "OT001",
                "Toyota",
                1850,
                1000000000m,
                5,
                2.0);

            Console.WriteLine(
                "FAIL - Khong phat sinh ngoai le!");
        }
        catch (ArgumentException ex)
        {
            if (ex.Message == "Nam san xuat khong hop le!")
            {
                Console.WriteLine(
                    "PASS - Da phat sinh ArgumentException");

                Console.WriteLine(
                    "Message: " + ex.Message);
            }
            else
            {
                Console.WriteLine(
                    "FAIL - Sai noi dung exception");
            }
        }
    }

    static void TestTC02()
    {
        Console.WriteLine("\nTC02 - Tinh gia lan banh O To");

        OTo oto = new OTo(
            "OT001",
            "Toyota",
            2024,
            1000000000m,
            5,
            2.0);

        decimal ketQua =
            oto.TinhGiaLanBanh();

        decimal expected =
            1420000000m;

        if (ketQua == expected)
        {
            Console.WriteLine(
                "PASS - Gia lan banh = 1,420,000,000 VND");
        }
        else
        {
            Console.WriteLine(
                "FAIL - Ket qua = " +
                ketQua.ToString("N0"));
        }
    }

    static void TestTC03()
    {
        Console.WriteLine("\nTC03 - Tinh gia lan banh Xe May");

        XeMay xeMay = new XeMay(
            "XM001",
            "Honda",
            2024,
            50000000m,
            150);

        decimal ketQua =
            xeMay.TinhGiaLanBanh();

        decimal expected =
            51000000m;

        if (ketQua == expected)
        {
            Console.WriteLine(
                "PASS - Gia lan banh = 51,000,000 VND");
        }
        else
        {
            Console.WriteLine(
                "FAIL - Ket qua = " +
                ketQua.ToString("N0"));
        }
    }

    static void TestTC04()
    {
        Console.WriteLine("\nTC04 - Kiem tra da hinh");

        List<PhuongTien> danhSach =
            new List<PhuongTien>();

        danhSach.Add(
            new OTo(
                "OT001",
                "Toyota",
                2024,
                1000000000m,
                5,
                2.0));

        danhSach.Add(
            new XeMay(
                "XM001",
                "Honda",
                2024,
                50000000m,
                150));

        bool dung = true;

        foreach (PhuongTien pt in danhSach)
        {
            decimal gia =
                pt.TinhGiaLanBanh();

            Console.WriteLine(
                pt.GetType().Name +
                " -> " +
                gia.ToString("N0") +
                " VND");

            if (pt is OTo && gia != 1420000000m)
                dung = false;

            if (pt is XeMay && gia != 51000000m)
                dung = false;
        }

        if (dung)
        {
            Console.WriteLine(
                "PASS - Da hinh hoat dong dung");
        }
        else
        {
            Console.WriteLine(
                "FAIL - Cong thuc khong dung");
        }
    }

    static void TestTC05()
    {
        Console.WriteLine(
            "\nTC05 - Tim gia lan banh cao nhat");

        QuanLyPhuongTien quanLy =
            new QuanLyPhuongTien();

        OTo oto = new OTo(
            "OT001",
            "Toyota",
            2024,
            1000000000m,
            5,
            2.0);

        XeMay xeMay = new XeMay(
            "XM001",
            "Honda",
            2024,
            50000000m,
            150);

        quanLy.AddPhuongTien(oto);
        quanLy.AddPhuongTien(xeMay);

        PhuongTien max =
            quanLy.FindMaxGiaLanBanh();

        if (max == oto &&
            max.TinhGiaLanBanh() == 1420000000m)
        {
            Console.WriteLine(
                "PASS - Tim dung O To 5 cho");

            Console.WriteLine(
                "Gia lan banh = 1,420,000,000 VND");
        }
        else
        {
            Console.WriteLine(
                "FAIL - Tim sai phuong tien");
        }
    }
}
```
