# BÀI KIỂM TRA 01 - LẬP TRÌNH .NET

**Họ và tên:** Nguyễn Thanh Đạt  
**MSSV:** 24810340130

---

## Câu 1: Sự khác nhau giữa Value Types và Reference Types về cơ chế lưu trữ vùng nhớ (Stack vs Heap)

### 1.1. Value Types (Kiểu giá trị)

- **Lưu trữ:** Dữ liệu được lưu trực tiếp trên **Stack**.
- **Các kiểu thuộc Value Types:** `int`, `float`, `double`, `bool`, `char`, `struct`, `enum`.
- **Cơ chế gán:** Khi gán một biến Value Type cho biến khác, một **bản sao độc lập** của giá trị được tạo ra. Thay đổi trên bản sao **không ảnh hưởng** đến biến gốc.
- **Vòng đời:** Được giải phóng tự động khi ra khỏi phạm vi (scope) của phương thức, không cần Garbage Collector.

```csharp
int a = 10;
int b = a;    // b là bản sao của a
b = 20;       // a vẫn = 10, b = 20
```

### 1.2. Reference Types (Kiểu tham chiếu)

- **Lưu trữ:** Dữ liệu (object) được lưu trên **Heap**, còn biến trên Stack chỉ chứa **địa chỉ tham chiếu** (reference/con trỏ) trỏ tới vùng nhớ Heap.
- **Các kiểu thuộc Reference Types:** `class`, `string`, `array`, `delegate`, `interface`, `object`.
- **Cơ chế gán:** Khi gán một biến Reference Type cho biến khác, cả hai biến cùng **trỏ đến một đối tượng** trên Heap. Thay đổi qua một biến sẽ **ảnh hưởng** đến biến còn lại.
- **Vòng đời:** Được thu hồi bởi **Garbage Collector (GC)** khi không còn tham chiếu nào trỏ tới.

```csharp
int[] arr1 = { 1, 2, 3 };
int[] arr2 = arr1;   // arr2 trỏ cùng vùng nhớ với arr1
arr2[0] = 99;        // arr1[0] cũng = 99
```

### 1.3. Bảng so sánh tổng hợp

| Tiêu chí | Value Types | Reference Types |
|---|---|---|
| **Vùng nhớ** | Stack | Heap (biến tham chiếu trên Stack) |
| **Chứa gì** | Giá trị thực tế | Địa chỉ tham chiếu đến object |
| **Gán biến** | Sao chép giá trị (copy) | Sao chép tham chiếu (cùng trỏ 1 object) |
| **Giá trị mặc định** | `0`, `false`, `'\0'`... | `null` |
| **Giải phóng bộ nhớ** | Tự động khi hết scope | Garbage Collector |
| **Ví dụ** | `int`, `struct`, `enum` | `class`, `string`, `array` |

---

## Câu 2: Tính năng Init-only Properties (`init`) khác gì so với thuộc tính có `set` thông thường?

### 2.1. Thuộc tính có `set` thông thường

- Cho phép **gán giá trị bất kỳ lúc nào** sau khi đối tượng đã được khởi tạo.
- Không đảm bảo tính **bất biến (immutability)** của đối tượng.

```csharp
public class SinhVien
{
    public string HoTen { get; set; }
    public int MaSV { get; set; }
}

var sv = new SinhVien { HoTen = "Đạt", MaSV = 123 };
sv.HoTen = "Tên khác";   // ✅ Được phép - có thể thay đổi bất kỳ lúc nào
```

### 2.2. Init-only Properties (`init`) — C# 9+

- Chỉ cho phép gán giá trị **tại thời điểm khởi tạo** (trong constructor hoặc object initializer).
- Sau khi khởi tạo xong, thuộc tính trở thành **chỉ đọc (read-only)**, không thể thay đổi.
- Kết hợp được cả **tính bất biến** và **sự tiện lợi** của object initializer.

