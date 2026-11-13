# Hướng dẫn bố trí controls FormMuonTra

| Loại control | Name | Text/giá trị hiển thị | Vị trí (X, Y) | Kích thước (W, H) | Chức năng |
|---|---|---|---:|---:|---|
| Label | lblTitle | QUẢN LÝ MƯỢN - TRẢ SÁCH THƯ VIỆN | (24, 18) | Tự động | Tiêu đề form |
| Label | lblHoTen | Tên độc giả cần lọc | (26, 76) | Tự động | Nhãn tìm kiếm độc giả |
| TextBox | txtHoTen | Trống | (29, 96) | 220 × 23 | Lọc theo họ tên |
| Label | lblTrangThai | Trạng thái | (268, 76) | Tự động | Nhãn trạng thái phiếu |
| ComboBox | cboTrangThai | Tất cả / Đang mượn / Đã trả | (271, 96) | 150 × 23 | Lọc theo trạng thái |
| Button | btnLoc | Lọc phiếu | (438, 94) | 88 × 27 | Áp dụng bộ lọc tên và trạng thái |
| Button | btnTaiLai | Tải lại | (532, 94) | 88 × 27 | Xóa tên tìm kiếm và tải lại |
| DataGridView | dgvPhieuMuon | Dữ liệu phiếu mượn | (28, 137) | 945 × 300 | Hiển thị danh sách, chọn phiếu để trả |
| TextBox | txtMaDocGia | Trống | (32, 481) | 110 × 23 | Nhập mã độc giả để lập phiếu |
| TextBox | txtMaSach | Trống | (162, 481) | 100 × 23 | Nhập mã sách để lập phiếu |
| NumericUpDown | nudSoLuong | 1 | (282, 481) | 77 × 23 | Nhập số lượng mượn |
| DateTimePicker | dtpHanTra | Hôm nay + 14 ngày | (380, 481) | 112 × 23 | Chọn hạn trả |
| Button | btnLapPhieu | Lập phiếu mượn | (508, 478) | 112 × 29 | Tạo phiếu qua BUS/DAL |
| Button | btnTraSach | Trả phiếu đã chọn | (28, 530) | 130 × 34 | Trả sách và cập nhật tồn kho |
| Button | btnCanhBao | Cảnh báo quá hạn | (172, 530) | 165 × 34 | Hiển thị phiếu có hạn trả đã qua |

Giao diện đã được tạo trong `FormMuonTra.Designer.cs`; mở form trong Visual Studio để xem và chỉnh sửa trực quan bằng Windows Forms Designer.
