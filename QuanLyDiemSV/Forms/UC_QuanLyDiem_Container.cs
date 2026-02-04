using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyDiemSV.Data;

namespace QuanLyDiemSV.Forms
{
    public partial class UC_QuanLyDiem_Container : UserControl
    {
        QLDSVDbContext context = new QLDSVDbContext();
        public event Action<string> OnChuyenManHinhNhapDiem;

        public UC_QuanLyDiem_Container()
        {
            InitializeComponent();
            this.Load += UC_QuanLyDiem_Container_Load;
            dgvDanhSachSV.CellContentClick += dgvDanhSachSV_CellContentClick;
        }

        private void UC_QuanLyDiem_Container_Load(object sender, EventArgs e)
        {
            LoadCboKhoa();
            SetupDataGridView(); // Cấu hình cột trước
            LoadData();
        }

        private void SetupDataGridView()
        {
            // 1. Map đúng DataPropertyName khớp với LINQ
            dgvDanhSachSV.AutoGenerateColumns = false; // Chặn tự sinh cột

            // Đảm bảo tên cột trong Designer (Name) trùng với bên dưới:
            if (dgvDanhSachSV.Columns["LopHanhChinh"] != null)
                dgvDanhSachSV.Columns["LopHanhChinh"].DataPropertyName = "TenLop"; // Sửa mapping

            if (dgvDanhSachSV.Columns["MaKhoa"] != null)
                dgvDanhSachSV.Columns["MaKhoa"].DataPropertyName = "TenKhoa"; // Sửa mapping

            // 2. Xử lý cột Thao tác dư thừa (Xóa cột text cũ trong designer nếu có)
            if (dgvDanhSachSV.Columns["ThaoTac"] != null)
            {
                dgvDanhSachSV.Columns.Remove("ThaoTac");
            }

            // 3. Thêm cột Button "Nhập điểm" nếu chưa có
            if (dgvDanhSachSV.Columns["btnNhapDiem"] == null)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnNhapDiem";
                btn.HeaderText = "Thao tác";
                btn.Text = "Nhập điểm";
                btn.UseColumnTextForButtonValue = true;
                dgvDanhSachSV.Columns.Add(btn);
            }
        }

        private void LoadData()
        {
            try
            {
                var query = from sv in context.SinhVien
                            join lop in context.LopHanhChinh on sv.MaLop equals lop.MaLop
                            join nganh in context.Nganh on lop.MaNganh equals nganh.MaNganh
                            join khoa in context.Khoa on nganh.MaKhoa equals khoa.MaKhoa
                            select new
                            {
                                MaSV = sv.MaSV,
                                HoTen = sv.HoTen,
                                NgaySinh = sv.NgaySinh,
                                GioiTinh = sv.GioiTinh,
                                TenLop = lop.TenLop,
                                TenKhoa = khoa.TenKhoa,
                                TrangThai = "Đang học"
                            };

                // Logic lọc theo khoa
                if (cboKhoa.SelectedIndex != -1 && cboKhoa.SelectedValue is string maKhoa)
                {
                    // Lọc client-side sau khi query vì cấu trúc join phức tạp
                    // Hoặc viết lại query dynamic. Ở đây dùng cách đơn giản:
                    var list = query.ToList().Where(x => x.TenKhoa == cboKhoa.Text).ToList();
                    // Lưu ý: Cách lọc chính xác nhất là thêm Where vào LINQ, nhưng ở đây tôi dùng filter list tạm thời
                    dgvDanhSachSV.DataSource = list;
                }
                else
                {
                    dgvDanhSachSV.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ... Các hàm LoadCboKhoa, dgvDanhSachSV_CellContentClick giữ nguyên như cũ ...
        private void LoadCboKhoa()
        {
            cboKhoa.DataSource = context.Khoa.ToList();
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
            cboKhoa.SelectedIndex = -1;
        }

        private void dgvDanhSachSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDanhSachSV.Columns[e.ColumnIndex].Name == "btnNhapDiem")
            {
                string maSV = dgvDanhSachSV.Rows[e.RowIndex].Cells["MaSV"].Value.ToString();
                OnChuyenManHinhNhapDiem?.Invoke(maSV);
            }
        }
    }
}