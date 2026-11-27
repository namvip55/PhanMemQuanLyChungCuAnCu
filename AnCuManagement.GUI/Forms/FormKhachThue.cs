using System;
using System.Data;
using System.Windows.Forms;
using AnCuManagement.BLL;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    /// <summary>
    /// Giao diện Quản lý hồ sơ khách thuê & Xác thực CCCD 12 số (UC3.1 - UC3.5)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách thiết kế & lập trình.
    /// </summary>
    public partial class FormKhachThue : Form
    {
        private readonly KhachThueBLL _khachThueBLL = new();
        private readonly PhongBLL _phongBLL = new();
        private readonly TaiKhoanDTO? _currentUser;
        private int _selectedMaKhachThue = 0;

        public FormKhachThue(TaiKhoanDTO? currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void FormKhachThue_Load(object? sender, EventArgs e)
        {
            cboGioiTinh.SelectedIndex = 0;
            LoadPhongComboBox();
            LoadData();
        }

        private void LoadPhongComboBox()
        {
            try
            {
                DataTable dt = _phongBLL.GetAllPhong();
                DataRow emptyRow = dt.NewRow();
                emptyRow["MaPhong"] = DBNull.Value;
                emptyRow["SoPhong"] = "-- Chưa gán phòng --";
                dt.Rows.InsertAt(emptyRow, 0);

                cboPhong.DisplayMember = "SoPhong";
                cboPhong.ValueMember = "MaPhong";
                cboPhong.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _khachThueBLL.GetAllKhachThue();
                dgvKhachThue.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hồ sơ cư dân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvKhachThue.Columns.Count == 0) return;

            if (dgvKhachThue.Columns.Contains("MaKhachThue")) dgvKhachThue.Columns["MaKhachThue"].HeaderText = "Mã cư dân";
            if (dgvKhachThue.Columns.Contains("HoTen")) dgvKhachThue.Columns["HoTen"].HeaderText = "Họ và tên";
            if (dgvKhachThue.Columns.Contains("CCCD")) dgvKhachThue.Columns["CCCD"].HeaderText = "Số CCCD (12 số)";
            if (dgvKhachThue.Columns.Contains("SoDienThoai")) dgvKhachThue.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            if (dgvKhachThue.Columns.Contains("Email")) dgvKhachThue.Columns["Email"].HeaderText = "Email";
            if (dgvKhachThue.Columns.Contains("NgaySinh"))
            {
                dgvKhachThue.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvKhachThue.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvKhachThue.Columns.Contains("GioiTinh")) dgvKhachThue.Columns["GioiTinh"].HeaderText = "Giới tính";
            if (dgvKhachThue.Columns.Contains("QueQuan")) dgvKhachThue.Columns["QueQuan"].HeaderText = "Quê quán";
            if (dgvKhachThue.Columns.Contains("DiaChiThuongTru")) dgvKhachThue.Columns["DiaChiThuongTru"].HeaderText = "Thường trú";
            if (dgvKhachThue.Columns.Contains("MaPhong")) dgvKhachThue.Columns["MaPhong"].Visible = false;
            if (dgvKhachThue.Columns.Contains("SoPhong")) dgvKhachThue.Columns["SoPhong"].HeaderText = "Phòng đang ở";
            if (dgvKhachThue.Columns.Contains("LaChuHo")) dgvKhachThue.Columns["LaChuHo"].HeaderText = "Chủ hộ";
            if (dgvKhachThue.Columns.Contains("IsActive")) dgvKhachThue.Columns["IsActive"].Visible = false;
            if (dgvKhachThue.Columns.Contains("NgayDangKy"))
            {
                dgvKhachThue.Columns["NgayDangKy"].HeaderText = "Ngày đăng ký";
                dgvKhachThue.Columns["NgayDangKy"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void dgvKhachThue_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvKhachThue.Rows.Count) return;

                DataGridViewRow row = dgvKhachThue.Rows[e.RowIndex];
                _selectedMaKhachThue = Convert.ToInt32(row.Cells["MaKhachThue"].Value);
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                txtCCCD.Text = row.Cells["CCCD"].Value?.ToString() ?? "";
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                string gt = row.Cells["GioiTinh"].Value?.ToString() ?? "Nam";
                if (gt.StartsWith("N", StringComparison.OrdinalIgnoreCase) && !gt.Equals("Nam", StringComparison.OrdinalIgnoreCase))
                    cboGioiTinh.SelectedItem = "Nữ";
                else
                    cboGioiTinh.SelectedItem = "Nam";
                txtQueQuan.Text = row.Cells["QueQuan"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChiThuongTru"].Value?.ToString() ?? "";
                chkLaChuHo.Checked = Convert.ToBoolean(row.Cells["LaChuHo"].Value ?? false);

                if (DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out DateTime ns))
                {
                    dtpNgaySinh.Value = ns;
                }

                if (row.Cells["MaPhong"].Value != DBNull.Value && int.TryParse(row.Cells["MaPhong"].Value.ToString(), out int maPhong))
                {
                    cboPhong.SelectedValue = maPhong;
                }
                else
                {
                    cboPhong.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                int? maPhong = null;
                if (cboPhong.SelectedIndex > 0 && int.TryParse(cboPhong.SelectedValue?.ToString(), out int mp))
                {
                    maPhong = mp;
                }

                var kt = new KhachThueDTO
                {
                    HoTen = txtHoTen.Text.Trim(),
                    CCCD = txtCCCD.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value.Date,
                    GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                    QueQuan = txtQueQuan.Text.Trim(),
                    DiaChiThuongTru = txtDiaChi.Text.Trim(),
                    MaPhong = maPhong,
                    LaChuHo = chkLaChuHo.Checked
                };

                bool taoTaiKhoan = chkAutoCreateAccount.Checked;
                var (success, message, id) = _khachThueBLL.ThemKhachThue(kt, taoTaiKhoan);
                MessageBox.Show(message, "Thông báo (UC3.2)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm cư dân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaKhachThue <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một cư dân trên danh sách để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int? maPhong = null;
                if (cboPhong.SelectedIndex > 0 && int.TryParse(cboPhong.SelectedValue?.ToString(), out int mp))
                {
                    maPhong = mp;
                }

                var kt = new KhachThueDTO
                {
                    MaKhachThue = _selectedMaKhachThue,
                    HoTen = txtHoTen.Text.Trim(),
                    CCCD = txtCCCD.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value.Date,
                    GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam",
                    QueQuan = txtQueQuan.Text.Trim(),
                    DiaChiThuongTru = txtDiaChi.Text.Trim(),
                    MaPhong = maPhong,
                    LaChuHo = chkLaChuHo.Checked
                };

                var (success, message) = _khachThueBLL.CapNhatKhachThue(kt);
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật cư dân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaKhachThue <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một cư dân cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa hồ sơ cư dân này (xóa mềm lưu lịch sử)?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var (success, message) = _khachThueBLL.XoaMemKhachThue(_selectedMaKhachThue);
                    MessageBox.Show(message, "Thông báo (UC3.4)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    if (success)
                    {
                        LoadData();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa cư dân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                DataTable dt = _khachThueBLL.TimKiemKhachThue(string.IsNullOrEmpty(keyword) ? null : keyword);
                dgvKhachThue.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm cư dân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacThuc_Click(object? sender, EventArgs e)
        {
            try
            {
                string input = txtTimKiem.Text.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Vui lòng nhập số CCCD hoặc Số điện thoại vào ô tìm kiếm để xác thực định danh cư dân!", "Yêu cầu thông tin (UC3.5)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTimKiem.Focus();
                    return;
                }

                var (success, message, row) = _khachThueBLL.XacThucKhachThue(input);
                if (success && row != null)
                {
                    MessageBox.Show(message, "Kết quả xác thực định danh (UC3.5)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = _khachThueBLL.TimKiemKhachThue(input);
                    dgvKhachThue.DataSource = dt;
                    FormatGrid();
                }
                else
                {
                    MessageBox.Show(message, "Không tìm thấy hồ sơ (UC3.5)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xác thực cư dân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTatCa_Click(object? sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải toàn bộ danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedMaKhachThue = 0;
            txtHoTen.Clear();
            txtCCCD.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtQueQuan.Clear();
            txtDiaChi.Clear();
            cboGioiTinh.SelectedIndex = 0;
            cboPhong.SelectedIndex = 0;
            chkLaChuHo.Checked = false;
            chkAutoCreateAccount.Checked = true;
            dtpNgaySinh.Value = new DateTime(1995, 1, 1);
            txtHoTen.Focus();
        }
    }
}
