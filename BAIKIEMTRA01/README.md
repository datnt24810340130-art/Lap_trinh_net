# BÀI KIỂM TRA 01 - LẬP TRÌNH .NET

Họ và tên: Nguyễn Thanh Đạt  
MSSV: 24810340130

---

## Câu 1: Phân biệt Value Types và Reference Types

1. Value Types (Kiểu giá trị):
- Lưu trữ: Dữ liệu lưu trực tiếp trên bộ nhớ Stack.
- Cơ chế gán: Tạo bản sao mới độc lập. Thay đổi biến này không ảnh hưởng biến khác.
- Quản lý bộ nhớ: Tự động giải phóng khi ra khỏi phạm vi.
- Ví dụ: int, float, double, bool, struct, enum.

2. Reference Types (Kiểu tham chiếu):
- Lưu trữ: Dữ liệu lưu trên Heap, biến lưu địa chỉ tham chiếu trên Stack.
- Cơ chế gán: Cùng trỏ đến một vùng nhớ Heap. Thay đổi qua một biến sẽ ảnh hưởng đến đối tượng chung.
- Quản lý bộ nhớ: Thu hồi tự động bởi Garbage Collector (GC).
- Ví dụ: class, string, array, interface.

```csharp
int a = 10;
int b = a;
b = 20;

int[] arr1 = { 1, 2, 3 };
int[] arr2 = arr1;
arr2[0] = 99;
```

---

## Câu 2: So sánh Init-only Properties (init) và set thông thường

1. Thuộc tính set:
- Cho phép gán hoặc sửa đổi giá trị bất kỳ lúc nào sau khi khởi tạo.
- Không đảm bảo tính bất biến của dữ liệu.

2. Thuộc tính init (C# 9+):
- Chỉ cho phép gán giá trị tại thời điểm khởi tạo đối tượng.
- Sau khi khởi tạo xong, thuộc tính trở thành chỉ đọc, không thể thay đổi giá trị.
- Giúp bảo vệ dữ liệu không bị sửa đổi vô ý (như DTO, hóa đơn, cấu hình).

```csharp
public class SinhVien
{
    public string HoTen { get; init; }
    public int MaSV { get; init; }
}

var sv = new SinhVien { HoTen = "Đạt", MaSV = 123 };
```

---

## Câu 3: Phân biệt phương thức virtual (lớp cha) và override (lớp con)

1. Phương thức virtual (Lớp cha):
- Khai báo tại lớp cha để cho phép lớp con ghi đè.
- Cung cấp sẵn một phiên bản xử lý mặc định.

2. Phương thức override (Lớp con):
- Khai báo tại lớp con để ghi đè (thay thế) cách xử lý của phương thức virtual từ lớp cha.
- Giúp triển khai tính đa hình (Polymorphism) tại runtime.

```csharp
public class DongVat
{
    public virtual void Keu()
    {
        Console.WriteLine("Dong vat keu");
    }
}

public class Cho : DongVat
{
    public override void Keu()
    {
        Console.WriteLine("Gau gau");
    }
}

DongVat dv = new Cho();
dv.Keu();
```

---

## Câu 4: Tại sao thành phần static không thể truy xuất qua Object Instance?

1. Bản chất của static:
- Thuộc về Lớp (Class) chứ không thuộc về từng đối tượng cụ thể (Instance).
- Được cấp phát vùng nhớ duy nhất một lần và dùng chung cho tất cả các đối tượng.

2. Lý do thiết kế:
- Rõ ràng ngữ nghĩa: Gọi qua tên lớp (ClassName.Member) thể hiện rõ đây là dữ liệu dùng chung.
- Tránh nhầm lẫn: Không làm lập trình viên hiểu nhầm rằng mỗi đối tượng có một bản sao riêng.
- An toàn mã nguồn: Trình biên dịch C# bắt buộc gọi qua tên lớp để tránh các lỗi logic không đáng có.

```csharp
public class SinhVien
{
    public string HoTen { get; set; }
    public static int TongSoSV { get; set; }
}

SinhVien.TongSoSV = 10;
```

---

Bài làm bởi: Nguyễn Thanh Đạt - MSSV: 24810340130

