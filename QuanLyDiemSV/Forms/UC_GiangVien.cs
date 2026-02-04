using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDiemSV.Data; // Đảm bảo namespace này đúng

namespace QuanLyDiemSV.Forms
{
    public partial class UC_GiangVien : UserControl
    {
        // Khởi tạo Context và BindingSource
        QLDSVDbContext context = new QLDSVDbContext();
        BindingSource bsGiangVien = new BindingSource();
        bool xuLyThem = false;

        public UC_GiangVien()
        {
            InitializeComponent();
            this.Load += UC_GiangVien_Load;
        }

        private void UC_GiangVien_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadCboKhoa();
            LoadCboHocVi();
            LoadData();
        }

        #region 1. HÀM HỖ TRỢ & LOAD DỮ LIỆU

        private void BatTatChucNang(bool giaTri)
        {
            // Các nút thao tác
            btnLuu.Enabled = giaTri;
            btnLamLai.Enabled = giaTri;

            // Các ô nhập liệu
            txtMaGV.Enabled = giaTri;
            txtHoTenGV.Enabled = giaTri;
            dtpNamSinhGV.Enabled = giaTri;
            radNam.Enabled = giaTri;
            radNu.Enabled = giaTri;
            txtEmail.Enabled = giaTri;   // Ô Email
            txtSDT.Enabled = giaTri;  // Ô Số điện thoại (do tên biến trong designer đặt hơi lạ)
            cboHocVi.Enabled = giaTri;
            cboKhoa.Enabled = giaTri;

            // Nút chức năng
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
        }

        private void LoadCboKhoa()
        {
            var listKhoa = context.Khoa.ToList();
            cboKhoa.DataSource = listKhoa;
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
            cboKhoa.SelectedIndex = -1;
        }

        private void LoadCboHocVi()
        {
            // Tạo danh sách học vị (hoặc load từ DB nếu có bảng riêng)
            List<string> listHocVi = new List<string>() { "Cử nhân", "Thạc sĩ", "Tiến sĩ", "Phó Giáo sư", "Giáo sư" };
            cboHocVi.DataSource = listHocVi;
        }

        private void LoadData()
        {
            try
            {
                // 1. Lấy dữ liệu từ DB
                var listGV = context.GiangVien.ToList();

                // 2. Gán vào BindingSource
                bsGiangVien.DataSource = listGV;

                // 3. Xóa Binding cũ
                txtMaGV.DataBindings.Clear();
                txtHoTenGV.DataBindings.Clear();
                dtpNamSinhGV.DataBindings.Clear();
                txtEmail.DataBindings.Clear();   
                txtSDT.DataBindings.Clear();  // SDT
                cboKhoa.DataBindings.Clear();
                cboHocVi.DataBindings.Clear();

                // 4. Tạo Binding mới
                txtMaGV.DataBindings.Add("Text", bsGiangVien, "MaGV", true, DataSourceUpdateMode.Never);
                txtHoTenGV.DataBindings.Add("Text", bsGiangVien, "HoTen", true, DataSourceUpdateMode.Never);

                // Binding ngày sinh (DateTimePicker)
                dtpNamSinhGV.DataBindings.Add("Value", bsGiangVien, "NgaySinh", true, DataSourceUpdateMode.Never);

                // Binding CCCD và SDT
                txtEmail.DataBindings.Add("Text", bsGiangVien, "Email", true, DataSourceUpdateMode.Never);
                txtSDT.DataBindings.Add("Text", bsGiangVien, "SDT", true, DataSourceUpdateMode.Never); // Lưu ý tên biến

                // Binding ComboBox
                cboKhoa.DataBindings.Add("SelectedValue", bsGiangVien, "MaKhoa", true, DataSourceUpdateMode.Never);
                cboHocVi.DataBindings.Add("SelectedItem", bsGiangVien, "HocVi", true, DataSourceUpdateMode.Never);

                // 5. Hiển thị lên Grid
                dgvAdminGiangVien.AutoGenerateColumns = false;
                dgvAdminGiangVien.DataSource = bsGiangVien;

                // 6. Xử lý sự kiện thay đổi dòng để map RadioButton (Giới tính)
                // Vì BindingSource không bind trực tiếp tốt với RadioButton
                bsGiangVien.CurrentChanged += BsGiangVien_CurrentChanged;
                BsGiangVien_CurrentChanged(null, null); // Gọi lần đầu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Sự kiện xử lý riêng cho RadioButton Giới tính
        private void BsGiangVien_CurrentChanged(object sender, EventArgs e)
        {
            if (bsGiangVien.Current != null && !xuLyThem)
            {
                var gv = (GiangVien)bsGiangVien.Current;
                if (gv.GioiTinh == "Nam") radNam.Checked = true;
                else radNu.Checked = true;
            }
        }

        #endregion

        #region 2. XỬ LÝ NÚT BẤM (CRUD)

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);

            // Xóa trắng để nhập mới
            txtMaGV.Clear();
            txtHoTenGV.Clear();
            txtEmail.Clear();
            txtSDT.Clear(); // SDT
            dtpNamSinhGV.Value = DateTime.Now;
            radNam.Checked = true;
            cboKhoa.SelectedIndex = -1;
            cboHocVi.SelectedIndex = 0;

            txtMaGV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (bsGiangVien.Current == null) return;

            xuLyThem = false;
            BatTatChucNang(true);
            txtMaGV.Enabled = false; // Không sửa khóa chính
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bsGiangVien.Current == null) return;
            var gv = (GiangVien)bsGiangVien.Current;

