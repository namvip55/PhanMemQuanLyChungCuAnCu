namespace AnCuManagement.GUI.Forms
{
    partial class FormPhong
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.TextBox txtSoPhong;
        private System.Windows.Forms.Label lblTang;
        private System.Windows.Forms.NumericUpDown nudTang;
        private System.Windows.Forms.Label lblDienTich;
        private System.Windows.Forms.NumericUpDown nudDienTich;
        private System.Windows.Forms.Label lblGiaThue;
        private System.Windows.Forms.TextBox txtGiaThue;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblLoaiPhong;
        private System.Windows.Forms.ComboBox cboLoaiPhong;
        private System.Windows.Forms.Label lblToaNha;
        private System.Windows.Forms.ComboBox cboToaNha;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnDoiTrangThai;
        private System.Windows.Forms.Button btnCapNhatGia;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.Label lblLocTang;
        private System.Windows.Forms.ComboBox cboLocTang;
        private System.Windows.Forms.Label lblLocTrangThai;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.Label lblGiaMin;
        private System.Windows.Forms.TextBox txtGiaMin;
        private System.Windows.Forms.Label lblGiaMax;
        private System.Windows.Forms.TextBox txtGiaMax;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTatCa;
        private System.Windows.Forms.TabControl tabView;
        private System.Windows.Forms.TabPage tabTableView;
        private System.Windows.Forms.TabPage tabCardView;
        private System.Windows.Forms.FlowLayoutPanel pnlRoomCards;
        private System.Windows.Forms.DataGridView dgvPhong;

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
            this.btnCapNhatGia = new System.Windows.Forms.Button();
            this.btnDoiTrangThai = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.cboToaNha = new System.Windows.Forms.ComboBox();
            this.lblToaNha = new System.Windows.Forms.Label();
            this.cboLoaiPhong = new System.Windows.Forms.ComboBox();
            this.lblLoaiPhong = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtGiaThue = new System.Windows.Forms.TextBox();
            this.lblGiaThue = new System.Windows.Forms.Label();
            this.nudDienTich = new System.Windows.Forms.NumericUpDown();
            this.lblDienTich = new System.Windows.Forms.Label();
            this.nudTang = new System.Windows.Forms.NumericUpDown();
            this.lblTang = new System.Windows.Forms.Label();
            this.txtSoPhong = new System.Windows.Forms.TextBox();
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.lblGiaMin = new System.Windows.Forms.Label();
            this.txtGiaMin = new System.Windows.Forms.TextBox();
            this.lblGiaMax = new System.Windows.Forms.Label();
            this.txtGiaMax = new System.Windows.Forms.TextBox();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.lblLocTrangThai = new System.Windows.Forms.Label();
            this.cboLocTang = new System.Windows.Forms.ComboBox();
            this.lblLocTang = new System.Windows.Forms.Label();
            this.tabView = new System.Windows.Forms.TabControl();
            this.tabTableView = new System.Windows.Forms.TabPage();
            this.tabCardView = new System.Windows.Forms.TabPage();
            this.pnlRoomCards = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDienTich)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTang)).BeginInit();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
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
            this.lblTitle.Size = new System.Drawing.Size(434, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "UC2: QUẢN LÝ PHÒNG & TRẠNG THÁI CHO THUÊ";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.btnLamMoi);
            this.grpThongTin.Controls.Add(this.btnCapNhatGia);
            this.grpThongTin.Controls.Add(this.btnDoiTrangThai);
            this.grpThongTin.Controls.Add(this.btnSua);
            this.grpThongTin.Controls.Add(this.btnThem);
            this.grpThongTin.Controls.Add(this.txtMoTa);
            this.grpThongTin.Controls.Add(this.lblMoTa);
            this.grpThongTin.Controls.Add(this.cboToaNha);
            this.grpThongTin.Controls.Add(this.lblToaNha);
            this.grpThongTin.Controls.Add(this.cboLoaiPhong);
            this.grpThongTin.Controls.Add(this.lblLoaiPhong);
            this.grpThongTin.Controls.Add(this.cboTrangThai);
            this.grpThongTin.Controls.Add(this.lblTrangThai);
            this.grpThongTin.Controls.Add(this.txtGiaThue);
            this.grpThongTin.Controls.Add(this.lblGiaThue);
            this.grpThongTin.Controls.Add(this.nudDienTich);
            this.grpThongTin.Controls.Add(this.lblDienTich);
            this.grpThongTin.Controls.Add(this.nudTang);
            this.grpThongTin.Controls.Add(this.lblTang);
            this.grpThongTin.Controls.Add(this.txtSoPhong);
            this.grpThongTin.Controls.Add(this.lblSoPhong);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpThongTin.Location = new System.Drawing.Point(12, 56);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1176, 175);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chi tiết căn hộ / phòng";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Location = new System.Drawing.Point(1060, 125);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(95, 32);
            this.btnLamMoi.TabIndex = 20;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnCapNhatGia
            // 
            this.btnCapNhatGia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnCapNhatGia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapNhatGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhatGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCapNhatGia.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatGia.Location = new System.Drawing.Point(935, 125);
            this.btnCapNhatGia.Name = "btnCapNhatGia";
            this.btnCapNhatGia.Size = new System.Drawing.Size(115, 32);
            this.btnCapNhatGia.TabIndex = 19;
            this.btnCapNhatGia.Text = "💰 Cập nhật giá";
            this.btnCapNhatGia.UseVisualStyleBackColor = false;
            this.btnCapNhatGia.Click += new System.EventHandler(this.btnCapNhatGia_Click);
            // 
            // btnDoiTrangThai
            // 
            this.btnDoiTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.btnDoiTrangThai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoiTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDoiTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnDoiTrangThai.Location = new System.Drawing.Point(810, 125);
            this.btnDoiTrangThai.Name = "btnDoiTrangThai";
            this.btnDoiTrangThai.Size = new System.Drawing.Size(115, 32);
            this.btnDoiTrangThai.TabIndex = 18;
            this.btnDoiTrangThai.Text = "⚡ Đổi trạng thái";
            this.btnDoiTrangThai.UseVisualStyleBackColor = false;
            this.btnDoiTrangThai.Click += new System.EventHandler(this.btnDoiTrangThai_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(700, 125);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.TabIndex = 17;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(590, 125);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.TabIndex = 16;
            this.btnThem.Text = "+ Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(90, 125);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(480, 24);
            this.txtMoTa.TabIndex = 15;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(15, 128);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(46, 17);
            this.lblMoTa.TabIndex = 14;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // cboToaNha
            // 
            this.cboToaNha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboToaNha.FormattingEnabled = true;
            this.cboToaNha.Location = new System.Drawing.Point(880, 75);
            this.cboToaNha.Name = "cboToaNha";
            this.cboToaNha.Size = new System.Drawing.Size(180, 25);
            this.cboToaNha.TabIndex = 13;
            // 
            // lblToaNha
            // 
            this.lblToaNha.AutoSize = true;
            this.lblToaNha.Location = new System.Drawing.Point(795, 78);
            this.lblToaNha.Name = "lblToaNha";
            this.lblToaNha.Size = new System.Drawing.Size(59, 17);
            this.lblToaNha.TabIndex = 12;
            this.lblToaNha.Text = "Tòa nhà:";
            // 
            // cboLoaiPhong
            // 
            this.cboLoaiPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiPhong.FormattingEnabled = true;
            this.cboLoaiPhong.Items.AddRange(new object[] {
            "Phòng Đơn",
            "Phòng Đôi",
            "Studio",
            "Phòng VIP"});
            this.cboLoaiPhong.Location = new System.Drawing.Point(620, 75);
            this.cboLoaiPhong.Name = "cboLoaiPhong";
            this.cboLoaiPhong.Size = new System.Drawing.Size(150, 25);
            this.cboLoaiPhong.TabIndex = 11;
            // 
            // lblLoaiPhong
            // 
            this.lblLoaiPhong.AutoSize = true;
            this.lblLoaiPhong.Location = new System.Drawing.Point(535, 78);
            this.lblLoaiPhong.Name = "lblLoaiPhong";
            this.lblLoaiPhong.Size = new System.Drawing.Size(76, 17);
            this.lblLoaiPhong.TabIndex = 10;
            this.lblLoaiPhong.Text = "Loại phòng:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Trống",
            "ĐangThuê",
            "SửaChữa",
            "ĐặtCọc"});
            this.cboTrangThai.Location = new System.Drawing.Point(365, 75);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(150, 25);
            this.cboTrangThai.TabIndex = 9;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(285, 78);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(69, 17);
            this.lblTrangThai.TabIndex = 8;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtGiaThue
            // 
            this.txtGiaThue.Location = new System.Drawing.Point(90, 75);
            this.txtGiaThue.Name = "txtGiaThue";
            this.txtGiaThue.Size = new System.Drawing.Size(170, 24);
            this.txtGiaThue.TabIndex = 7;
            // 
            // lblGiaThue
            // 
            this.lblGiaThue.AutoSize = true;
            this.lblGiaThue.Location = new System.Drawing.Point(15, 78);
            this.lblGiaThue.Name = "lblGiaThue";
            this.lblGiaThue.Size = new System.Drawing.Size(60, 17);
            this.lblGiaThue.TabIndex = 6;
            this.lblGiaThue.Text = "Giá thuê:";
            // 
            // nudDienTich
            // 
            this.nudDienTich.DecimalPlaces = 1;
            this.nudDienTich.Location = new System.Drawing.Point(535, 30);
            this.nudDienTich.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudDienTich.Name = "nudDienTich";
            this.nudDienTich.Size = new System.Drawing.Size(100, 24);
            this.nudDienTich.TabIndex = 5;
            this.nudDienTich.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // lblDienTich
            // 
            this.lblDienTich.AutoSize = true;
            this.lblDienTich.Location = new System.Drawing.Point(440, 33);
            this.lblDienTich.Name = "lblDienTich";
            this.lblDienTich.Size = new System.Drawing.Size(89, 17);
            this.lblDienTich.TabIndex = 4;
            this.lblDienTich.Text = "Diện tích (m²):";
            // 
            // nudTang
            // 
            this.nudTang.Location = new System.Drawing.Point(335, 30);
            this.nudTang.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudTang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTang.Name = "nudTang";
            this.nudTang.Size = new System.Drawing.Size(80, 24);
            this.nudTang.TabIndex = 3;
            this.nudTang.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTang
            // 
            this.lblTang.AutoSize = true;
            this.lblTang.Location = new System.Drawing.Point(285, 33);
            this.lblTang.Name = "lblTang";
            this.lblTang.Size = new System.Drawing.Size(40, 17);
            this.lblTang.TabIndex = 2;
            this.lblTang.Text = "Tầng:";
            // 
            // txtSoPhong
            // 
            this.txtSoPhong.Location = new System.Drawing.Point(90, 30);
            this.txtSoPhong.Name = "txtSoPhong";
            this.txtSoPhong.Size = new System.Drawing.Size(170, 24);
            this.txtSoPhong.TabIndex = 1;
            // 
            // lblSoPhong
            // 
            this.lblSoPhong.AutoSize = true;
            this.lblSoPhong.Location = new System.Drawing.Point(15, 33);
            this.lblSoPhong.Name = "lblSoPhong";
            this.lblSoPhong.Size = new System.Drawing.Size(68, 17);
            this.lblSoPhong.TabIndex = 0;
            this.lblSoPhong.Text = "Số phòng:";
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTimKiem.Controls.Add(this.btnTatCa);
            this.grpTimKiem.Controls.Add(this.btnTimKiem);
            this.grpTimKiem.Controls.Add(this.txtGiaMax);
            this.grpTimKiem.Controls.Add(this.lblGiaMax);
            this.grpTimKiem.Controls.Add(this.txtGiaMin);
            this.grpTimKiem.Controls.Add(this.lblGiaMin);
            this.grpTimKiem.Controls.Add(this.cboLocTrangThai);
            this.grpTimKiem.Controls.Add(this.lblLocTrangThai);
            this.grpTimKiem.Controls.Add(this.cboLocTang);
            this.grpTimKiem.Controls.Add(this.lblLocTang);
            this.grpTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpTimKiem.Location = new System.Drawing.Point(12, 237);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(1176, 65);
            this.grpTimKiem.TabIndex = 2;
            this.grpTimKiem.TabStop = false;
            this.grpTimKiem.Text = "Tra cứu & Lọc danh sách phòng (UC2.4)";
            // 
            // btnTatCa
            // 
            this.btnTatCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnTatCa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTatCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTatCa.Location = new System.Drawing.Point(860, 24);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(110, 28);
            this.btnTatCa.TabIndex = 9;
            this.btnTatCa.Text = "Tất cả";
            this.btnTatCa.UseVisualStyleBackColor = false;
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(750, 24);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 28);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Lọc dữ liệu";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtGiaMax
            // 
            this.txtGiaMax.Location = new System.Drawing.Point(630, 25);
            this.txtGiaMax.Name = "txtGiaMax";
            this.txtGiaMax.Size = new System.Drawing.Size(100, 24);
            this.txtGiaMax.TabIndex = 7;
            // 
            // lblGiaMax
            // 
            this.lblGiaMax.AutoSize = true;
            this.lblGiaMax.Location = new System.Drawing.Point(590, 29);
            this.lblGiaMax.Name = "lblGiaMax";
            this.lblGiaMax.Size = new System.Drawing.Size(34, 17);
            this.lblGiaMax.TabIndex = 6;
            this.lblGiaMax.Text = "đến:";
            // 
            // txtGiaMin
            // 
            this.txtGiaMin.Location = new System.Drawing.Point(480, 25);
            this.txtGiaMin.Name = "txtGiaMin";
            this.txtGiaMin.Size = new System.Drawing.Size(100, 24);
            this.txtGiaMin.TabIndex = 5;
            // 
            // lblGiaMin
            // 
            this.lblGiaMin.AutoSize = true;
            this.lblGiaMin.Location = new System.Drawing.Point(425, 29);
            this.lblGiaMin.Name = "lblGiaMin";
            this.lblGiaMin.Size = new System.Drawing.Size(49, 17);
            this.lblGiaMin.TabIndex = 4;
            this.lblGiaMin.Text = "Giá từ:";
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.FormattingEnabled = true;
            this.cboLocTrangThai.Items.AddRange(new object[] {
            "-- Tất cả trạng thái --",
            "Trống",
            "ĐangThuê",
            "SửaChữa",
            "ĐặtCọc"});
            this.cboLocTrangThai.Location = new System.Drawing.Point(265, 25);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(145, 25);
            this.cboLocTrangThai.TabIndex = 3;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Location = new System.Drawing.Point(195, 29);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(69, 17);
            this.lblLocTrangThai.TabIndex = 2;
            this.lblLocTrangThai.Text = "Trạng thái:";
            // 
            // cboLocTang
            // 
            this.cboLocTang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTang.FormattingEnabled = true;
            this.cboLocTang.Items.AddRange(new object[] {
            "-- Tất cả các tầng --",
            "Tầng 1",
            "Tầng 2",
            "Tầng 3",
            "Tầng 4",
            "Tầng 5"});
            this.cboLocTang.Location = new System.Drawing.Point(55, 25);
            this.cboLocTang.Name = "cboLocTang";
            this.cboLocTang.Size = new System.Drawing.Size(130, 25);
            this.cboLocTang.TabIndex = 1;
            // 
            // lblLocTang
            // 
            this.lblLocTang.AutoSize = true;
            this.lblLocTang.Location = new System.Drawing.Point(12, 29);
            this.lblLocTang.Name = "lblLocTang";
            this.lblLocTang.Size = new System.Drawing.Size(40, 17);
            this.lblLocTang.TabIndex = 0;
            this.lblLocTang.Text = "Tầng:";
            // 
            // tabView
            // 
            this.tabView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabView.Controls.Add(this.tabTableView);
            this.tabView.Controls.Add(this.tabCardView);
            this.tabView.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabView.Location = new System.Drawing.Point(12, 310);
            this.tabView.Name = "tabView";
            this.tabView.SelectedIndex = 0;
            this.tabView.Size = new System.Drawing.Size(1176, 290);
            this.tabView.TabIndex = 3;
            // 
            // tabTableView
            // 
            this.tabTableView.Controls.Add(this.dgvPhong);
            this.tabTableView.Location = new System.Drawing.Point(4, 25);
            this.tabTableView.Name = "tabTableView";
            this.tabTableView.Padding = new System.Windows.Forms.Padding(3);
            this.tabTableView.Size = new System.Drawing.Size(1168, 261);
            this.tabTableView.TabIndex = 0;
            this.tabTableView.Text = "📋 Danh sách dạng bảng";
            this.tabTableView.UseVisualStyleBackColor = true;
            // 
            // tabCardView
            // 
            this.tabCardView.Controls.Add(this.pnlRoomCards);
            this.tabCardView.Location = new System.Drawing.Point(4, 25);
            this.tabCardView.Name = "tabCardView";
            this.tabCardView.Padding = new System.Windows.Forms.Padding(5);
            this.tabCardView.Size = new System.Drawing.Size(1168, 261);
            this.tabCardView.TabIndex = 1;
            this.tabCardView.Text = "🏢 Sơ đồ thẻ trực quan (Card Grid)";
            this.tabCardView.UseVisualStyleBackColor = true;
            // 
            // pnlRoomCards
            // 
            this.pnlRoomCards.AutoScroll = true;
            this.pnlRoomCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlRoomCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRoomCards.Location = new System.Drawing.Point(5, 5);
            this.pnlRoomCards.Name = "pnlRoomCards";
            this.pnlRoomCards.Size = new System.Drawing.Size(1158, 251);
            this.pnlRoomCards.TabIndex = 0;
            // 
            // dgvPhong
            // 
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AllowUserToDeleteRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhong.Location = new System.Drawing.Point(3, 3);
            this.dgvPhong.MultiSelect = false;
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.RowHeadersVisible = false;
            this.dgvPhong.RowHeadersWidth = 51;
            this.dgvPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.Size = new System.Drawing.Size(1162, 255);
            this.dgvPhong.TabIndex = 0;
            this.dgvPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhong_CellClick);
            this.dgvPhong.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvPhong_RowPrePaint);
            // 
            // FormPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 610);
            this.Controls.Add(this.tabView);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlTop);
            this.Name = "FormPhong";
            this.Text = "Quản lý phòng";
            this.Load += new System.EventHandler(this.FormPhong_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDienTich)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTang)).EndInit();
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            this.tabView.ResumeLayout(false);
            this.tabTableView.ResumeLayout(false);
            this.tabCardView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
