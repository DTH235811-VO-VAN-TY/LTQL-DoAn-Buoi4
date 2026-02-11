using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyDiemSV.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyDiemSV.Forms
{
    public partial class FrmKhoa : Form
    {
        public FrmKhoa()
        {
            InitializeComponent();
        }
        QLDSVDbContext context = new QLDSVDbContext();
        bool xuLyThem = false;
        public event EventHandler DataSaved;

        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;

            // Các ô nhập liệu
            txtMaKhoa.Enabled = giaTri; // Đây là ô Mã Ngành (theo designer cũ của bạn)
            txtTenKhoa.Enabled = giaTri;   // Đây là ô Tên Ngành
            dtpNgayLapKhoa.Enabled = giaTri;
            txtTruongKhoa.Enabled = giaTri;

            // Các nút thao tác
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnThoat.Enabled = !giaTri;
        }
        private void LoadData()
        {
            try
            {
                // 1. Lấy dữ liệu từ CSDL
                List<Khoa> kh = context.Khoa.ToList();

                // 2. Đổ dữ liệu vào BindingSource NGAY LẬP TỨC
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = kh; // <--- QUAN TRỌNG: Phải có dòng này ở đây!

                // 3. Binding dữ liệu vào các ô nhập liệu (Lúc này bindingSource đã có data nên không lỗi nữa)
                txtMaKhoa.DataBindings.Clear();
                txtMaKhoa.DataBindings.Add("Text", bindingSource, "MaKhoa", true, DataSourceUpdateMode.Never);

                txtTenKhoa.DataBindings.Clear();
                txtTenKhoa.DataBindings.Add("Text", bindingSource, "TenKhoa", true, DataSourceUpdateMode.Never);

                dtpNgayLapKhoa.DataBindings.Clear();
                // Lưu ý: Dùng "Value" thay vì "Text" cho DateTimePicker
                dtpNgayLapKhoa.DataBindings.Add("Value", bindingSource, "NgayThanhLap", true, DataSourceUpdateMode.Never);

                txtTruongKhoa.DataBindings.Clear();
                txtTruongKhoa.DataBindings.Add("Text", bindingSource, "TruongKhoa", true, DataSourceUpdateMode.Never);

                // 4. Hiển thị lên lưới (DataGridView)
                dgvKhoa.AutoGenerateColumns = false; // Tắt tự tạo cột thừa TRƯỚC khi gán dữ liệu
                dgvKhoa.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtTruongKhoa.Clear();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtMaKhoa.Text))
            {
                MessageBox.Show("Mã khoa không được để trống!", "Lỗi", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(string.IsNullOrEmpty(txtTenKhoa.Text))
            {
                MessageBox.Show("Tên khoa không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (xuLyThem)
                {
                    Khoa khoa = new Khoa();
                    khoa.MaKhoa = txtMaKhoa.Text;
                    khoa.TenKhoa = txtTenKhoa.Text;
                    khoa.NgayThanhLap = DateOnly.FromDateTime(dtpNgayLapKhoa.Value);
                    khoa.TruongKhoa = txtTruongKhoa.Text;

                    context.Khoa.Add(khoa);
                }
                else
                {
                    string maKhoa = txtMaKhoa.Text;
                    Khoa khoa = context.Khoa.FirstOrDefault(k => k.MaKhoa == maKhoa);
                    if (khoa != null)
                    {
                        khoa.TenKhoa = txtTenKhoa.Text;
                        khoa.NgayThanhLap = DateOnly.FromDateTime(dtpNgayLapKhoa.Value);
                        khoa.TruongKhoa = txtTruongKhoa.Text;
                    }
                }
                context.SaveChanges();
                LoadData();
                DataSaved?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BatTatChucNang(false);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            BatTatChucNang(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa khoa " + txtTenKhoa.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maKhoa = txtMaKhoa.Text;
                    Khoa khoa = context.Khoa.FirstOrDefault(k => k.MaKhoa == maKhoa);
                    if (khoa != null)
                    {
                        context.Khoa.Remove(khoa);
                        context.SaveChanges();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message);
                }



            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            FrmKhoa_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
           var traloi = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
            {
                this.Close();
            }
        }  

        private void FrmKhoa_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadData();
        }
    }
}
