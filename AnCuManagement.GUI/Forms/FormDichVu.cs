using System;
using System.Data;
using System.Windows.Forms;
using AnCuManagement.BLL;
using AnCuManagement.Common;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    /// <summary>
    /// Giao diện Quản lý danh mục & biểu phí dịch vụ chung cư (UC5.2, UC5.5)
    /// Do Lê Ngọc Quyền (MSSV: 24810310259) phụ trách thiết kế & lập trình.
    /// </summary>
    public partial class FormDichVu : Form
    {
        private readonly DichVuBLL _dichVuBLL = new();
        private readonly TaiKhoanDTO? _currentUser;
        private int _selectedMaDichVu = 0;

        public FormDichVu(TaiKhoanDTO? currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void FormDichVu_Load(object? sender, EventArgs e)
        {
            LoadData();
            LoadBangEVN();

            if (_currentUser != null && _currentUser.QuyenHan == AppConstants.QUYEN_KHACH_THUE)
            {
                btnThem.Visible = false;
                btnSua.Visible = false;
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _dichVuBLL.GetAllDichVu();
                dgvDichVu.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvDichVu.Columns.Count == 0) return;

            if (dgvDichVu.Columns.Contains("MaDichVu")) dgvDichVu.Columns["MaDichVu"].HeaderText = "Mã DV";
            if (dgvDichVu.Columns.Contains("TenDichVu")) dgvDichVu.Columns["TenDichVu"].HeaderText = "Tên dịch vụ";
            if (dgvDichVu.Columns.Contains("DonGia"))
            {
                dgvDichVu.Columns["DonGia"].HeaderText = "Đơn giá (VNĐ)";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "#,##0 đ";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDichVu.Columns.Contains("DonViTinh")) dgvDichVu.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
            if (dgvDichVu.Columns.Contains("MoTa")) dgvDichVu.Columns["MoTa"].HeaderText = "Mô tả";
            if (dgvDichVu.Columns.Contains("IsActive")) dgvDichVu.Columns["IsActive"].HeaderText = "Áp dụng";
        }

        private void LoadBangEVN()
        {
            var dt = new DataTable();
            dt.Columns.Add("Bậc thang", typeof(string));
            dt.Columns.Add("Mức tiêu thụ (kWh)", typeof(string));
            dt.Columns.Add("Đơn giá (đ/kWh)", typeof(string));

            dt.Rows.Add("Bậc 1", "Cho kWh từ 0 - 50", "1.893 đ");
            dt.Rows.Add("Bậc 2", "Cho kWh từ 51 - 100", "1.956 đ");
            dt.Rows.Add("Bậc 3", "Cho kWh từ 101 - 200", "2.271 đ");
            dt.Rows.Add("Bậc 4", "Cho kWh từ 201 - 300", "2.860 đ");
            dt.Rows.Add("Bậc 5", "Cho kWh từ 301 - 400", "3.197 đ");
            dt.Rows.Add("Bậc 6", "Cho kWh từ 401 trở lên", "3.302 đ");

            dgvEVN.DataSource = dt;
        }

        private void dgvDichVu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDichVu.Rows.Count) return;

            DataGridViewRow row = dgvDichVu.Rows[e.RowIndex];
            _selectedMaDichVu = Convert.ToInt32(row.Cells["MaDichVu"].Value);
            txtTenDichVu.Text = row.Cells["TenDichVu"].Value?.ToString() ?? "";
            txtDonGia.Text = row.Cells["DonGia"].Value?.ToString() ?? "";
            txtDonViTinh.Text = row.Cells["DonViTinh"].Value?.ToString() ?? "";
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString() ?? "";
            chkIsActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value ?? true);
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtDonGia.Text.Replace(".", "").Replace(",", "").Trim(), out decimal donGia) || donGia <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đơn giá dịch vụ hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dv = new DichVuDTO
                {
                    TenDichVu = txtTenDichVu.Text.Trim(),
                    DonGia = donGia,
                    DonViTinh = txtDonViTinh.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };

                var (success, message, id) = _dichVuBLL.ThemDichVu(dv);
                MessageBox.Show(message, "Thông báo (UC5.3)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedMaDichVu <= 0)
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ trên danh sách để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDonGia.Text.Replace(".", "").Replace(",", "").Trim(), out decimal donGia) || donGia <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đơn giá dịch vụ hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dv = new DichVuDTO
                {
                    MaDichVu = _selectedMaDichVu,
                    TenDichVu = txtTenDichVu.Text.Trim(),
                    DonGia = donGia,
                    DonViTinh = txtDonViTinh.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    IsActive = chkIsActive.Checked
                };

                var (success, message) = _dichVuBLL.CapNhatDichVu(dv);
                MessageBox.Show(message, "Thông báo (UC5.3)", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (success)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedMaDichVu = 0;
            txtTenDichVu.Clear();
            txtDonGia.Clear();
            txtDonViTinh.Text = "đồng/tháng";
            txtMoTa.Clear();
            chkIsActive.Checked = true;
            txtTenDichVu.Focus();
        }
    }
}
