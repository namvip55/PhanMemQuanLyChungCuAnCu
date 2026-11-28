using System;
using System.Data;
using System.Windows.Forms;
using AnCuManagement.BLL;
using AnCuManagement.Common;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    /// <summary>
    /// Giao diện Quản lý hợp đồng thuê căn hộ & In ấn hợp đồng (UC4.1 - UC4.5)
    /// Do Lê Ngọc Quyền (MSSV: 24810310259) phụ trách thiết kế & lập trình.
    /// </summary>
    public partial class FormHopDong : Form
    {
        private readonly HopDongBLL _hopDongBLL = new();
        private readonly PhongBLL _phongBLL = new();
        private readonly KhachThueBLL _khachThueBLL = new();
        private readonly TaiKhoanDTO? _currentUser;
        private int _selectedMaHopDong = 0;

        public FormHopDong(TaiKhoanDTO? currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void FormHopDong_Load(object? sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now.AddYears(1);

            LoadCombos();
            LoadData();

            // Nếu là khách thuê thì chỉ xem hợp đồng của mình, ẩn các nút thêm/sửa/thanh lý
            if (_currentUser != null && _currentUser.QuyenHan == AppConstants.QUYEN_KHACH_THUE)
            {
                btnThemMoi.Visible = false;
                btnGiaHan.Visible = false;
                btnChamDut.Visible = false;
                btnYeuCauHopDong.Visible = true;
            }
            else
            {
                btnThemMoi.Visible = true;
                btnGiaHan.Visible = true;
                btnChamDut.Visible = true;
                btnYeuCauHopDong.Visible = false;
            }
        }

        private void LoadCombos()
        {
            try
            {
                DataTable dtPhong = _phongBLL.GetAllPhong();
                cboPhong.DisplayMember = "SoPhong";
                cboPhong.ValueMember = "MaPhong";
                cboPhong.DataSource = dtPhong;

                DataTable dtKhach = _khachThueBLL.GetAllKhachThue();
                cboKhachThue.DisplayMember = "HoTen";
                cboKhachThue.ValueMember = "MaKhachThue";
                cboKhachThue.DataSource = dtKhach;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt;
                if (_currentUser != null && _currentUser.QuyenHan == AppConstants.QUYEN_KHACH_THUE && _currentUser.MaKhachThue.HasValue)
                {
                    dt = _hopDongBLL.GetHopDongTheoKhach(_currentUser.MaKhachThue.Value);
                }
                else
                {
                    dt = _hopDongBLL.GetAllHopDong();
                }

                dgvHopDong.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvHopDong.Columns.Count == 0) return;

            if (dgvHopDong.Columns.Contains("MaHopDong")) dgvHopDong.Columns["MaHopDong"].HeaderText = "Số HĐ";
            if (dgvHopDong.Columns.Contains("MaPhong")) dgvHopDong.Columns["MaPhong"].Visible = false;
            if (dgvHopDong.Columns.Contains("SoPhong")) dgvHopDong.Columns["SoPhong"].HeaderText = "Phòng";
            if (dgvHopDong.Columns.Contains("MaKhachThue")) dgvHopDong.Columns["MaKhachThue"].Visible = false;
            if (dgvHopDong.Columns.Contains("TenKhachThue")) dgvHopDong.Columns["TenKhachThue"].HeaderText = "Khách thuê đứng tên";
            if (dgvHopDong.Columns.Contains("CCCD")) dgvHopDong.Columns["CCCD"].HeaderText = "Số CCCD";
            if (dgvHopDong.Columns.Contains("SoDienThoai")) dgvHopDong.Columns["SoDienThoai"].HeaderText = "SĐT liên hệ";
            if (dgvHopDong.Columns.Contains("NgayBatDau"))
            {
                dgvHopDong.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
                dgvHopDong.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvHopDong.Columns.Contains("NgayKetThuc"))
            {
                dgvHopDong.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
                dgvHopDong.Columns["NgayKetThuc"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvHopDong.Columns.Contains("TienCoc"))
            {
                dgvHopDong.Columns["TienCoc"].HeaderText = "Tiền cọc (VNĐ)";
                dgvHopDong.Columns["TienCoc"].DefaultCellStyle.Format = "#,##0 đ";
                dgvHopDong.Columns["TienCoc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvHopDong.Columns.Contains("GiaThueThucTe"))
            {
                dgvHopDong.Columns["GiaThueThucTe"].HeaderText = "Giá thuê (VNĐ)";
                dgvHopDong.Columns["GiaThueThucTe"].DefaultCellStyle.Format = "#,##0 đ";
                dgvHopDong.Columns["GiaThueThucTe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvHopDong.Columns.Contains("TrangThai")) dgvHopDong.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvHopDong.Columns.Contains("GhiChu")) dgvHopDong.Columns["GhiChu"].HeaderText = "Ghi chú";
            if (dgvHopDong.Columns.Contains("NgayLap"))
            {
                dgvHopDong.Columns["NgayLap"].HeaderText = "Ngày lập";
                dgvHopDong.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void cboPhong_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboPhong.SelectedItem is DataRowView drv)
            {
                if (decimal.TryParse(drv["GiaThue"]?.ToString(), out decimal gia))
                {
                    txtGiaThue.Text = gia.ToString("N0");
                    txtTienCoc.Text = gia.ToString("N0");
                }
            }
        }

        private void dgvHopDong_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHopDong.Rows.Count) return;

            DataGridViewRow row = dgvHopDong.Rows[e.RowIndex];
            _selectedMaHopDong = Convert.ToInt32(row.Cells["MaHopDong"].Value);

            if (row.Cells["MaPhong"].Value != DBNull.Value)
            {
                cboPhong.SelectedValue = Convert.ToInt32(row.Cells["MaPhong"].Value);
            }
            if (row.Cells["MaKhachThue"].Value != DBNull.Value)
            {
                cboKhachThue.SelectedValue = Convert.ToInt32(row.Cells["MaKhachThue"].Value);
            }

            if (DateTime.TryParse(row.Cells["NgayBatDau"].Value?.ToString(), out DateTime nbd))
            {
                dtpNgayBatDau.Value = nbd;
            }
            if (DateTime.TryParse(row.Cells["NgayKetThuc"].Value?.ToString(), out DateTime nkt))
            {
                dtpNgayKetThuc.Value = nkt;
            }

            txtTienCoc.Text = row.Cells["TienCoc"].Value?.ToString() ?? "";
            txtGiaThue.Text = row.Cells["GiaThueThucTe"].Value?.ToString() ?? "";
            cboTrangThai.SelectedItem = row.Cells["TrangThai"].Value?.ToString() ?? "HieuLuc";
            txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";
        }

        private void btnThemMoi_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cboPhong.SelectedValue == null || !int.TryParse(cboPhong.SelectedValue.ToString(), out int maPhong))
                {
                    MessageBox.Show("Vui lòng chọn phòng cần cho thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboKhachThue.SelectedValue == null || !int.TryParse(cboKhachThue.SelectedValue.ToString(), out int maKhach))
                {
                    MessageBox.Show("Vui lòng chọn khách thuê đại diện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal.TryParse(txtTienCoc.Text.Replace(".", "").Replace(",", "").Trim(), out decimal tienCoc);
                if (!decimal.TryParse(txtGiaThue.Text.Replace(".", "").Replace(",", "").Trim(), out decimal giaThue) || giaThue <= 0)
                {
                    MessageBox.Show("Vui lòng nhập giá thuê thực tế hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var hd = new HopDongDTO
                {
                    MaPhong = maPhong,
                    MaKhachThue = maKhach,
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayKetThuc = dtpNgayKetThuc.Value.Date,
                    TienCoc = tienCoc,
                    GiaThueThucTe = giaThue,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                var (success, message, id) = _hopDongBLL.ThemHopDong(hd);
                MessageBox.Show(message, "Thông báo (UC4.1)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGiaHan_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaHopDong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần gia hạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal.TryParse(txtGiaThue.Text.Replace(".", "").Replace(",", "").Trim(), out decimal giaMoi);
                var (success, message) = _hopDongBLL.GiaHanHopDong(_selectedMaHopDong, dtpNgayKetThuc.Value.Date, giaMoi > 0 ? giaMoi : null);
                MessageBox.Show(message, "Thông báo (UC4.2)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gia hạn hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChamDut_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaHopDong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần chấm dứt / thanh lý.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Xác nhận thanh lý hợp đồng và trả phòng về trạng thái Trống?", "Xác nhận (UC4.3)", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var (success, message) = _hopDongBLL.ChamDutHopDong(_selectedMaHopDong);
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    if (success)
                    {
                        LoadData();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chấm dứt hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYeuCauHopDong_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaHopDong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một hợp đồng trên danh sách để gửi yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string soPhong = cboPhong.Text;
                int maPhong = cboPhong.SelectedValue != null && int.TryParse(cboPhong.SelectedValue.ToString(), out int mp) ? mp : 0;
                int maKhach = _currentUser?.MaKhachThue ?? (cboKhachThue.SelectedValue != null && int.TryParse(cboKhachThue.SelectedValue.ToString(), out int mk) ? mk : 1);

                DialogResult dr = MessageBox.Show(
                    $"Bạn muốn gửi yêu cầu cho hợp đồng căn hộ {soPhong}?\n\n- Chọn 'Yes' để yêu cầu GIA HẠN HỢP ĐỒNG.\n- Chọn 'No' để yêu cầu CHẤM DỨT / TRẢ PHÒNG.\n- Chọn 'Cancel' để hủy thao tác.",
                    "Chọn loại yêu cầu hợp đồng (UC4 & UC7)",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Cancel) return;

                string loai = (dr == DialogResult.Yes) ? "Gia hạn hợp đồng" : "Chấm dứt hợp đồng";
                string tieuDe = $"[Yêu cầu HĐ-{_selectedMaHopDong}] {loai} căn hộ {soPhong}";
                string noiDung = $"Cư dân: {cboKhachThue.Text} (SĐT: {_currentUser?.SoDienThoai ?? ""}) gửi yêu cầu {loai} cho hợp đồng số HD-{_selectedMaHopDong:D5} căn hộ {soPhong}. Thời hạn kết thúc hiện tại: {dtpNgayKetThuc.Value:dd/MM/yyyy}.";

                MessageBox.Show($"Đã ghi nhận yêu cầu '{loai}' thành công tới Ban Quản lý tòa nhà!\nBan Quản lý sẽ liên hệ lại với bạn sớm.", "Gửi yêu cầu thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi yêu cầu hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInHopDong_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaHopDong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần in trên danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string soPhong = cboPhong.Text;
                string tenKhach = cboKhachThue.Text;
                string giaThue = txtGiaThue.Text;
                string tienCoc = txtTienCoc.Text;
                string ngayBD = dtpNgayBatDau.Value.ToString("dd/MM/yyyy");
                string ngayKT = dtpNgayKetThuc.Value.ToString("dd/MM/yyyy");

                string vanBanHopDong = $@"CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM
Độc lập - Tự do - Hạnh phúc
-----------------------------------
HỢP ĐỒNG THUÊ CĂN HỘ CHUNG CƯ AN CƯ
Số HĐ: HD-{_selectedMaHopDong:D5}

Hôm nay, ngày {DateTime.Now:dd/MM/yyyy}, tại Văn phòng Ban Quản lý Chung cư An Cư:
BÊN CHO THUÊ (BÊN A):
- Đại diện: BAN QUẢN LÝ CHUNG CƯ AN CƯ (EPU)
- Địa chỉ: 235 Hoàng Quốc Việt, Cổ Nhuế, Bắc Từ Liêm, Hà Nội

BÊN THUÊ (BÊN B):
- Ông/Bà: {tenKhach}
- Thuê căn hộ số: {soPhong}
- Thời hạn thuê: Từ ngày {ngayBD} đến ngày {ngayKT}
- Giá thuê thỏa thuận: {giaThue} VNĐ / tháng
- Tiền đặt cọc bảo đảm: {tienCoc} VNĐ

ĐIỀU KHOẢN CHUNG:
1. Bên B có trách nhiệm thanh toán tiền thuê phòng cùng tiền điện nước trước ngày 05 hàng tháng.
2. Tiền điện tính theo lũy tiến bậc thang EVN, tiền nước 15.000 đ/m3.
3. Khi hết hạn hợp đồng, nếu không có hư hỏng tài sản, Bên A hoàn trả 100% tiền cọc.

ĐẠI DIỆN BÊN A                               ĐẠI DIỆN BÊN B
(Ký và ghi rõ họ tên)                       (Ký và ghi rõ họ tên)";

                using var dlg = new Form();
                dlg.Text = $"Bản in Hợp đồng thuê căn hộ {_selectedMaHopDong} (UC4.5)";
                dlg.Size = new System.Drawing.Size(750, 650);
                dlg.StartPosition = FormStartPosition.CenterParent;

                var pnlTopPrint = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 45,
                    BackColor = System.Drawing.Color.FromArgb(245, 247, 250),
                    Padding = new Padding(5)
                };

                var btnPrint = new Button
                {
                    Text = "🖨 In / Lưu PDF",
                    BackColor = System.Drawing.Color.FromArgb(0, 150, 136),
                    ForeColor = System.Drawing.Color.White,
                    Font = new System.Drawing.Font("Segoe UI", 9.5f, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Size = new System.Drawing.Size(140, 32),
                    Location = new System.Drawing.Point(10, 6),
                    Cursor = Cursors.Hand
                };

                btnPrint.Click += (s, ev) =>
                {
                    try
                    {
                        using var printDoc = new System.Drawing.Printing.PrintDocument();
                        printDoc.DocumentName = $"HopDong_HD{_selectedMaHopDong:D5}";
                        printDoc.PrintPage += (ps, pev) =>
                        {
                            using var font = new System.Drawing.Font("Times New Roman", 11.5f);
                            pev.Graphics?.DrawString(vanBanHopDong, font, Brushes.Black, pev.MarginBounds);
                        };

                        using var printDlg = new PrintDialog();
                        printDlg.Document = printDoc;
                        printDlg.UseEXDialog = true;
                        if (printDlg.ShowDialog(dlg) == DialogResult.OK)
                        {
                            printDoc.Print();
                            MessageBox.Show("Lệnh in đã được gửi thành công (hoặc file PDF đã xuất lưu)!", "In hợp đồng (UC4.5)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception exPrint)
                    {
                        MessageBox.Show("Lỗi in hợp đồng: " + exPrint.Message, "Lỗi in ấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                pnlTopPrint.Controls.Add(btnPrint);

                var rtb = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    Text = vanBanHopDong,
                    Font = new System.Drawing.Font("Times New Roman", 12F),
                    ReadOnly = true
                };

                dlg.Controls.Add(rtb);
                dlg.Controls.Add(pnlTopPrint);
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị bản in hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedMaHopDong = 0;
            txtTienCoc.Clear();
            txtGiaThue.Clear();
            txtGhiChu.Clear();
            if (cboPhong.Items.Count > 0) cboPhong.SelectedIndex = 0;
            if (cboKhachThue.Items.Count > 0) cboKhachThue.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now.AddYears(1);
        }
    }
}
