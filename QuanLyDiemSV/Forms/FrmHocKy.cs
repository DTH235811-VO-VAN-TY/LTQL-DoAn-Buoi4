using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDiemSV.Data; // Namespace chứa DBContext và Entity

namespace QuanLyDiemSV.Forms
{
    public partial class FrmHocKy : Form
    {
        public FrmHocKy()
        {
            InitializeComponent();
        }

        // Khởi tạo Context và biến cờ
        QLDSVDbContext context = new QLDSVDbContext();
        bool xuLyThem = false;

        // Hàm bật tắt chức năng (giống FrmKhoa)
        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;

            // Các ô nhập liệu
            txtMaHK.Enabled = giaTri;
            txtTenHK.Enabled = giaTri;
            txtNamBatDau.Enabled = giaTri;
            txtNamKetThuc.Enabled = giaTri;

            // Các nút thao tác
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;

            // Nút thoát luôn sáng hoặc tắt theo logic bạn muốn, ở đây để luôn sáng cũng được
            // hoặc tắt khi đang nhập liệu:
            btnThoat.Enabled = !giaTri;
        }

        // Hàm tải dữ liệu
        private void LoadData()
        {
            try
            {
                // 1. Lấy dữ liệu
                List<HocKy> listHK = context.HocKy.ToList();

                // 2. Tạo BindingSource
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = listHK;

                // 3. Binding vào TextBox (Xóa binding cũ trước)
                txtMaHK.DataBindings.Clear();
                txtMaHK.DataBindings.Add("Text", bindingSource, "MaHK", true, DataSourceUpdateMode.Never);

                txtTenHK.DataBindings.Clear();
                txtTenHK.DataBindings.Add("Text", bindingSource, "TenHK", true, DataSourceUpdateMode.Never);

                txtNamBatDau.DataBindings.Clear();
                txtNamBatDau.DataBindings.Add("Text", bindingSource, "NamHocBatDau", true, DataSourceUpdateMode.Never);

                txtNamKetThuc.DataBindings.Clear();
                txtNamKetThuc.DataBindings.Add("Text", bindingSource, "NamHocKetThuc", true, DataSourceUpdateMode.Never);

                // 4. Hiển thị lên Grid
                dgvHocKy.AutoGenerateColumns = false; // Chặn cột thừa
                dgvHocKy.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Sự kiện Load Form
        private void FrmHocKy_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadData();
        }

        // Nút THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);

            // Xóa trắng ô nhập
            txtMaHK.Clear();
            txtTenHK.Clear();
            txtNamBatDau.Clear();
            txtNamKetThuc.Clear();

            txtMaHK.Focus();
        }

        // Nút SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHK.Text))
            {
                MessageBox.Show("Vui lòng chọn học kỳ cần sửa!");
                return;
            }

            xuLyThem = false;
            BatTatChucNang(true);
            txtMaHK.Enabled = false; // Khóa chính không được sửa
        }

        // Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHK.Text)) return;

            if (MessageBox.Show("Xác nhận xóa học kỳ " + txtTenHK.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maHK = txtMaHK.Text;
                    var hk = context.HocKy.Find(maHK);
                    if (hk != null)
                    {
                        context.HocKy.Remove(hk);
                        context.SaveChanges();
                        LoadData();
                        MessageBox.Show("Xóa thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa (dữ liệu đang được sử dụng): " + ex.Message);
                }
            }
        }

        // Nút LƯU
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu
            if (string.IsNullOrWhiteSpace(txtMaHK.Text))
            {
                MessageBox.Show("Mã học kỳ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTenHK.Text))
            {
                MessageBox.Show("Tên học kỳ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (xuLyThem) // --- Thêm mới ---
                {
                    // Kiểm tra trùng mã
                    var check = context.HocKy.Find(txtMaHK.Text);
                    if (check != null)
                    {
                        MessageBox.Show("Mã học kỳ này đã tồn tại!");
                        return;
                    }

                    HocKy hk = new HocKy();
                    hk.MaHK = txtMaHK.Text.Trim();
                    hk.TenHK = txtTenHK.Text.Trim();

                    // Chuyển đổi chuỗi sang số nguyên cho Năm học
                    if (int.TryParse(txtNamBatDau.Text, out int namBD))
                        hk.NamHocBatDau = namBD;
                    else
                        hk.NamHocBatDau = 0; // Hoặc báo lỗi nếu bắt buộc nhập số

                    if (int.TryParse(txtNamKetThuc.Text, out int namKT))
                        hk.NamHocKetThuc = namKT;
                    else
                        hk.NamHocKetThuc = 0;

                    context.HocKy.Add(hk);
                }
                else // --- Cập nhật (Sửa) ---
                {
                    var hk = context.HocKy.Find(txtMaHK.Text);
                    if (hk != null)
                    {
                        hk.TenHK = txtTenHK.Text.Trim();

                        if (int.TryParse(txtNamBatDau.Text, out int namBD))
                            hk.NamHocBatDau = namBD;

                        if (int.TryParse(txtNamKetThuc.Text, out int namKT))
                            hk.NamHocKetThuc = namKT;
                    }
                }

                context.SaveChanges(); // Lưu xuống DB
                LoadData();            // Tải lại lên lưới
                BatTatChucNang(false); // Khóa nút
                MessageBox.Show("Lưu dữ liệu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        // Nút HỦY BỎ
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadData(); // Load lại để phục hồi dữ liệu cũ vào textbox
        }

        // Nút THOÁT
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}