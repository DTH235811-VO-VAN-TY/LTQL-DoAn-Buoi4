using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyDiemSV.Data;
using QuanLyDiemSV.Forms;

namespace QuanLyDiemSV
{
    public partial class UC_LopHocPhan : UserControl
    {
        QLDSVDbContext context = new QLDSVDbContext();
        BindingSource bsLopHP = new BindingSource();
        bool xuLyThem = false;

        public UC_LopHocPhan()
        {
            InitializeComponent();
            this.Load += UC_LopHocPhan_Load;
        }

        private void UC_LopHocPhan_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadComboBoxData();
            LoadData();
        }

        #region 1. HÀM HỖ TRỢ VÀ LOAD DỮ LIỆU

        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnLamLai.Enabled = giaTri;

            txtMaLHP.Enabled = giaTri;
            txtTenLHP.Enabled = giaTri;
            txtPhongHoc.Enabled = giaTri;
            txtSiSo.Enabled = giaTri;

            cboMaMon.Enabled = giaTri;
            cboMaGV.Enabled = giaTri;
            cboHocKy.Enabled = giaTri;
            cboTrangThai.Enabled = giaTri;

            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
        }

        private void LoadComboBoxData()
        {
            try
            {
                cboMaMon.DataSource = context.MonHoc.ToList();
                cboMaMon.DisplayMember = "TenMon";
                cboMaMon.ValueMember = "MaMon";
                cboMaMon.SelectedIndex = -1;

                cboMaGV.DataSource = context.GiangVien.ToList();
                cboMaGV.DisplayMember = "HoTen";
                cboMaGV.ValueMember = "MaGV";
                cboMaGV.SelectedIndex = -1;

                cboHocKy.DataSource = context.HocKy.ToList();
                cboHocKy.DisplayMember = "TenHK";
                cboHocKy.ValueMember = "MaHK";
                cboHocKy.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                var listLHP = context.LopHocPhan.ToList();
                bsLopHP.DataSource = listLHP;

                txtMaLHP.DataBindings.Clear();
                txtTenLHP.DataBindings.Clear();
                txtPhongHoc.DataBindings.Clear();
                txtSiSo.DataBindings.Clear();
                cboMaMon.DataBindings.Clear();
                cboMaGV.DataBindings.Clear();
                cboHocKy.DataBindings.Clear();
                cboTrangThai.DataBindings.Clear();

                // Binding MaLHP (int) vào Text (string) - Tự động convert khi hiển thị
                txtMaLHP.DataBindings.Add("Text", bsLopHP, "MaLHP", true, DataSourceUpdateMode.Never);
                txtTenLHP.DataBindings.Add("Text", bsLopHP, "TenLopHP", true, DataSourceUpdateMode.Never);
                txtPhongHoc.DataBindings.Add("Text", bsLopHP, "PhongHoc", true, DataSourceUpdateMode.Never);
                txtSiSo.DataBindings.Add("Text", bsLopHP, "SiSoToiDa", true, DataSourceUpdateMode.Never);

                cboMaMon.DataBindings.Add("SelectedValue", bsLopHP, "MaMon", true, DataSourceUpdateMode.Never);
                cboMaGV.DataBindings.Add("SelectedValue", bsLopHP, "MaGV", true, DataSourceUpdateMode.Never);
                cboHocKy.DataBindings.Add("SelectedValue", bsLopHP, "MaHK", true, DataSourceUpdateMode.Never);

                dgvLopHocPhan.AutoGenerateColumns = false;
                dgvLopHocPhan.DataSource = bsLopHP;

                bsLopHP.CurrentChanged += BsLopHP_CurrentChanged;
                BsLopHP_CurrentChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BsLopHP_CurrentChanged(object sender, EventArgs e)
        {
            if (bsLopHP.Current != null && !xuLyThem)
            {
                var lhp = (LopHocPhan)bsLopHP.Current;
                // Giả sử: 1 = "Mở lớp", 0 = "Đóng"
                if (lhp.TrangThai == 1) cboTrangThai.SelectedIndex = 0;
                else cboTrangThai.SelectedIndex = 1;
            }
        }

        #endregion

        #region 2. XỬ LÝ NÚT BẤM (CRUD)

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);

            txtMaLHP.Clear();
            txtTenLHP.Clear();
            txtPhongHoc.Clear();
            txtSiSo.Text = "0";
            cboMaMon.SelectedIndex = -1;
            cboMaGV.SelectedIndex = -1;
            cboHocKy.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = 0;

            txtMaLHP.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (bsLopHP.Current == null) return;

            xuLyThem = false;
            BatTatChucNang(true);
            txtMaLHP.Enabled = false; // Không sửa khóa chính
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bsLopHP.Current == null) return;
            var lhp = (LopHocPhan)bsLopHP.Current;

            if (MessageBox.Show($"Bạn có chắc muốn xóa lớp {lhp.TenLopHP}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    context.LopHocPhan.Remove(lhp);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa: " + ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLHP.Text) || string.IsNullOrWhiteSpace(txtTenLHP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã lớp và Tên lớp!");
                return;
            }
            if (cboMaMon.SelectedValue == null || cboMaGV.SelectedValue == null || cboHocKy.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Môn, Giảng viên và Học kỳ!");
                return;
            }

            // --- QUAN TRỌNG: Ép kiểu string sang int ---
            if (!int.TryParse(txtMaLHP.Text.Trim(), out int maLHP))
            {
                MessageBox.Show("Mã Lớp Học Phần phải là số nguyên!");
                return;
            }

            try
            {
                if (xuLyThem) // --- THÊM ---
                {
                    // Check trùng mã (So sánh int với int)
                    if (context.LopHocPhan.Any(x => x.MaLHP == maLHP))
                    {
                        MessageBox.Show("Mã lớp học phần đã tồn tại!");
                        return;
                    }

                    LopHocPhan lhp = new LopHocPhan();
                    lhp.MaLHP = maLHP; // Gán giá trị int đã parse
                    lhp.TenLopHP = txtTenLHP.Text.Trim();
                    lhp.PhongHoc = txtPhongHoc.Text.Trim();
                    lhp.SiSoToiDa = int.TryParse(txtSiSo.Text, out int ss) ? ss : 0;

                    lhp.MaMon = cboMaMon.SelectedValue.ToString();
                    lhp.MaGV = cboMaGV.SelectedValue.ToString();
                    lhp.MaHK = cboHocKy.SelectedValue.ToString();
                    lhp.TrangThai = cboTrangThai.SelectedIndex == 0 ? 1 : 0;

                    context.LopHocPhan.Add(lhp);
                }
                else // --- SỬA ---
                {
                    // Tìm theo int
                    var lhp = context.LopHocPhan.Find(maLHP);
                    if (lhp != null)
                    {
                        lhp.TenLopHP = txtTenLHP.Text.Trim();
                        lhp.PhongHoc = txtPhongHoc.Text.Trim();
                        lhp.SiSoToiDa = int.TryParse(txtSiSo.Text, out int ss) ? ss : 0;
                        lhp.MaMon = cboMaMon.SelectedValue.ToString();
                        lhp.MaGV = cboMaGV.SelectedValue.ToString();
                        lhp.MaHK = cboHocKy.SelectedValue.ToString();
                        lhp.TrangThai = cboTrangThai.SelectedIndex == 0 ? 1 : 0;

                        context.LopHocPhan.Update(lhp);
                    }
                }

                context.SaveChanges();
                LoadData();
                BatTatChucNang(false);
                MessageBox.Show("Lưu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            bsLopHP.ResetBindings(false);
            BsLopHP_CurrentChanged(null, null);
        }

        private void btnAddHocKy_Click(object sender, EventArgs e)
        {
            FrmHocKy frm = new FrmHocKy();
            frm.ShowDialog();
            LoadComboBoxData();
        }

        #endregion
    }
}