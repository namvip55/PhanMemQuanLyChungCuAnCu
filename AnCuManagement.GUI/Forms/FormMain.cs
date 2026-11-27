using System;
using System.Windows.Forms;
using AnCuManagement.Common;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    public partial class FormMain : Form
    {
        private readonly TaiKhoanDTO _currentUser;
        private Form? _activeForm = null;

        public FormMain(TaiKhoanDTO currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void FormMain_Load(object? sender, EventArgs e)
        {
            lblNguoiDung.Text = $"Người dùng: {_currentUser.HoTen} ({_currentUser.TenDangNhap})";
            lblVaiTro.Text = $"Vai trò: {GetRoleDisplayName(_currentUser.QuyenHan)}";
            UpdateClock();
            PhanQuyenMenu();

            if (_currentUser.QuyenHan != AppConstants.QUYEN_KHACH_THUE)
            {
                OpenChildForm(new FormPhong(_currentUser));
            }
        }

        private string GetRoleDisplayName(string quyenHan)
        {
            return quyenHan switch
            {
                AppConstants.QUYEN_CHU_NHA => "Chủ chung cư",
                AppConstants.QUYEN_QUAN_LY => "Quản lý tòa nhà",
                AppConstants.QUYEN_KHACH_THUE => "Cư dân / Khách thuê",
                _ => quyenHan
            };
        }

        private void PhanQuyenMenu()
        {
            if (_currentUser.QuyenHan == AppConstants.QUYEN_KHACH_THUE)
            {
                mnuQuanLyPhong.Visible = false;
                mnuQuanLyKhach.Visible = false;
                mnuBaoCaoThongKe.Visible = false;
            }
            else
            {
                mnuQuanLyPhong.Visible = true;
                mnuQuanLyKhach.Visible = true;
                mnuBaoCaoThongKe.Visible = true;
            }
        }

        public void OpenChildForm(Form childForm)
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
            }

            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void timerClock_Tick(object? sender, EventArgs e) => UpdateClock();

        private void UpdateClock()
        {
            lblThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void ShowModulePending(string moduleName)
        {
            MessageBox.Show($"{moduleName} đang được triển khai trên nhánh tính năng tương ứng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuDanhSachPhong_Click(object? sender, EventArgs e) => OpenChildForm(new FormPhong(_currentUser));
        private void mnuThietBiPhong_Click(object? sender, EventArgs e) => OpenChildForm(new FormThietBiPhong(_currentUser));
        private void mnuKhachThue_Click(object? sender, EventArgs e) => OpenChildForm(new FormKhachThue(_currentUser));
        private void mnuDanhSachHopDong_Click(object? sender, EventArgs e) => ShowModulePending("Phân hệ Hợp đồng thuê nhà (Lê Ngọc Quyền - UC4)");
        private void mnuChiSoDienNuoc_Click(object? sender, EventArgs e) => ShowModulePending("Phân hệ Chỉ số điện nước (Lê Ngọc Quyền - UC5)");
        private void mnuDichVu_Click(object? sender, EventArgs e) => ShowModulePending("Phân hệ Quản lý dịch vụ (Lê Ngọc Quyền - UC5)");
        private void mnuHoaDon_Click(object? sender, EventArgs e) => ShowModulePending("Phân hệ Hóa đơn thanh toán (Nguyễn Văn Tiến - UC6)");
        private void mnuYeuCau_Click(object? sender, EventArgs e) => ShowModulePending("Phân hệ Yêu cầu thông báo (Nguyễn Văn Tiến - UC7)");
        private void mnuBaoCao_Click(object? sender, EventArgs e) => ShowModulePending("Phân hệ Báo cáo thống kê (Nguyễn Văn Tiến - UC8)");

        private void mnuDoiMatKhau_Click(object? sender, EventArgs e)
        {
            using (var dlg = new FormChangePassword(_currentUser))
            {
                dlg.ShowDialog(this);
            }
        }

        private void mnuDangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
