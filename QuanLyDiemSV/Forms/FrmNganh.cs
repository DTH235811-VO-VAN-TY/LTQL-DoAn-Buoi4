using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDiemSV.Data; // Đảm bảo namespace đúng với project

namespace QuanLyDiemSV.Forms
{
    public partial class FrmNganh : Form
    {
        public FrmNganh()
        {
            InitializeComponent();
        }

        // Khởi tạo Context
        QLDSVDbContext context = new QLDSVDbContext();
        bool xuLyThem = false; // Cờ kiểm tra trạng thái Thêm/Sửa

        // Hàm bật tắt các nút chức năng
        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;

            // Các ô nhập liệu
            txtTenLoai.Enabled = giaTri; // Đây là ô Mã Ngành (theo designer cũ của bạn)
            textBox1.Enabled = giaTri;   // Đây là ô Tên Ngành
            comboBox1.Enabled = giaTri;  // ComboBox Khoa

            // Các nút thao tác
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnThoat.Enabled = !giaTri;
        }

        // Sự kiện Load Form
        private void FrmNganh_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadData();
        }

        // Hàm tải dữ liệu (Binding)
        private void LoadData()
        {
            try
            {
                // 1. Tải danh sách KHOA vào ComboBox trước
                // Để khi binding Ngành, nó có dữ liệu để map Mã Khoa
                List<Khoa> listKhoa = context.Khoa.ToList();
                comboBox1.DataSource = listKhoa;
                comboBox1.DisplayMember = "TenKhoa"; // Hiển thị tên khoa
                comboBox1.ValueMember = "MaKhoa";    // Giá trị ngầm là Mã khoa

                // 2. Tải danh sách NGÀNH
                List<Nganh> listNganh = context.Nganh.ToList();

                // Tạo BindingSource
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = listNganh;

                // 3. Xóa binding cũ (tránh lỗi khi reload)
                txtTenLoai.DataBindings.Clear();
                textBox1.DataBindings.Clear();
                comboBox1.DataBindings.Clear();

                // 4. Binding dữ liệu vào TextBox
                // txtTenLoai đang là ô MÃ NGÀNH trong designer của bạn
                txtTenLoai.DataBindings.Add("Text", bindingSource, "MaNganh", false, DataSourceUpdateMode.Never);

                // textBox1 đang là ô TÊN NGÀNH
                textBox1.DataBindings.Add("Text", bindingSource, "TenNganh", false, DataSourceUpdateMode.Never);

                // 5. Binding cho ComboBox (Quan trọng)
                // SelectedValue của ComboBox sẽ nhảy theo MaKhoa của Ngành đang chọn
                comboBox1.DataBindings.Add("SelectedValue", bindingSource, "MaKhoa", false, DataSourceUpdateMode.Never);

                dgvLoaiSanPham.AutoGenerateColumns = false;
                // 6. Đổ dữ liệu lên Grid
                dgvLoaiSanPham.DataSource = bindingSource;

                // Lưu ý: Trong Designer, bạn cần chỉnh DataPropertyName của các cột:
                // Cột ID -> MaNganh
                // Cột Tên -> TenNganh
                // Cột Khoa -> MaKhoa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Nút THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);

            // Xóa trắng các ô để nhập mới
            txtTenLoai.Clear(); // Mã ngành
            textBox1.Clear();   // Tên ngành
            comboBox1.SelectedIndex = -1; // Reset ComboBox

            txtTenLoai.Focus();
        }

        // Nút SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn ngành cần sửa!");
                return;
            }

            xuLyThem = false;
            BatTatChucNang(true);

            // Khi sửa thì KHÔNG được sửa Mã Ngành (Khóa chính)
            txtTenLoai.Enabled = false;
        }

        // Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenLoai.Text)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa ngành: " + textBox1.Text + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string maNganh = txtTenLoai.Text;
                    var nganh = context.Nganh.Find(maNganh);

                    if (nganh != null)
                    {
                        context.Nganh.Remove(nganh);
                        context.SaveChanges();
                        MessageBox.Show("Xóa thành công!");
                        LoadData(); // Tải lại form
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa (có thể ngành này đang có sinh viên/lớp học): " + ex.Message);
                }
            }
        }

        // Nút LƯU
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text)) // Mã ngành
            {
                MessageBox.Show("Vui lòng nhập Mã ngành!");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text)) // Tên ngành
            {
                MessageBox.Show("Vui lòng nhập Tên ngành!");
                return;
            }
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khoa!");
                return;
            }

            try
            {
                if (xuLyThem) // --- XỬ LÝ THÊM MỚI ---
                {
                    // Kiểm tra trùng mã
                    var check = context.Nganh.Find(txtTenLoai.Text);
                    if (check != null)
                    {
                        MessageBox.Show("Mã ngành này đã tồn tại!");
                        return;
                    }

                    Nganh n = new Nganh();
                    n.MaNganh = txtTenLoai.Text.Trim();
                    n.TenNganh = textBox1.Text.Trim();
                    n.MaKhoa = comboBox1.SelectedValue.ToString(); // Lấy mã khoa từ ComboBox

                    context.Nganh.Add(n);
                    context.SaveChanges();
                    MessageBox.Show("Thêm mới thành công!");
                }
                else // --- XỬ LÝ CẬP NHẬT (SỬA) ---
                {
                    var n = context.Nganh.Find(txtTenLoai.Text); // Tìm theo Mã ngành (khóa chính)
                    if (n != null)
                    {
                        n.TenNganh = textBox1.Text.Trim();
                        n.MaKhoa = comboBox1.SelectedValue.ToString();

                        context.Nganh.Update(n);
                        context.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!");
                    }
                }

                BatTatChucNang(false);
                LoadData(); // Load lại dữ liệu để Binding cập nhật lại
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
            // BindingSource sẽ tự động trả lại giá trị cũ của dòng đang chọn
            // nhưng để chắc chắn, ta load lại data
            LoadData();
        }

        // Nút THOÁT
        private void btnThoat_Click(object sender, EventArgs e)
        {
            var traloi = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
                this.Close();
        }

        private void txtTenLoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddKhoa_Click(object sender, EventArgs e)
        {
            FrmKhoa frmKhoa = new FrmKhoa();
            frmKhoa.ShowDialog();

        }
    }
}