            if (MessageBox.Show($"Bạn có chắc muốn xóa giảng viên {gv.HoTen}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    context.GiangVien.Remove(gv);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa (dữ liệu đang được sử dụng): " + ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaGV.Text) || string.IsNullOrWhiteSpace(txtHoTenGV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã GV và Họ tên!");
                return;
            }

            try
            {
                if (xuLyThem) // --- THÊM MỚI ---
                {
                    // Check trùng mã
                    if (context.GiangVien.Any(x => x.MaGV == txtMaGV.Text.Trim()))
                    {
                        MessageBox.Show("Mã giảng viên đã tồn tại!");
                        return;
                    }

                    GiangVien gv = new GiangVien();
                    gv.MaGV = txtMaGV.Text.Trim();
                    gv.HoTen = txtHoTenGV.Text.Trim();
                    gv.NgaySinh = DateOnly.FromDateTime(dtpNamSinhGV.Value); // DateTime
                    gv.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
                    gv.Email = txtEmail.Text.Trim();
                    gv.SDT = txtSDT.Text.Trim(); // Ô này là SDT
                    gv.HocVi = cboHocVi.SelectedItem?.ToString();
                    gv.MaKhoa = cboKhoa.SelectedValue?.ToString();

                    context.GiangVien.Add(gv);
                }
                else // --- SỬA ---
                {
                    var gv = context.GiangVien.Find(txtMaGV.Text);
                    if (gv != null)
                    {
                        gv.HoTen = txtHoTenGV.Text.Trim();
                        gv.NgaySinh = DateOnly.FromDateTime(dtpNamSinhGV.Value);
                        gv.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
                        gv.Email = txtEmail.Text.Trim();
                        gv.SDT = txtSDT.Text.Trim();
                        gv.HocVi = cboHocVi.SelectedItem?.ToString();
                        gv.MaKhoa = cboKhoa.SelectedValue?.ToString();

                        context.GiangVien.Update(gv);
                    }
                }

                context.SaveChanges();
                LoadData();
                BatTatChucNang(false);
                MessageBox.Show("Lưu dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            bsGiangVien.ResetBindings(false); // Reset về giá trị cũ

            // Gọi lại hàm này để reset radio button về đúng dòng đang chọn
            BsGiangVien_CurrentChanged(null, null);
        }

        #endregion
    }
}