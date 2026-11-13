# Hướng dẫn giao diện FormKhamBenh

Form đã được bố trí sẵn trong `FormKhamBenh.Designer.cs`. Bảng dưới đây dùng để nhận diện các control trong Visual Studio Designer.

| Khu vực | Tên control | Loại | Chức năng |
|---|---|---|---|
| Tiếp nhận | `txtSoDienThoai` | TextBox | Nhập số điện thoại bệnh nhân |
| Tiếp nhận | `btnTimKiem` | Button | Tìm bệnh nhân theo số điện thoại |
| Tiếp nhận | `txtHoTen` | TextBox | Hiển thị/nhập họ tên bệnh nhân |
| Tiếp nhận | `txtChanDoan` | TextBox | Nhập chẩn đoán |
| Tiếp nhận | `nudTienKham` | NumericUpDown | Nhập tiền khám |
| Tiếp nhận | `btnTaoHoSo` | Button | Tạo hồ sơ cho bệnh nhân tìm thấy hoặc tạo mới |
| Kê đơn | `cboThuoc` | ComboBox | Chọn thuốc trong danh mục |
| Kê đơn | `txtSoLuong` | NumericUpDown | Nhập số lượng kê |
| Kê đơn | `btnThemThuoc` | Button | Thêm thuốc vào đơn |
| Kê đơn | `dgvDonThuoc` | DataGridView | Hiển thị các thuốc và thành tiền |
| Kê đơn | `btnXoaThuoc` | Button | Xóa thuốc được chọn khỏi đơn nháp |
| Thanh toán | `btnLuuXuatDon` | Button | Lưu đơn và trừ tồn kho trong transaction |
| Thanh toán | `btnThanhToan` | Button | Tính tổng tiền và đánh dấu đã thanh toán |
| Thanh toán | `lblTongTien` | Label | Hiển thị tổng tiền khám và thuốc |

## Bố cục

- Nhóm 1: Tiếp nhận bệnh nhân và tạo hồ sơ khám.
- Nhóm 2: Chọn thuốc, nhập số lượng và xem chi tiết đơn.
- Cuối form: Tổng tiền, lưu/xuất đơn và thanh toán.
