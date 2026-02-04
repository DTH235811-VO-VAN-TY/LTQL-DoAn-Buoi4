
namespace QuanLyDiemSV.Forms
{
    partial class FrmNganh
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
            groupBox1 = new GroupBox();
            comboBox1 = new ComboBox();
            btnThoat = new Button();
            btnHuyBo = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnLuu = new Button();
            btnAddKhoa = new Button();
            btnThem = new Button();
            textBox1 = new TextBox();
            txtTenLoai = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dgvLoaiSanPham = new DataGridView();
            MaNganh = new DataGridViewTextBoxColumn();
            TenNganh = new DataGridViewTextBoxColumn();
            Khoa = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSanPham).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(btnThoat);
            groupBox1.Controls.Add(btnHuyBo);
            groupBox1.Controls.Add(btnXoa);
            groupBox1.Controls.Add(btnSua);
            groupBox1.Controls.Add(btnLuu);
            groupBox1.Controls.Add(btnAddKhoa);
            groupBox1.Controls.Add(btnThem);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(txtTenLoai);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(14, 18);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1185, 158);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin loại sản phầm";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(112, 69);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(323, 28);
            comboBox1.TabIndex = 4;
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
            // btnAddKhoa
            // 
            btnAddKhoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddKhoa.Location = new Point(451, 68);
            btnAddKhoa.Name = "btnAddKhoa";
            btnAddKhoa.Size = new Size(122, 29);
            btnAddKhoa.TabIndex = 2;
            btnAddKhoa.Text = "Thêm Khoa";
            btnAddKhoa.UseVisualStyleBackColor = true;
            btnAddKhoa.Click += btnAddKhoa_Click;
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
            // textBox1
            // 
            textBox1.Location = new Point(556, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(324, 27);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += txtTenLoai_TextChanged;
            // 
            // txtTenLoai
            // 
            txtTenLoai.Location = new Point(111, 27);
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(324, 27);
            txtTenLoai.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(451, 33);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 0;
            label2.Text = "Tên Ngành:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 72);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 0;
            label3.Text = "Mã Khoa:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 30);
            label1.Name = "label1";
            label1.Size = new Size(81, 20);
            label1.TabIndex = 0;
            label1.Text = "Mã Ngành:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvLoaiSanPham);
            groupBox2.Location = new Point(20, 182);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1179, 493);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách loại sản phẩm";
            // 
            // dgvLoaiSanPham
            // 
            dgvLoaiSanPham.AllowUserToAddRows = false;
            dgvLoaiSanPham.AllowUserToDeleteRows = false;
            dgvLoaiSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoaiSanPham.Columns.AddRange(new DataGridViewColumn[] { MaNganh, TenNganh, Khoa });
            dgvLoaiSanPham.Dock = DockStyle.Fill;
            dgvLoaiSanPham.Location = new Point(3, 23);
            dgvLoaiSanPham.MultiSelect = false;
            dgvLoaiSanPham.Name = "dgvLoaiSanPham";
            dgvLoaiSanPham.ReadOnly = true;
            dgvLoaiSanPham.RowHeadersWidth = 51;
            dgvLoaiSanPham.Size = new Size(1173, 467);
            dgvLoaiSanPham.TabIndex = 0;
            // 
            // MaNganh
            // 
            MaNganh.DataPropertyName = "MaNganh";
            MaNganh.HeaderText = "Mã Ngành";
            MaNganh.MinimumWidth = 6;
            MaNganh.Name = "MaNganh";
            MaNganh.ReadOnly = true;
            // 
            // TenNganh
            // 
            TenNganh.DataPropertyName = "TenNganh";
            TenNganh.HeaderText = "Tên Ngành";
            TenNganh.MinimumWidth = 6;
            TenNganh.Name = "TenNganh";
            TenNganh.ReadOnly = true;
            // 
            // Khoa
            // 
            Khoa.DataPropertyName = "MaKhoa";
            Khoa.HeaderText = "Khoa";
            Khoa.MinimumWidth = 6;
            Khoa.Name = "Khoa";
            Khoa.ReadOnly = true;
            // 
            // FrmNganh
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1213, 703);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "FrmNganh";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thêm Ngành Học";
            Load += FrmNganh_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSanPham).EndInit();
            ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnThoat;
        private Button btnHuyBo;
        private Button btnXoa;
        private Button btnSua;
        private Button btnLuu;
        private Button btnThem;
        private TextBox txtTenLoai;
        private Label label1;
        private GroupBox groupBox2;
        private DataGridView dgvLoaiSanPham;
        private TextBox textBox1;
        private Label label2;
        private ComboBox comboBox1;
        private Button btnAddKhoa;
        private Label label3;
        private DataGridViewTextBoxColumn MaNganh;
        private DataGridViewTextBoxColumn TenNganh;
        private DataGridViewTextBoxColumn Khoa;
    }
}