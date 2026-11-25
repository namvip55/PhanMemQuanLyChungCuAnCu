using System;
using System.Drawing;
using System.Windows.Forms;
using AnCuManagement.BLL;
using AnCuManagement.DTO;

namespace AnCuManagement.GUI.Forms
{
    /// <summary>
    /// Giao diện Đổi mật khẩu tài khoản người dùng (UC1)
    /// Do Nguyễn Thành Nam (MSSV: 24810310230) phụ trách thiết kế & lập trình.
    /// </summary>
    public partial class FormChangePassword : Form
    {
        private readonly TaiKhoanBLL _bll = new();
        private readonly TaiKhoanDTO _currentUser;

        public FormChangePassword(TaiKhoanDTO currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void btnDoiMatKhau_Click(object? sender, EventArgs e)
        {
            try
            {
                lblThongBao.Text = string.Empty;
                string cu = txtMatKhauCu.Text;
                string moi = txtMatKhauMoi.Text;
                string xacNhan = txtXacNhan.Text;

                var (success, message) = _bll.DoiMatKhau(_currentUser.MaTaiKhoan, cu, moi, xacNhan);

                if (success)
                {
                    MessageBox.Show(message, "Thông báo (UC1.4)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblThongBao.ForeColor = Color.FromArgb(211, 47, 47);
                    lblThongBao.Text = message;
                }
            }
            catch (Exception ex)
            {
                lblThongBao.ForeColor = Color.FromArgb(211, 47, 47);
                lblThongBao.Text = "Lỗi đổi mật khẩu: " + ex.Message;
            }
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
