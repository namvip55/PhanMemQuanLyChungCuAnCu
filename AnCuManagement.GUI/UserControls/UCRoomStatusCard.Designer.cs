namespace AnCuManagement.GUI.UserControls
{
    partial class UCRoomStatusCard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblGiaThue;
        private System.Windows.Forms.Label lblThongTinPhu;

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
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblGiaThue = new System.Windows.Forms.Label();
            this.lblThongTinPhu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSoPhong
            // 
            this.lblSoPhong.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoPhong.ForeColor = System.Drawing.Color.White;
            this.lblSoPhong.Location = new System.Drawing.Point(10, 8);
            this.lblSoPhong.Name = "lblSoPhong";
            this.lblSoPhong.Size = new System.Drawing.Size(160, 32);
            this.lblSoPhong.TabIndex = 0;
            this.lblSoPhong.Text = "A101";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTrangThai.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTrangThai.Location = new System.Drawing.Point(10, 42);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(160, 20);
            this.lblTrangThai.TabIndex = 1;
            this.lblTrangThai.Text = "Đang thuê";
            // 
            // lblGiaThue
            // 
            this.lblGiaThue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblGiaThue.ForeColor = System.Drawing.Color.White;
            this.lblGiaThue.Location = new System.Drawing.Point(10, 64);
            this.lblGiaThue.Name = "lblGiaThue";
            this.lblGiaThue.Size = new System.Drawing.Size(160, 22);
            this.lblGiaThue.TabIndex = 2;
            this.lblGiaThue.Text = "5.500.000 đ/tháng";
            // 
            // lblThongTinPhu
            // 
            this.lblThongTinPhu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblThongTinPhu.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblThongTinPhu.Location = new System.Drawing.Point(10, 88);
            this.lblThongTinPhu.Name = "lblThongTinPhu";
            this.lblThongTinPhu.Size = new System.Drawing.Size(160, 20);
            this.lblThongTinPhu.TabIndex = 3;
            this.lblThongTinPhu.Text = "Tầng 1 - Căn Đơn";
            // 
            // UCRoomStatusCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.lblThongTinPhu);
            this.Controls.Add(this.lblGiaThue);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.lblSoPhong);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "UCRoomStatusCard";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(180, 115);
            this.ResumeLayout(false);
        }
    }
}
