using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AnCuManagement.Common;

namespace AnCuManagement.GUI.UserControls
{
    /// <summary>
    /// Thẻ hiển thị trực quan trạng thái phòng theo màu sắc (UC2)
    /// </summary>
    public partial class UCRoomStatusCard : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaPhong { get; set; }

        public UCRoomStatusCard()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SoPhong
        {
            get => lblSoPhong.Text;
            set => lblSoPhong.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TrangThai
        {
            get => lblTrangThai.Text;
            set
            {
                lblTrangThai.Text = value;
                UpdateCardColor(value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string GiaThue
        {
            get => lblGiaThue.Text;
            set => lblGiaThue.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ThongTinPhu
        {
            get => lblThongTinPhu.Text;
            set => lblThongTinPhu.Text = value;
        }

        private void UpdateCardColor(string trangThai)
        {
            if (AppConstants.IsTrong(trangThai))
            {
                // Màu xanh lá: Phòng trống
                BackColor = Color.FromArgb(46, 125, 50);
            }
            else if (AppConstants.IsDangThue(trangThai))
            {
                // Màu xanh dương: Đang thuê
                BackColor = Color.FromArgb(25, 118, 210);
            }
            else if (AppConstants.IsSuaChua(trangThai))
            {
                // Màu cam/đỏ: Đang sửa chữa
                BackColor = Color.FromArgb(211, 47, 47);
            }
            else if (AppConstants.IsDatCoc(trangThai))
            {
                // Màu vàng cam: Đã đặt cọc
                BackColor = Color.FromArgb(245, 124, 0);
            }
            else
            {
                BackColor = Color.FromArgb(108, 117, 125);
            }
        }
    }
}