```csharp
public class SinhVien
{
    public string HoTen { get; init; }
    public int MaSV { get; init; }
}

var sv = new SinhVien { HoTen = "Đạt", MaSV = 123 };
sv.HoTen = "Tên khác";   // ❌ Lỗi biên dịch - không thể thay đổi sau khởi tạo
```

### 2.3. Bảng so sánh

| Tiêu chí | `set` | `init` |
|---|---|---|
| **Gán khi khởi tạo** | ✅ Được | ✅ Được |
| **Gán lại sau khởi tạo** | ✅ Được | ❌ Không được |
| **Tính bất biến** | Không đảm bảo | Đảm bảo |
| **Phiên bản C#** | Mọi phiên bản | C# 9 trở lên |

### 2.4. Trường hợp sử dụng thực tế

- **Data Transfer Object (DTO):** Khi nhận dữ liệu từ API, cần đảm bảo dữ liệu không bị thay đổi sau khi ánh xạ.
- **Record / Entity bất biến:** Các đối tượng đại diện cho dữ liệu cố định như thông tin hóa đơn, giao dịch ngân hàng — một khi đã tạo thì không được phép sửa.
- **Cấu hình ứng dụng:** Đọc cấu hình từ file `appsettings.json` và bind vào object, sau đó không cho phép code khác thay đổi giá trị cấu hình.

```csharp
// Ví dụ thực tế: DTO nhận dữ liệu từ API
public class DonHangDto
{
    public int MaDonHang { get; init; }
    public DateTime NgayTao { get; init; }
    public decimal TongTien { get; init; }
}

// Khởi tạo 1 lần, sau đó không ai có thể sửa đổi
var donHang = new DonHangDto
{
    MaDonHang = 1001,
    NgayTao = DateTime.Now,
    TongTien = 500000m
};
```

---

## Câu 3: Phân biệt phương thức `virtual` ở lớp cha và phương thức `override` ở lớp con (Đa hình - Polymorphism)

### 3.1. Phương thức `virtual` (Lớp cha)

- Được khai báo tại **lớp cha (base class)** với từ khóa `virtual`.
- Cung cấp một **phiên bản mặc định** (default implementation) của phương thức.
- **Cho phép** lớp con ghi đè (override) nhưng **không bắt buộc** — nếu lớp con không ghi đè, phiên bản của lớp cha sẽ được sử dụng.

### 3.2. Phương thức `override` (Lớp con)

- Được khai báo tại **lớp con (derived class)** với từ khóa `override`.
- **Ghi đè** (thay thế) hành vi của phương thức `virtual` từ lớp cha.
- Cung cấp **phiên bản cụ thể** phù hợp với lớp con.
- Có thể gọi lại phương thức gốc của lớp cha bằng `base.TenPhuongThuc()`.

### 3.3. Cơ chế hoạt động của Đa hình (Polymorphism)

Khi gọi phương thức thông qua biến kiểu lớp cha, **runtime** sẽ xác định kiểu thực tế của đối tượng để gọi đúng phiên bản phương thức — đây gọi là **late binding (liên kết muộn)**.

```csharp
public class DongVat
{
    public virtual void Keu()
    {
        Console.WriteLine("Động vật kêu...");
    }
}

public class Cho : DongVat
{
    public override void Keu()
    {
        Console.WriteLine("Gâu gâu!");
    }
}

public class Meo : DongVat
{
    public override void Keu()
    {
        Console.WriteLine("Meo meo!");
    }
}

// Đa hình trong thực tế
DongVat dv1 = new Cho();
DongVat dv2 = new Meo();
DongVat dv3 = new DongVat();

dv1.Keu();   // Output: "Gâu gâu!"      → gọi phiên bản của lớp Cho
dv2.Keu();   // Output: "Meo meo!"      → gọi phiên bản của lớp Meo
dv3.Keu();   // Output: "Động vật kêu..." → gọi phiên bản gốc lớp cha
```

