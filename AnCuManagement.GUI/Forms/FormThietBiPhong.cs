using System;
using System.Data;
using System.Windows.Forms;
using AnCuManagement.BLL;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    /// <summary>
    /// Giao diện Quản lý trang thiết bị & tiện nghi bàn giao phòng (UC2)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách thiết kế & lập trình.
    /// </summary>
    public partial class FormThietBiPhong : Form
    {
        private readonly ThietBiPhongBLL _thietBiBLL = new();
        private readonly PhongBLL _phongBLL = new();
        private readonly TaiKhoanDTO? _currentUser;
        private int _selectedMaThietBi = 0;

        public FormThietBiPhong(TaiKhoanDTO? currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void FormThietBiPhong_Load(object? sender, EventArgs e)
        {
            cboTinhTrang.SelectedIndex = 0;
            LoadPhongComboBox();
        }

        private void LoadPhongComboBox()
        {
            try
            {
                DataTable dt = _phongBLL.GetAllPhong();
                cboPhong.DisplayMember = "SoPhong";
                cboPhong.ValueMember = "MaPhong";
                cboPhong.DataSource = dt;

                if (cboPhong.Items.Count > 0)
                {
                    cboPhong.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboPhong_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadThietBiTheoPhong();
        }

        private void LoadThietBiTheoPhong()
        {
            if (cboPhong.SelectedValue == null || !int.TryParse(cboPhong.SelectedValue.ToString(), out int maPhong))
            {
                return;
            }

            try
            {
                DataTable dt = _thietBiBLL.GetThietBiTheoPhong(maPhong);
                dgvThietBi.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvThietBi.Columns.Count == 0) return;

            if (dgvThietBi.Columns.Contains("MaThietBi")) dgvThietBi.Columns["MaThietBi"].HeaderText = "Mã thiết bị";
            if (dgvThietBi.Columns.Contains("MaPhong")) dgvThietBi.Columns["MaPhong"].Visible = false;
            if (dgvThietBi.Columns.Contains("TenThietBi")) dgvThietBi.Columns["TenThietBi"].HeaderText = "Tên thiết bị";
            if (dgvThietBi.Columns.Contains("SoLuong")) dgvThietBi.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgvThietBi.Columns.Contains("TinhTrang")) dgvThietBi.Columns["TinhTrang"].HeaderText = "Tình trạng";
            if (dgvThietBi.Columns.Contains("NgayLapDat"))
            {
                dgvThietBi.Columns["NgayLapDat"].HeaderText = "Ngày lắp đặt";
                dgvThietBi.Columns["NgayLapDat"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvThietBi.Columns.Contains("GhiChu")) dgvThietBi.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private void dgvThietBi_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvThietBi.Rows.Count) return;

                DataGridViewRow row = dgvThietBi.Rows[e.RowIndex];
                _selectedMaThietBi = Convert.ToInt32(row.Cells["MaThietBi"].Value);
                txtTenThietBi.Text = row.Cells["TenThietBi"].Value?.ToString() ?? "";
                nudSoLuong.Value = Convert.ToDecimal(row.Cells["SoLuong"].Value ?? 1);
                cboTinhTrang.SelectedItem = row.Cells["TinhTrang"].Value?.ToString() ?? "Tốt";
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";

                if (DateTime.TryParse(row.Cells["NgayLapDat"].Value?.ToString(), out DateTime ngay))
                {
                    dtpNgayLapDat.Value = ngay;
                }
            }
            catch { }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cboPhong.SelectedValue == null || !int.TryParse(cboPhong.SelectedValue.ToString(), out int maPhong))
                {
                    MessageBox.Show("Vui lòng chọn phòng cần gán thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tb = new ThietBiPhongDTO
                {
                    MaPhong = maPhong,
                    TenThietBi = txtTenThietBi.Text.Trim(),
                    SoLuong = (int)nudSoLuong.Value,
                    TinhTrang = cboTinhTrang.SelectedItem?.ToString() ?? "Tốt",
                    NgayLapDat = dtpNgayLapDat.Value.Date,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                var (success, message, id) = _thietBiBLL.ThemThietBi(tb);
                MessageBox.Show(message, "Thông báo (UC2.2)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadThietBiTheoPhong();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaThietBi <= 0)
                {
                    MessageBox.Show("Vui lòng chọn thiết bị trên danh sách để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maPhong = Convert.ToInt32(cboPhong.SelectedValue);
                var tb = new ThietBiPhongDTO
                {
                    MaThietBi = _selectedMaThietBi,
                    MaPhong = maPhong,
                    TenThietBi = txtTenThietBi.Text.Trim(),
                    SoLuong = (int)nudSoLuong.Value,
                    TinhTrang = cboTinhTrang.SelectedItem?.ToString() ?? "Tốt",
                    NgayLapDat = dtpNgayLapDat.Value.Date,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                var (success, message) = _thietBiBLL.CapNhatThietBi(tb);
                MessageBox.Show(message, "Thông báo (UC2.2)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadThietBiTheoPhong();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaThietBi <= 0)
                {
                    MessageBox.Show("Vui lòng chọn thiết bị cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa thiết bị này khỏi phòng?", "Xác nhận (UC2.2)", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var (success, message) = _thietBiBLL.XoaThietBi(_selectedMaThietBi);
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    if (success)
                    {
                        LoadThietBiTheoPhong();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedMaThietBi = 0;
            txtTenThietBi.Clear();
            nudSoLuong.Value = 1;
            cboTinhTrang.SelectedIndex = 0;
            dtpNgayLapDat.Value = DateTime.Now;
            txtGhiChu.Clear();
            txtTenThietBi.Focus();
        }
    }
}
