namespace QuanLyDiemSV.Forms
{
    partial class FrmRole
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
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            button2 = new Button();
            button1 = new Button();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label5 = new Label();
            txtMota = new TextBox();
            label3 = new Label();
            txtTenQuyenHan = new TextBox();
            label2 = new Label();
            dgvRoleID = new TextBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            btnHuyBo = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnLuu = new Button();
            btnThem = new Button();
            label6 = new Label();
            groupBox1 = new GroupBox();
            btnThoat = new Button();
            dgvRole = new DataGridView();
            RoleID = new DataGridViewTextBoxColumn();
            RoleName = new DataGridViewTextBoxColumn();
            MoTA = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRole).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(911, 221);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 24;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(333, 230);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(222, 27);
            textBox1.TabIndex = 23;
            // 
            // button2
            // 
            button2.Location = new Point(698, 230);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 21;
            button2.Text = "Tải Lại";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(587, 229);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 22;
            button1.Text = "Tìm Kiếm";
            button1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(1076, 246);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(65, 24);
            radioButton2.TabIndex = 19;
            radioButton2.Text = "Giảm";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(1079, 216);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(62, 24);
            radioButton1.TabIndex = 20;
            radioButton1.TabStop = true;
            radioButton1.Text = "Tăng";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(32, 231);
            label5.Name = "label5";
            label5.Size = new Size(108, 20);
            label5.TabIndex = 18;
            label5.Text = "Kiểu Tìm Kiếm:";
            // 
            // txtMota
            // 
            txtMota.Location = new Point(215, 92);
            txtMota.Name = "txtMota";
            txtMota.Size = new Size(692, 30);
            txtMota.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(76, 95);
            label3.Name = "label3";
            label3.Size = new Size(59, 23);
            label3.TabIndex = 10;
            label3.Text = "Mô tả:";
            // 
            // txtTenQuyenHan
            // 
            txtTenQuyenHan.Location = new Point(661, 39);
            txtTenQuyenHan.Name = "txtTenQuyenHan";
            txtTenQuyenHan.Size = new Size(246, 30);
            txtTenQuyenHan.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(528, 42);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 10;
            label2.Text = "Quyền Hạn:";
            // 
            // dgvRoleID
            // 
            dgvRoleID.Location = new Point(215, 46);
            dgvRoleID.Name = "dgvRoleID";
            dgvRoleID.Size = new Size(246, 30);
            dgvRoleID.TabIndex = 11;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(157, 231);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(76, 46);
            label1.Name = "label1";
            label1.Size = new Size(64, 23);
            label1.TabIndex = 10;
            label1.Text = "RoleID:";
            // 
            // btnHuyBo
            // 
            btnHuyBo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuyBo.Location = new Point(695, 137);
            btnHuyBo.Name = "btnHuyBo";
            btnHuyBo.Size = new Size(94, 29);
            btnHuyBo.TabIndex = 6;
            btnHuyBo.Text = "Hủy bỏ ";
            btnHuyBo.UseVisualStyleBackColor = true;
            btnHuyBo.Click += btnHuyBo_Click;
            // 
            // btnXoa
            // 
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoa.ForeColor = Color.Red;
            btnXoa.Location = new Point(564, 137);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 7;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSua.Location = new Point(423, 137);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 8;
            btnSua.Text = "Sữa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnLuu
            // 
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.ForeColor = Color.Blue;
            btnLuu.Location = new Point(272, 137);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 29);
            btnLuu.TabIndex = 9;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThem.Location = new Point(144, 137);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 4;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(798, 233);
            label6.Name = "label6";
            label6.Size = new Size(97, 20);
            label6.TabIndex = 25;
            label6.Text = "Kiểu Sắp Xếp";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtMota);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtTenQuyenHan);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dgvRoleID);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnThoat);
            groupBox1.Controls.Add(btnHuyBo);
            groupBox1.Controls.Add(btnXoa);
            groupBox1.Controls.Add(btnSua);
            groupBox1.Controls.Add(btnLuu);
            groupBox1.Controls.Add(btnThem);
            groupBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(23, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1173, 200);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin học kỳ";
            // 
            // btnThoat
            // 
            btnThoat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThoat.Location = new Point(813, 137);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 5;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // dgvRole
            // 
            dgvRole.AllowUserToAddRows = false;
            dgvRole.AllowUserToDeleteRows = false;
            dgvRole.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRole.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRole.Columns.AddRange(new DataGridViewColumn[] { RoleID, RoleName, MoTA });
            dgvRole.Dock = DockStyle.Fill;
            dgvRole.Location = new Point(3, 23);
            dgvRole.MultiSelect = false;
            dgvRole.Name = "dgvRole";
            dgvRole.ReadOnly = true;
            dgvRole.RowHeadersWidth = 51;
            dgvRole.Size = new Size(1173, 422);
            dgvRole.TabIndex = 0;
            // 
            // RoleID
            // 
            RoleID.DataPropertyName = "RoleID";
            RoleID.HeaderText = "RoleID";
            RoleID.MinimumWidth = 6;
            RoleID.Name = "RoleID";
            RoleID.ReadOnly = true;
            // 
            // RoleName
            // 
            RoleName.DataPropertyName = "RoleName";
            RoleName.HeaderText = "Quyền Hạn";
            RoleName.MinimumWidth = 6;
            RoleName.Name = "RoleName";
            RoleName.ReadOnly = true;
            // 
            // MoTA
            // 
            MoTA.DataPropertyName = "MoTa";
            MoTA.HeaderText = "Mô tả";
            MoTA.MinimumWidth = 6;
            MoTA.Name = "MoTA";
            MoTA.ReadOnly = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvRole);
            groupBox2.Location = new Point(20, 265);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1179, 448);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách học kỳ";
            // 
            // FrmRole
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1219, 725);
            Controls.Add(comboBox2);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(label5);
            Controls.Add(comboBox1);
            Controls.Add(label6);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "FrmRole";
            Text = "Quyền Hạn";
            Load += FrmRole_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRole).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox2;
        private TextBox textBox1;
        private Button button2;
        private Button button1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label5;
        private TextBox txtMota;
        private Label label3;
        private TextBox txtTenQuyenHan;
        private Label label2;
        private TextBox dgvRoleID;
        private ComboBox comboBox1;
        private Label label1;
        private Button btnHuyBo;
        private Button btnXoa;
        private Button btnSua;
        private Button btnLuu;
        private Button btnThem;
        private Label label6;
        private GroupBox groupBox1;
        private Button btnThoat;
        private DataGridView dgvRole;
        private DataGridViewTextBoxColumn RoleID;
        private DataGridViewTextBoxColumn RoleName;
        private DataGridViewTextBoxColumn MoTA;
        private GroupBox groupBox2;
    }
}