### 3.4. Bảng so sánh

| Tiêu chí | `virtual` (Lớp cha) | `override` (Lớp con) |
|---|---|---|
| **Vị trí khai báo** | Lớp cha (base class) | Lớp con (derived class) |
| **Vai trò** | Định nghĩa hành vi mặc định, cho phép ghi đè | Ghi đè hành vi, cung cấp triển khai riêng |
| **Bắt buộc không?** | Không bắt buộc lớp con phải override | Chỉ dùng khi muốn thay đổi hành vi lớp cha |
| **Cơ chế** | Đánh dấu phương thức có thể bị ghi đè | Thực hiện ghi đè tại runtime (late binding) |
| **Gọi phương thức cha** | — | Dùng `base.TenPhuongThuc()` |

---

## Câu 4: Tại sao thành phần `static` không thể truy xuất qua Object Instance?

### 4.1. Bản chất của thành phần `static`

- Thành phần `static` **thuộc về lớp (Class)**, không thuộc về bất kỳ đối tượng (instance) cụ thể nào.
- Được cấp phát vùng nhớ **duy nhất một lần** khi lớp được nạp (load) vào bộ nhớ, và tồn tại suốt vòng đời ứng dụng.
- **Mọi instance** của lớp đều chia sẻ chung một bản duy nhất của thành phần `static`.

### 4.2. Lý do không thể truy xuất qua Instance

1. **Về mặt ngữ nghĩa:** Thành phần `static` không gắn liền với trạng thái riêng của bất kỳ object nào. Cho phép truy xuất qua instance sẽ gây **hiểu nhầm** rằng giá trị đó thuộc về object cụ thể đó.

2. **Về mặt thiết kế ngôn ngữ:** C# thiết kế rõ ràng để phân biệt giữa:
   - **Instance member:** truy xuất qua `object.Member` → mỗi object có bản riêng.
   - **Static member:** truy xuất qua `ClassName.Member` → chỉ có 1 bản duy nhất cho cả lớp.

3. **Tránh nhập nhằng:** Nếu cho phép truy xuất `static` qua instance, lập trình viên có thể nhầm lẫn rằng mỗi object có giá trị `static` riêng, dẫn đến bug khó phát hiện.

### 4.3. Ví dụ minh họa

```csharp
public class SinhVien
{
    public string HoTen { get; set; }            // Instance member
    public static int TongSoSV { get; set; }     // Static member

    public SinhVien(string hoTen)
    {
        HoTen = hoTen;
        TongSoSV++;    // Tăng biến đếm chung cho cả lớp
    }
}

// Sử dụng
var sv1 = new SinhVien("Đạt");
var sv2 = new SinhVien("Minh");

// ✅ Đúng: Truy xuất static qua tên lớp
Console.WriteLine(SinhVien.TongSoSV);    // Output: 2

// ❌ Sai: Không thể truy xuất static qua instance
// Console.WriteLine(sv1.TongSoSV);      // Lỗi biên dịch CS0176
```

### 4.4. Tóm tắt

| Tiêu chí | Instance Member | Static Member |
|---|---|---|
| **Thuộc về** | Đối tượng cụ thể | Lớp (Class) |
| **Số bản sao** | Mỗi object có 1 bản riêng | Chỉ có 1 bản duy nhất |
| **Cách truy xuất** | `object.Member` | `ClassName.Member` |
| **Cần tạo instance?** | ✅ Có | ❌ Không |
| **Vùng nhớ** | Cấp phát khi `new` | Cấp phát khi lớp được nạp |

> **Kết luận:** Compiler C# cấm truy xuất thành phần `static` qua instance nhằm đảm bảo **tính rõ ràng**, **nhất quán** trong thiết kế và tránh những lỗi logic khó phát hiện khi lập trình.

---

*Bài làm bởi: Nguyễn Thanh Đạt — MSSV: 24810340130*
