using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AnCuManagement.BLL;
using AnCuManagement.Common;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    /// <summary>
    /// Giao diện Quản lý danh mục phòng & Trạng thái cho thuê (UC2.1 - UC2.5)
    /// Do Phan Công Thành Đạt (MSSV: 24810310223) phụ trách thiết kế & lập trình.
    /// </summary>
    public partial class FormPhong : Form
    {
        private readonly PhongBLL _phongBLL = new();
        private readonly TaiKhoanDTO? _currentUser;
        private int _selectedMaPhong = 0;

        public FormPhong(TaiKhoanDTO? currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void FormPhong_Load(object? sender, EventArgs e)
        {
            LoadToaNhaCombo();
            cboTrangThai.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            cboLocTang.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndex = 0;

            LoadData();
        }

        private void LoadToaNhaCombo()
        {
            try
            {
                DataTable dt = _phongBLL.GetAllToaNha();
                if (dt.Rows.Count > 0)
                {
                    cboToaNha.DisplayMember = "TenToaNha";
                    cboToaNha.ValueMember = "MaToaNha";
                    cboToaNha.DataSource = dt;
                    if (cboToaNha.Items.Count > 0) cboToaNha.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục tòa nhà: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _phongBLL.GetAllPhong();
                dgvPhong.DataSource = dt;
                FormatGrid();
                LoadRoomCards(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoomCards(DataTable dtPhong)
        {
            try
            {
                pnlRoomCards.SuspendLayout();
                pnlRoomCards.Controls.Clear();

                foreach (DataRow row in dtPhong.Rows)
                {
                    var card = new AnCuManagement.GUI.UserControls.UCRoomStatusCard
                    {
                        MaPhong = Convert.ToInt32(row["MaPhong"]),
                        SoPhong = row["SoPhong"]?.ToString() ?? "",
                        TrangThai = row["TrangThai"]?.ToString() ?? AppConstants.TRANG_THAI_PHONG_TRONG,
                        GiaThue = FormatHelper.FormatTienVND(Convert.ToDecimal(row["GiaThue"] ?? 0)),
                        ThongTinPhu = $"Tầng {row["Tang"]} | {row["DienTich"]} m²",
                        Margin = new Padding(8),
                        Cursor = Cursors.Hand
                    };

                    AttachCardClickEvents(card, row);
                    pnlRoomCards.Controls.Add(card);
                }

                pnlRoomCards.ResumeLayout();
            }
            catch { }
        }

        private void AttachCardClickEvents(Control c, DataRow row)
        {
            c.Click += (s, e) => SelectRoomFromRow(row);
            foreach (Control child in c.Controls)
            {
                AttachCardClickEvents(child, row);
            }
        }

        private void SelectRoomFromRow(DataRow row)
        {
            try
            {
                _selectedMaPhong = Convert.ToInt32(row["MaPhong"]);
                txtSoPhong.Text = row["SoPhong"]?.ToString() ?? "";
                nudTang.Value = Convert.ToDecimal(row["Tang"] ?? 1);
                nudDienTich.Value = Convert.ToDecimal(row["DienTich"] ?? 45);
                txtGiaThue.Text = row["GiaThue"]?.ToString() ?? "";
                cboTrangThai.SelectedItem = row["TrangThai"]?.ToString() ?? "Trống";
                cboLoaiPhong.SelectedItem = row["LoaiPhong"]?.ToString() ?? "Phòng Đơn";
                txtMoTa.Text = row["MoTa"]?.ToString() ?? "";

                if (row["MaToaNha"] != DBNull.Value && int.TryParse(row["MaToaNha"].ToString(), out int maTN))
                {
                    cboToaNha.SelectedValue = maTN;
                }

                // Chọn dòng tương ứng trên DataGridView
                foreach (DataGridViewRow gridRow in dgvPhong.Rows)
                {
                    if (gridRow.Cells["MaPhong"]?.Value != null && Convert.ToInt32(gridRow.Cells["MaPhong"].Value) == _selectedMaPhong)
                    {
                        gridRow.Selected = true;
                        dgvPhong.CurrentCell = gridRow.Cells["SoPhong"];
                        break;
                    }
                }
            }
            catch { }
        }

        private void FormatGrid()
        {
            if (dgvPhong.Columns.Count == 0) return;

            if (dgvPhong.Columns.Contains("MaPhong")) dgvPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
            if (dgvPhong.Columns.Contains("SoPhong")) dgvPhong.Columns["SoPhong"].HeaderText = "Số phòng";
            if (dgvPhong.Columns.Contains("Tang")) dgvPhong.Columns["Tang"].HeaderText = "Tầng";
            if (dgvPhong.Columns.Contains("DienTich"))
            {
                dgvPhong.Columns["DienTich"].HeaderText = "Diện tích (m²)";
                dgvPhong.Columns["DienTich"].DefaultCellStyle.Format = "N1";
            }
            if (dgvPhong.Columns.Contains("GiaThue"))
            {
                dgvPhong.Columns["GiaThue"].HeaderText = "Giá thuê (VNĐ)";
                dgvPhong.Columns["GiaThue"].DefaultCellStyle.Format = "#,##0 đ";
                dgvPhong.Columns["GiaThue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvPhong.Columns.Contains("TrangThai")) dgvPhong.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvPhong.Columns.Contains("LoaiPhong")) dgvPhong.Columns["LoaiPhong"].HeaderText = "Loại phòng";
            if (dgvPhong.Columns.Contains("MoTa")) dgvPhong.Columns["MoTa"].HeaderText = "Mô tả";
            if (dgvPhong.Columns.Contains("MaToaNha")) dgvPhong.Columns["MaToaNha"].Visible = false;
            if (dgvPhong.Columns.Contains("TenToaNha")) dgvPhong.Columns["TenToaNha"].HeaderText = "Tòa nhà";
        }

        private void dgvPhong_RowPrePaint(object? sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPhong.Rows.Count) return;

            var row = dgvPhong.Rows[e.RowIndex];
            if (row.Cells["TrangThai"].Value == null) return;

            string status = row.Cells["TrangThai"].Value.ToString() ?? "";
            if (AppConstants.IsTrong(status))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233);
            }
            else if (AppConstants.IsDangThue(status))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(227, 242, 253);
            }
            else if (AppConstants.IsSuaChua(status))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
            }
            else if (AppConstants.IsDatCoc(status))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225);
            }
        }

        private void dgvPhong_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPhong.Rows.Count) return;

            DataGridViewRow row = dgvPhong.Rows[e.RowIndex];
            if (row.DataBoundItem is DataRowView drv)
            {
                SelectRoomFromRow(drv.Row);
            }
            else
            {
                _selectedMaPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);
                txtSoPhong.Text = row.Cells["SoPhong"].Value?.ToString() ?? "";
                nudTang.Value = Convert.ToDecimal(row.Cells["Tang"].Value ?? 1);
                nudDienTich.Value = Convert.ToDecimal(row.Cells["DienTich"].Value ?? 45);
                txtGiaThue.Text = row.Cells["GiaThue"].Value?.ToString() ?? "";
                cboTrangThai.SelectedItem = row.Cells["TrangThai"].Value?.ToString() ?? "Trống";
                cboLoaiPhong.SelectedItem = row.Cells["LoaiPhong"].Value?.ToString() ?? "Phòng Đơn";
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                if (row.Cells["MaToaNha"].Value != DBNull.Value && int.TryParse(row.Cells["MaToaNha"].Value.ToString(), out int maTN))
                {
                    cboToaNha.SelectedValue = maTN;
                }
            }
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtGiaThue.Text.Replace(".", "").Replace(",", "").Trim(), out decimal giaThue))
                {
                    MessageBox.Show("Vui lòng nhập giá thuê hợp lệ dạng số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maToaNha = 1;
                if (cboToaNha.SelectedValue != null && int.TryParse(cboToaNha.SelectedValue.ToString(), out int tnId))
                {
                    maToaNha = tnId;
                }

                var p = new PhongDTO
                {
                    SoPhong = txtSoPhong.Text.Trim(),
                    Tang = (int)nudTang.Value,
                    DienTich = nudDienTich.Value,
                    GiaThue = giaThue,
                    TrangThai = cboTrangThai.SelectedItem?.ToString() ?? AppConstants.TRANG_THAI_PHONG_TRONG,
                    LoaiPhong = cboLoaiPhong.SelectedItem?.ToString() ?? "Phòng Đơn",
                    MoTa = txtMoTa.Text.Trim(),
                    MaToaNha = maToaNha
                };

                var (success, message, id) = _phongBLL.ThemPhong(p);
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaPhong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một phòng trên danh sách để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtGiaThue.Text.Replace(".", "").Replace(",", "").Trim(), out decimal giaThue))
                {
                    MessageBox.Show("Vui lòng nhập giá thuê hợp lệ dạng số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maToaNha = 1;
                if (cboToaNha.SelectedValue != null && int.TryParse(cboToaNha.SelectedValue.ToString(), out int tnId))
                {
                    maToaNha = tnId;
                }

                var p = new PhongDTO
                {
                    MaPhong = _selectedMaPhong,
                    SoPhong = txtSoPhong.Text.Trim(),
                    Tang = (int)nudTang.Value,
                    DienTich = nudDienTich.Value,
                    GiaThue = giaThue,
                    TrangThai = cboTrangThai.SelectedItem?.ToString() ?? AppConstants.TRANG_THAI_PHONG_TRONG,
                    LoaiPhong = cboLoaiPhong.SelectedItem?.ToString() ?? "Phòng Đơn",
                    MoTa = txtMoTa.Text.Trim(),
                    MaToaNha = maToaNha
                };

                var (success, message) = _phongBLL.CapNhatPhong(p);
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiTrangThai_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaPhong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một phòng cần đổi trạng thái!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string trangThaiMoi = cboTrangThai.SelectedItem?.ToString() ?? AppConstants.TRANG_THAI_PHONG_TRONG;
                var (success, message) = _phongBLL.CapNhatTrangThaiPhong(_selectedMaPhong, trangThaiMoi);
                MessageBox.Show(message, "Đổi trạng thái phòng (UC2.3)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhatGia_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaPhong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một phòng cần cập nhật giá thuê!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtGiaThue.Text.Replace(".", "").Replace(",", "").Trim(), out decimal giaThueMoi) || giaThueMoi <= 0)
                {
                    MessageBox.Show("Vui lòng nhập giá thuê hợp lệ lớn hơn 0 VNĐ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiaThue.Focus();
                    return;
                }

                var (success, message) = _phongBLL.CapNhatGiaThue(_selectedMaPhong, giaThueMoi);
                MessageBox.Show(message, "Cập nhật giá thuê (UC2.5)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật giá thuê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                int? tang = cboLocTang.SelectedIndex > 0 ? cboLocTang.SelectedIndex : null;
                string? trangThai = cboLocTrangThai.SelectedIndex > 0 ? cboLocTrangThai.SelectedItem?.ToString() : null;

                decimal? giaMin = null;
                if (!string.IsNullOrWhiteSpace(txtGiaMin.Text) && decimal.TryParse(txtGiaMin.Text.Replace(".", "").Replace(",", "").Trim(), out decimal minVal))
                {
                    giaMin = minVal;
                }

                decimal? giaMax = null;
                if (!string.IsNullOrWhiteSpace(txtGiaMax.Text) && decimal.TryParse(txtGiaMax.Text.Replace(".", "").Replace(",", "").Trim(), out decimal maxVal))
                {
                    giaMax = maxVal;
                }

                DataTable dt = _phongBLL.TimKiemPhong(tang, trangThai, giaMin, giaMax);
                dgvPhong.DataSource = dt;
                FormatGrid();
                LoadRoomCards(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTatCa_Click(object? sender, EventArgs e)
        {
            try
            {
                cboLocTang.SelectedIndex = 0;
                cboLocTrangThai.SelectedIndex = 0;
                txtGiaMin.Clear();
                txtGiaMax.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tất cả danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedMaPhong = 0;
            txtSoPhong.Clear();
            nudTang.Value = 1;
            nudDienTich.Value = 45;
            txtGiaThue.Clear();
            txtMoTa.Clear();
            cboTrangThai.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            if (cboToaNha.Items.Count > 0) cboToaNha.SelectedIndex = 0;
            txtSoPhong.Focus();
        }
    }
}
