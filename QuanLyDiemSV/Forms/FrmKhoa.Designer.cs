namespace QuanLyDiemSV.Forms
{
    partial class FrmKhoa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnThoat = new Button();
            btnHuyBo = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnLuu = new Button();
            btnThem = new Button();
            dgvKhoa = new DataGridView();
            MaKhoa = new DataGridViewTextBoxColumn();
            TenKhoa = new DataGridViewTextBoxColumn();
            NgayThanhLap = new DataGridViewTextBoxColumn();
            TruongKhoa = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            txtTenKhoa = new TextBox();
            txtMaKhoa = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            dtpNgayLapKhoa = new DateTimePicker();
            txtTruongKhoa = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).BeginInit();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnThoat
            // 
            btnThoat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThoat.Location = new Point(841, 112);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 3;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // btnHuyBo
            // 
            btnHuyBo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuyBo.Location = new Point(723, 112);
            btnHuyBo.Name = "btnHuyBo";
            btnHuyBo.Size = new Size(94, 29);
            btnHuyBo.TabIndex = 3;
            btnHuyBo.Text = "Hủy bỏ ";
            btnHuyBo.UseVisualStyleBackColor = true;
            btnHuyBo.Click += btnHuyBo_Click;
            // 
            // btnXoa
            // 
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoa.ForeColor = Color.Red;
            btnXoa.Location = new Point(592, 112);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSua.Location = new Point(451, 112);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sữa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnLuu
            // 
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.ForeColor = Color.Blue;
            btnLuu.Location = new Point(300, 112);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 29);
            btnLuu.TabIndex = 3;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThem.Location = new Point(172, 112);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // dgvKhoa
            // 
            dgvKhoa.AllowUserToAddRows = false;
            dgvKhoa.AllowUserToDeleteRows = false;
            dgvKhoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhoa.Columns.AddRange(new DataGridViewColumn[] { MaKhoa, TenKhoa, NgayThanhLap, TruongKhoa });
            dgvKhoa.Dock = DockStyle.Fill;
            dgvKhoa.Location = new Point(3, 23);
            dgvKhoa.MultiSelect = false;
            dgvKhoa.Name = "dgvKhoa";
            dgvKhoa.ReadOnly = true;
            dgvKhoa.RowHeadersWidth = 51;
            dgvKhoa.Size = new Size(1173, 467);
            dgvKhoa.TabIndex = 0;
            // 
            // MaKhoa
            // 
            MaKhoa.DataPropertyName = "MaKhoa";
            MaKhoa.HeaderText = "Mã Khoa";
            MaKhoa.MinimumWidth = 6;
            MaKhoa.Name = "MaKhoa";
            MaKhoa.ReadOnly = true;
            // 
            // TenKhoa
            // 
            TenKhoa.DataPropertyName = "TenKhoa";
            TenKhoa.HeaderText = "Tên Khoa";
            TenKhoa.MinimumWidth = 6;
            TenKhoa.Name = "TenKhoa";
            TenKhoa.ReadOnly = true;
            // 
            // NgayThanhLap
            // 
            NgayThanhLap.DataPropertyName = "NgayThanhLap";
            NgayThanhLap.HeaderText = "Ngày Thành Lập";
            NgayThanhLap.MinimumWidth = 6;
            NgayThanhLap.Name = "NgayThanhLap";
            NgayThanhLap.ReadOnly = true;
            // 
            // TruongKhoa
            // 
            TruongKhoa.DataPropertyName = "TruongKhoa";
            TruongKhoa.HeaderText = "Trưởng Khoa";
            TruongKhoa.MinimumWidth = 6;
            TruongKhoa.Name = "TruongKhoa";
            TruongKhoa.ReadOnly = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvKhoa);
            groupBox2.Location = new Point(19, 187);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1179, 493);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách loại sản phẩm";
            // 
            // txtTenKhoa
            // 
            txtTenKhoa.Location = new Point(682, 30);
            txtTenKhoa.Name = "txtTenKhoa";
            txtTenKhoa.Size = new Size(324, 27);
            txtTenKhoa.TabIndex = 1;
            // 
            // txtMaKhoa
            // 
            txtMaKhoa.Location = new Point(221, 34);
            txtMaKhoa.Name = "txtMaKhoa";
            txtMaKhoa.Size = new Size(324, 27);
            txtMaKhoa.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(571, 37);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 0;
            label2.Text = "Tên Khoa:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 33);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 0;
            label1.Text = "Mã Khoa:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(88, 72);
            label3.Name = "label3";
            label3.Size = new Size(119, 20);
            label3.TabIndex = 0;
            label3.Text = "Ngày Thành Lập:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dtpNgayLapKhoa);
            groupBox1.Controls.Add(btnThoat);
            groupBox1.Controls.Add(btnHuyBo);
            groupBox1.Controls.Add(btnXoa);
            groupBox1.Controls.Add(btnSua);
            groupBox1.Controls.Add(btnLuu);
            groupBox1.Controls.Add(btnThem);
            groupBox1.Controls.Add(txtTruongKhoa);
            groupBox1.Controls.Add(txtTenKhoa);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtMaKhoa);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(13, 23);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1185, 158);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin loại sản phầm";
            // 
            // dtpNgayLapKhoa
            // 
            dtpNgayLapKhoa.Format = DateTimePickerFormat.Custom;
            dtpNgayLapKhoa.Location = new Point(221, 67);
            dtpNgayLapKhoa.Name = "dtpNgayLapKhoa";
            dtpNgayLapKhoa.Size = new Size(324, 27);
            dtpNgayLapKhoa.TabIndex = 4;
            // 
            // txtTruongKhoa
            // 
            txtTruongKhoa.Location = new Point(682, 65);
            txtTruongKhoa.Name = "txtTruongKhoa";
            txtTruongKhoa.Size = new Size(324, 27);
            txtTruongKhoa.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(571, 75);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 0;
            label4.Text = "Trưởng Khoa:";
            // 
            // FrmKhoa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1210, 702);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FrmKhoa";
            Text = "Quản lý khoa";
            Load += FrmKhoa_Load;
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btnThoat;
        private Button btnHuyBo;
        private Button btnXoa;
        private Button btnSua;
        private Button btnLuu;
        private Button btnThem;
        private DataGridView dgvKhoa;
        private GroupBox groupBox2;
        private TextBox txtTenKhoa;
        private TextBox txtMaKhoa;
        private Label label2;
        private Label label1;
        private Label label3;
        private GroupBox groupBox1;
        private DateTimePicker dtpNgayLapKhoa;
        private DataGridViewTextBoxColumn MaKhoa;
        private DataGridViewTextBoxColumn TenKhoa;
        private DataGridViewTextBoxColumn NgayThanhLap;
        private DataGridViewTextBoxColumn TruongKhoa;
        private TextBox txtTruongKhoa;
        private Label label4;
    }
}