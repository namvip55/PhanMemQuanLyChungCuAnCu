using System;
using System.Drawing;
using System.Windows.Forms;
using AnCuManagement.BLL;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    /// <summary>
    /// Giao diện Đăng nhập hệ thống & Xác thực 3 vai trò (UC1)
    /// Do Nguyễn Thành Nam (MSSV: 24810310230) phụ trách thiết kế & lập trình.
    /// </summary>
    public partial class FormLogin : Form
    {
        private readonly TaiKhoanBLL _taiKhoanBLL = new();

        public FormLogin()
        {
            InitializeComponent();
            try
            {
                _taiKhoanBLL.KiemTraVaChuanHoaFont();
            }
            catch { }
        }

        private void btnDangNhap_Click(object? sender, EventArgs e)
        {
            lblThongBao.Text = string.Empty;
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            try
            {
                var (taiKhoan, message) = _taiKhoanBLL.DangNhap(tenDangNhap, matKhau);

                if (taiKhoan == null)
                {
                    lblThongBao.ForeColor = Color.FromArgb(211, 47, 47);
                    lblThongBao.Text = message;
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                // Đăng nhập thành công -> Mở FormMain và truyền đối tượng TaiKhoanDTO
                this.Hide();
                using (var formMain = new FormMain(taiKhoan))
                {
                    formMain.ShowDialog();
                }

                // Khi đăng xuất khỏi FormMain thì hiện lại FormLogin và xóa mật khẩu
                txtMatKhau.Clear();
                lblThongBao.Text = string.Empty;
                this.Show();
                txtTenDangNhap.Focus();
            }
            catch (Exception ex)
            {
                lblThongBao.ForeColor = Color.FromArgb(211, 47, 47);
                lblThongBao.Text = "Lỗi kết nối CSDL: " + ex.Message;
            }
        }

        private void btnThoat_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
