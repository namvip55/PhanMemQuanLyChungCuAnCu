namespace AnCuManagement.GUI.Forms
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiMatKhau;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyPhong;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachPhong;
        private System.Windows.Forms.ToolStripMenuItem mnuThietBiPhong;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyKhach;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachThue;
        private System.Windows.Forms.ToolStripMenuItem mnuHopDong;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachHopDong;
        private System.Windows.Forms.ToolStripMenuItem mnuDienNuocDichVu;
        private System.Windows.Forms.ToolStripMenuItem mnuChiSoDienNuoc;
        private System.Windows.Forms.ToolStripMenuItem mnuDichVu;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDonThanhToan;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuYeuCauThongBao;
        private System.Windows.Forms.ToolStripMenuItem mnuYeuCau;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoThongKe;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblNguoiDung;
        private System.Windows.Forms.ToolStripStatusLabel lblVaiTro;
        private System.Windows.Forms.ToolStripStatusLabel lblThoiGian;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Timer timerClock;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyPhong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachPhong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThietBiPhong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyKhach = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachThue = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHopDong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachHopDong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDienNuocDichVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChiSoDienNuoc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDichVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDonThanhToan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuYeuCauThongBao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuYeuCau = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoThongKe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblNguoiDung = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVaiTro = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblThoiGian = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuQuanLyPhong,
            this.mnuQuanLyKhach,
            this.mnuHopDong,
            this.mnuDienNuocDichVu,
            this.mnuHoaDonThanhToan,
            this.mnuYeuCauThongBao,
            this.mnuBaoCaoThongKe});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoiMatKhau,
            this.toolStripSeparator1,
            this.mnuDangXuat});
            this.mnuHeThong.ForeColor = System.Drawing.Color.White;
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(79, 29);
            this.mnuHeThong.Text = "Hệ thống";
            // 
            // mnuDoiMatKhau
            // 
            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Size = new System.Drawing.Size(161, 24);
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            this.mnuDoiMatKhau.Click += new System.EventHandler(this.mnuDoiMatKhau_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(161, 24);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // mnuQuanLyPhong
            // 
            this.mnuQuanLyPhong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDanhSachPhong,
            this.mnuThietBiPhong});
            this.mnuQuanLyPhong.ForeColor = System.Drawing.Color.White;
            this.mnuQuanLyPhong.Name = "mnuQuanLyPhong";
            this.mnuQuanLyPhong.Size = new System.Drawing.Size(115, 29);
            this.mnuQuanLyPhong.Text = "Quản lý phòng";
            // 
            // mnuDanhSachPhong
            // 
            this.mnuDanhSachPhong.Name = "mnuDanhSachPhong";
            this.mnuDanhSachPhong.Size = new System.Drawing.Size(193, 24);
            this.mnuDanhSachPhong.Text = "Danh mục phòng";
            this.mnuDanhSachPhong.Click += new System.EventHandler(this.mnuDanhSachPhong_Click);
            // 
            // mnuThietBiPhong
            // 
            this.mnuThietBiPhong.Name = "mnuThietBiPhong";
            this.mnuThietBiPhong.Size = new System.Drawing.Size(193, 24);
            this.mnuThietBiPhong.Text = "Thiết bị & Tiện nghi";
            this.mnuThietBiPhong.Click += new System.EventHandler(this.mnuThietBiPhong_Click);
            // 
            // mnuQuanLyKhach
            // 
            this.mnuQuanLyKhach.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuKhachThue});
            this.mnuQuanLyKhach.ForeColor = System.Drawing.Color.White;
            this.mnuQuanLyKhach.Name = "mnuQuanLyKhach";
            this.mnuQuanLyKhach.Size = new System.Drawing.Size(142, 29);
            this.mnuQuanLyKhach.Text = "Quản lý khách thuê";
            // 
            // mnuKhachThue
            // 
            this.mnuKhachThue.Name = "mnuKhachThue";
            this.mnuKhachThue.Size = new System.Drawing.Size(188, 24);
            this.mnuKhachThue.Text = "Hồ sơ cư dân";
            this.mnuKhachThue.Click += new System.EventHandler(this.mnuKhachThue_Click);
            // 
            // mnuHopDong
            // 
            this.mnuHopDong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDanhSachHopDong});
            this.mnuHopDong.ForeColor = System.Drawing.Color.White;
            this.mnuHopDong.Name = "mnuHopDong";
            this.mnuHopDong.Size = new System.Drawing.Size(83, 29);
            this.mnuHopDong.Text = "Hợp đồng";
            // 
            // mnuDanhSachHopDong
            // 
            this.mnuDanhSachHopDong.Name = "mnuDanhSachHopDong";
            this.mnuDanhSachHopDong.Size = new System.Drawing.Size(206, 24);
            this.mnuDanhSachHopDong.Text = "Hợp đồng thuê nhà";
            this.mnuDanhSachHopDong.Click += new System.EventHandler(this.mnuDanhSachHopDong_Click);
            // 
            // mnuDienNuocDichVu
            // 
            this.mnuDienNuocDichVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChiSoDienNuoc,
            this.mnuDichVu});
            this.mnuDienNuocDichVu.ForeColor = System.Drawing.Color.White;
            this.mnuDienNuocDichVu.Name = "mnuDienNuocDichVu";
            this.mnuDienNuocDichVu.Size = new System.Drawing.Size(140, 29);
            this.mnuDienNuocDichVu.Text = "Điện nước & Dịch vụ";
            // 
            // mnuChiSoDienNuoc
            // 
            this.mnuChiSoDienNuoc.Name = "mnuChiSoDienNuoc";
            this.mnuChiSoDienNuoc.Size = new System.Drawing.Size(197, 24);
            this.mnuChiSoDienNuoc.Text = "Chỉ số điện nước";
            this.mnuChiSoDienNuoc.Click += new System.EventHandler(this.mnuChiSoDienNuoc_Click);
            // 
            // mnuDichVu
            // 
            this.mnuDichVu.Name = "mnuDichVu";
            this.mnuDichVu.Size = new System.Drawing.Size(197, 24);
            this.mnuDichVu.Text = "Bảng giá dịch vụ";
            this.mnuDichVu.Click += new System.EventHandler(this.mnuDichVu_Click);
            // 
            // mnuHoaDonThanhToan
            // 
            this.mnuHoaDonThanhToan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHoaDon});
            this.mnuHoaDonThanhToan.ForeColor = System.Drawing.Color.White;
            this.mnuHoaDonThanhToan.Name = "mnuHoaDonThanhToan";
            this.mnuHoaDonThanhToan.Size = new System.Drawing.Size(155, 29);
            this.mnuHoaDonThanhToan.Text = "Hóa đơn & Thanh toán";
            // 
            // mnuHoaDon
            // 
            this.mnuHoaDon.Name = "mnuHoaDon";
            this.mnuHoaDon.Size = new System.Drawing.Size(206, 24);
            this.mnuHoaDon.Text = "Hóa đơn & Thu tiền";
            this.mnuHoaDon.Click += new System.EventHandler(this.mnuHoaDon_Click);
            // 
            // mnuYeuCauThongBao
            // 
            this.mnuYeuCauThongBao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuYeuCau});
            this.mnuYeuCauThongBao.ForeColor = System.Drawing.Color.White;
            this.mnuYeuCauThongBao.Name = "mnuYeuCauThongBao";
            this.mnuYeuCauThongBao.Size = new System.Drawing.Size(144, 29);
            this.mnuYeuCauThongBao.Text = "Yêu cầu & Thông báo";
            // 
            // mnuYeuCau
            // 
            this.mnuYeuCau.Name = "mnuYeuCau";
            this.mnuYeuCau.Size = new System.Drawing.Size(217, 24);
            this.mnuYeuCau.Text = "Báo hỏng & Thông báo";
            this.mnuYeuCau.Click += new System.EventHandler(this.mnuYeuCau_Click);
            // 
            // mnuBaoCaoThongKe
            // 
            this.mnuBaoCaoThongKe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBaoCao});
            this.mnuBaoCaoThongKe.ForeColor = System.Drawing.Color.White;
            this.mnuBaoCaoThongKe.Name = "mnuBaoCaoThongKe";
            this.mnuBaoCaoThongKe.Size = new System.Drawing.Size(132, 29);
            this.mnuBaoCaoThongKe.Text = "Báo cáo thống kê";
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(203, 24);
            this.mnuBaoCao.Text = "Dashboard & Báo cáo";
            this.mnuBaoCao.Click += new System.EventHandler(this.mnuBaoCao_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblNguoiDung,
            this.lblVaiTro,
            this.lblThoiGian});
            this.statusStrip1.Location = new System.Drawing.Point(0, 703);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblNguoiDung
            // 
            this.lblNguoiDung.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNguoiDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.lblNguoiDung.Name = "lblNguoiDung";
            this.lblNguoiDung.Size = new System.Drawing.Size(160, 21);
            this.lblNguoiDung.Text = "Người dùng: Admin";
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(140, 21);
            this.lblVaiTro.Text = "Vai trò: Chủ chung cư";
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(949, 21);
            this.lblThoiGian.Spring = true;
            this.lblThoiGian.Text = "Thời gian: 18/11/2026 23:30:00";
            this.lblThoiGian.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 33);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1264, 670);
            this.pnlContent.TabIndex = 2;
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 729);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Chung cư An Cư - BTL Lập trình .NET (Nhóm 2 - D19CNPM3)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
