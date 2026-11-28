namespace AnCuManagement.GUI.Forms
{
    partial class FormHopDong
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cboPhong;
        private System.Windows.Forms.Label lblKhachThue;
        private System.Windows.Forms.ComboBox cboKhachThue;
        private System.Windows.Forms.Label lblNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.TextBox txtTienCoc;
        private System.Windows.Forms.Label lblGiaThue;
        private System.Windows.Forms.TextBox txtGiaThue;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.Button btnChamDut;
        private System.Windows.Forms.Button btnYeuCauHopDong;
        private System.Windows.Forms.Button btnInHopDong;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvHopDong;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnInHopDong = new System.Windows.Forms.Button();
            this.btnYeuCauHopDong = new System.Windows.Forms.Button();
            this.btnChamDut = new System.Windows.Forms.Button();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtGiaThue = new System.Windows.Forms.TextBox();
            this.lblGiaThue = new System.Windows.Forms.Label();
            this.txtTienCoc = new System.Windows.Forms.TextBox();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.cboKhachThue = new System.Windows.Forms.ComboBox();
            this.lblKhachThue = new System.Windows.Forms.Label();
            this.cboPhong = new System.Windows.Forms.ComboBox();
            this.lblPhong = new System.Windows.Forms.Label();
            this.dgvHopDong = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHopDong)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(512, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "UC4: QUẢN LÝ HỢP ĐỒNG THUÊ CĂN HỘ & PHÁP LÝ IN ẤN";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.btnLamMoi);
            this.grpThongTin.Controls.Add(this.btnInHopDong);
            this.grpThongTin.Controls.Add(this.btnYeuCauHopDong);
            this.grpThongTin.Controls.Add(this.btnChamDut);
            this.grpThongTin.Controls.Add(this.btnGiaHan);
            this.grpThongTin.Controls.Add(this.btnThemMoi);
            this.grpThongTin.Controls.Add(this.txtGhiChu);
            this.grpThongTin.Controls.Add(this.lblGhiChu);
            this.grpThongTin.Controls.Add(this.cboTrangThai);
            this.grpThongTin.Controls.Add(this.lblTrangThai);
            this.grpThongTin.Controls.Add(this.txtGiaThue);
            this.grpThongTin.Controls.Add(this.lblGiaThue);
            this.grpThongTin.Controls.Add(this.txtTienCoc);
            this.grpThongTin.Controls.Add(this.lblTienCoc);
            this.grpThongTin.Controls.Add(this.dtpNgayKetThuc);
            this.grpThongTin.Controls.Add(this.lblNgayKetThuc);
            this.grpThongTin.Controls.Add(this.dtpNgayBatDau);
            this.grpThongTin.Controls.Add(this.lblNgayBatDau);
            this.grpThongTin.Controls.Add(this.cboKhachThue);
            this.grpThongTin.Controls.Add(this.lblKhachThue);
            this.grpThongTin.Controls.Add(this.cboPhong);
            this.grpThongTin.Controls.Add(this.lblPhong);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpThongTin.Location = new System.Drawing.Point(12, 56);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1176, 185);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin hợp đồng thuê nhà";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Location = new System.Drawing.Point(1055, 140);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.TabIndex = 20;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnInHopDong
            // 
            this.btnInHopDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnInHopDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInHopDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInHopDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInHopDong.ForeColor = System.Drawing.Color.White;
            this.btnInHopDong.Location = new System.Drawing.Point(900, 140);
            this.btnInHopDong.Name = "btnInHopDong";
            this.btnInHopDong.Size = new System.Drawing.Size(145, 32);
            this.btnInHopDong.TabIndex = 19;
            this.btnInHopDong.Text = "🖨 In hợp đồng (PDF)";
            this.btnInHopDong.UseVisualStyleBackColor = false;
            this.btnInHopDong.Click += new System.EventHandler(this.btnInHopDong_Click);
            // 
            // btnYeuCauHopDong
            // 
            this.btnYeuCauHopDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnYeuCauHopDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYeuCauHopDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYeuCauHopDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnYeuCauHopDong.ForeColor = System.Drawing.Color.White;
            this.btnYeuCauHopDong.Location = new System.Drawing.Point(620, 140);
            this.btnYeuCauHopDong.Name = "btnYeuCauHopDong";
            this.btnYeuCauHopDong.Size = new System.Drawing.Size(260, 32);
            this.btnYeuCauHopDong.TabIndex = 21;
            this.btnYeuCauHopDong.Text = "📩 Yêu cầu gia hạn / chấm dứt HĐ";
            this.btnYeuCauHopDong.UseVisualStyleBackColor = false;
            this.btnYeuCauHopDong.Visible = false;
            this.btnYeuCauHopDong.Click += new System.EventHandler(this.btnYeuCauHopDong_Click);
            // 
            // btnChamDut
            // 
            this.btnChamDut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnChamDut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChamDut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChamDut.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChamDut.ForeColor = System.Drawing.Color.White;
            this.btnChamDut.Location = new System.Drawing.Point(735, 140);
            this.btnChamDut.Name = "btnChamDut";
            this.btnChamDut.Size = new System.Drawing.Size(155, 32);
            this.btnChamDut.TabIndex = 18;
            this.btnChamDut.Text = "Thanh lý hợp đồng";
            this.btnChamDut.UseVisualStyleBackColor = false;
            this.btnChamDut.Click += new System.EventHandler(this.btnChamDut_Click);
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnGiaHan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGiaHan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiaHan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGiaHan.ForeColor = System.Drawing.Color.White;
            this.btnGiaHan.Location = new System.Drawing.Point(600, 140);
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Size = new System.Drawing.Size(125, 32);
            this.btnGiaHan.TabIndex = 17;
            this.btnGiaHan.Text = "Gia hạn HĐ";
            this.btnGiaHan.UseVisualStyleBackColor = false;
            this.btnGiaHan.Click += new System.EventHandler(this.btnGiaHan_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnThemMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnThemMoi.ForeColor = System.Drawing.Color.White;
            this.btnThemMoi.Location = new System.Drawing.Point(445, 140);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(145, 32);
            this.btnThemMoi.TabIndex = 16;
            this.btnThemMoi.Text = "+ Lập hợp đồng mới";
            this.btnThemMoi.UseVisualStyleBackColor = false;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(115, 105);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(950, 24);
            this.txtGhiChu.TabIndex = 15;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(15, 108);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(54, 17);
            this.lblGhiChu.TabIndex = 14;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "HieuLuc",
            "HetHan",
            "DaThanhLy"});
            this.cboTrangThai.Location = new System.Drawing.Point(930, 68);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(135, 25);
            this.cboTrangThai.TabIndex = 13;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(855, 71);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(69, 17);
            this.lblTrangThai.TabIndex = 12;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtGiaThue
            // 
            this.txtGiaThue.Location = new System.Drawing.Point(685, 68);
            this.txtGiaThue.Name = "txtGiaThue";
            this.txtGiaThue.Size = new System.Drawing.Size(150, 24);
            this.txtGiaThue.TabIndex = 11;
            // 
            // lblGiaThue
            // 
            this.lblGiaThue.AutoSize = true;
            this.lblGiaThue.Location = new System.Drawing.Point(585, 71);
            this.lblGiaThue.Name = "lblGiaThue";
            this.lblGiaThue.Size = new System.Drawing.Size(95, 17);
            this.lblGiaThue.TabIndex = 10;
            this.lblGiaThue.Text = "Giá thuê thực tế:";
            // 
            // txtTienCoc
            // 
            this.txtTienCoc.Location = new System.Drawing.Point(405, 68);
            this.txtTienCoc.Name = "txtTienCoc";
            this.txtTienCoc.Size = new System.Drawing.Size(155, 24);
            this.txtTienCoc.TabIndex = 9;
            // 
            // lblTienCoc
            // 
            this.lblTienCoc.AutoSize = true;
            this.lblTienCoc.Location = new System.Drawing.Point(325, 71);
            this.lblTienCoc.Name = "lblTienCoc";
            this.lblTienCoc.Size = new System.Drawing.Size(60, 17);
            this.lblTienCoc.TabIndex = 8;
            this.lblTienCoc.Text = "Tiền cọc:";
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(115, 68);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(180, 24);
            this.dtpNgayKetThuc.TabIndex = 7;
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Location = new System.Drawing.Point(15, 71);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(90, 17);
            this.lblNgayKetThuc.TabIndex = 6;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(685, 28);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(150, 24);
            this.dtpNgayBatDau.TabIndex = 5;
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Location = new System.Drawing.Point(585, 31);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(89, 17);
            this.lblNgayBatDau.TabIndex = 4;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // cboKhachThue
            // 
            this.cboKhachThue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachThue.FormattingEnabled = true;
            this.cboKhachThue.Location = new System.Drawing.Point(380, 28);
            this.cboKhachThue.Name = "cboKhachThue";
            this.cboKhachThue.Size = new System.Drawing.Size(180, 25);
            this.cboKhachThue.TabIndex = 3;
            // 
            // lblKhachThue
            // 
            this.lblKhachThue.AutoSize = true;
            this.lblKhachThue.Location = new System.Drawing.Point(265, 31);
            this.lblKhachThue.Name = "lblKhachThue";
            this.lblKhachThue.Size = new System.Drawing.Size(109, 17);
            this.lblKhachThue.TabIndex = 2;
            this.lblKhachThue.Text = "Khách thuê đứng:";
            // 
            // cboPhong
            // 
            this.cboPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhong.FormattingEnabled = true;
            this.cboPhong.Location = new System.Drawing.Point(115, 28);
            this.cboPhong.Name = "cboPhong";
            this.cboPhong.Size = new System.Drawing.Size(130, 25);
            this.cboPhong.TabIndex = 1;
            this.cboPhong.SelectedIndexChanged += new System.EventHandler(this.cboPhong_SelectedIndexChanged);
            // 
            // lblPhong
            // 
            this.lblPhong.AutoSize = true;
            this.lblPhong.Location = new System.Drawing.Point(15, 31);
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Size = new System.Drawing.Size(82, 17);
            this.lblPhong.TabIndex = 0;
            this.lblPhong.Text = "Chọn phòng:";
            // 
            // dgvHopDong
            // 
            this.dgvHopDong.AllowUserToAddRows = false;
            this.dgvHopDong.AllowUserToDeleteRows = false;
            this.dgvHopDong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHopDong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHopDong.BackgroundColor = System.Drawing.Color.White;
            this.dgvHopDong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHopDong.Location = new System.Drawing.Point(12, 247);
            this.dgvHopDong.MultiSelect = false;
            this.dgvHopDong.Name = "dgvHopDong";
            this.dgvHopDong.ReadOnly = true;
            this.dgvHopDong.RowHeadersVisible = false;
            this.dgvHopDong.RowHeadersWidth = 51;
            this.dgvHopDong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHopDong.Size = new System.Drawing.Size(1176, 350);
            this.dgvHopDong.TabIndex = 2;
            this.dgvHopDong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHopDong_CellClick);
            // 
            // FormHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 610);
            this.Controls.Add(this.dgvHopDong);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlTop);
            this.Name = "FormHopDong";
            this.Text = "Hợp đồng thuê";
            this.Load += new System.EventHandler(this.FormHopDong_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHopDong)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
