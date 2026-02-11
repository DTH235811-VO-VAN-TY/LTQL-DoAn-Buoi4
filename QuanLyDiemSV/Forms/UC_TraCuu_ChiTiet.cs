using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyDiemSV.Data;
using QuanLyDiemSV.DTO;

namespace QuanLyDiemSV.Forms
{
    public partial class UC_TraCuu_ChiTiet : UserControl
    {
        QLDSVDbContext context = new QLDSVDbContext();
        public event EventHandler QuayLaiTraCuulicked;

        public UC_TraCuu_ChiTiet()
        {
            InitializeComponent();
        }

        public void LoadDuLieuChiTiet(string maSV)
        {
            // Load thông tin sinh viên
            var sv = context.SinhVien
                .Include(s => s.MaLopNavigation).ThenInclude(l => l.MaNganhNavigation).ThenInclude(n => n.MaKhoaNavigation)
                .FirstOrDefault(s => s.MaSV == maSV);

            if (sv != null)
            {
                lblMaSV.Text = sv.MaSV;
                lblHoTen.Text = sv.HoTen;
                lblLop.Text = sv.MaLopNavigation?.TenLop;
                lblNganh.Text = sv.MaLopNavigation?.MaNganhNavigation?.TenNganh;
                lblKhoa.Text = sv.MaLopNavigation?.MaNganhNavigation?.MaKhoaNavigation?.TenKhoa;
            }

            LoadDiemVaTaoGiaoDien(maSV);
        }

        private void LoadDiemVaTaoGiaoDien(string maSV)
        {
            flowLayoutPanel1.Controls.Clear();

            // 1. Lấy dữ liệu thô
            var listDiemRaw = (from kq in context.KetQuaHocTap
                               join lhp in context.LopHocPhan on kq.MaLHP equals lhp.MaLHP
                               join mh in context.MonHoc on lhp.MaMon equals mh.MaMon
                               join hk in context.HocKy on lhp.MaHK equals hk.MaHK
                               where kq.MaSV == maSV
                               select new
                               {
                                   hk.MaHK,
                                   hk.TenHK,
                                   mh.MaMon,
                                   mh.TenMon,
                                   SoTinChi = mh.SoTinChi,
                                   DiemQT = kq.DiemGK ?? 0,
                                   DiemThi = kq.DiemCK ?? 0
                               }).ToList();

            if (listDiemRaw.Count == 0) return;

            // 2. Tính toán điểm
            var listDiemProcessed = listDiemRaw.Select(x => {
                decimal tongKet10 = (x.DiemQT * 0.3m) + (x.DiemThi * 0.7m);
                tongKet10 = Math.Round(tongKet10, 1);

                return new DiemChiTietDTO
                {
                    // STT sẽ gán sau khi Group
                    MaHK = x.MaHK,
                    TenHK = x.TenHK,
                    MaMon = x.MaMon,
                    TenMon = x.TenMon,
                    SoTinChi = x.SoTinChi,
                    DiemQT = x.DiemQT,
                    DiemThi = x.DiemThi,
                    DiemTongKet = tongKet10,
                    DiemHe4 = QuyDoiHe4((double)tongKet10),
                    DiemChu = QuyDoiDiemChu((double)tongKet10)
                };
            }).ToList();

            // 3. Gom nhóm theo Học kỳ
            var groups = listDiemProcessed.GroupBy(x => new { x.MaHK, x.TenHK }).OrderBy(g => g.Key.MaHK);

            foreach (var group in groups)
            {
                // Đánh số STT lại cho từng nhóm
                int stt = 1;
                var listGroup = group.ToList();
                listGroup.ForEach(x => x.STT = stt++);

                // --- TẠO GIAO DIỆN HỌC KỲ ---
                GroupBox gbHocKy = new GroupBox();
                gbHocKy.Text = group.Key.TenHK;
                gbHocKy.Width = flowLayoutPanel1.Width - 40;
                gbHocKy.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                gbHocKy.ForeColor = Color.Blue;

                DataGridView dgv = new DataGridView();
                dgv.BackgroundColor = Color.White;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AllowUserToAddRows = false;
                dgv.ReadOnly = true;
                dgv.RowHeadersVisible = false;
                dgv.Dock = DockStyle.Top;
                dgv.DataSource = listGroup;

                // --- ĐỊNH DẠNG CỘT TIẾNG VIỆT ---
                dgv.AutoGenerateColumns = false;
                ThemCotThuCong(dgv);
                //  DinhDangCotDataGridView(dgv);


                int gridHeight = (listGroup.Count * 30) + 40;
                dgv.Height = gridHeight;

                // Tính toán tổng kết HK
                TinhTongKetHocKy(listGroup, out double dtb10, out double dtb4, out int tcDat, out string xepLoai);

                // Logic: Chỉ xếp loại nếu >= 5 môn
                string strXepLoai = (listGroup.Count >= 5) ? xepLoai : "Chưa đủ môn xếp loại";

                Label lblTongKet = new Label();
                lblTongKet.Text = $"Điểm trung bình học kỳ (Hệ 10): {dtb10}\n" +
                                  $"Điểm trung bình học kỳ (Hệ 4):  {dtb4}\n" +
                                  $"Số tín chỉ đạt được trong HK:   {tcDat}\n" +
                                  $"Phân loại điểm trung bình HK:   {strXepLoai}";
                lblTongKet.Dock = DockStyle.Bottom;
                lblTongKet.ForeColor = Color.Black;
                lblTongKet.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                lblTongKet.AutoSize = false;
                lblTongKet.Height = 100;
                lblTongKet.Padding = new Padding(10, 10, 0, 0);

                gbHocKy.Controls.Add(lblTongKet);
                gbHocKy.Controls.Add(dgv);
                gbHocKy.Height = gridHeight + 130;
                flowLayoutPanel1.Controls.Add(gbHocKy);
            }

            // --- 4. TẠO GROUPBOX TỔNG KẾT TOÀN KHÓA (MỚI) ---
            ThemGroupTongKetToanKhoa(listDiemProcessed);
        }

        
        private void ThemCotThuCong(DataGridView dgv)
        {
            dgv.Columns.Clear();

            // Helper function để thêm cột nhanh
            void AddCol(string dataPropertyName, string headerText, int width = 0)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = dataPropertyName; // Tên biến trong DTO
                col.HeaderText = headerText;             // Tên hiển thị Tiếng Việt
                if (width > 0) col.Width = width;
                dgv.Columns.Add(col);
            }

            // Chỉ thêm những cột mình muốn hiển thị
            AddCol("STT", "STT", 50);
            AddCol("MaMon", "Mã MH");
            AddCol("TenMon", "Tên Môn Học");
            AddCol("SoTinChi", "Số TC");
            AddCol("DiemQT", "Điểm QT");
            AddCol("DiemThi", "Điểm Thi");
            AddCol("DiemTongKet", "TK (10)");
            AddCol("DiemHe4", "TK (4)");
            AddCol("DiemChu", "Điểm Chữ");
        }

        private void ThemGroupTongKetToanKhoa(List<DiemChiTietDTO> allDiem)
        {
            // Tính toán toàn khóa
            double tongDiem10 = 0;
            double tongDiem4 = 0;
            int tongTinChiDaHoc = 0;
            int tongTinChiDat = 0;

            foreach (var item in allDiem)
            {
                if (item.DiemTongKet != null)
                {
                    double d10 = (double)item.DiemTongKet;
                    double d4 = (double)item.DiemHe4;
                    int tc = item.SoTinChi;

                    tongDiem10 += d10 * tc;
                    tongDiem4 += d4 * tc;
                    tongTinChiDaHoc += tc;

                    if (d10 >= 4.0) tongTinChiDat += tc;
                }
            }

            double tk10 = tongTinChiDaHoc > 0 ? Math.Round(tongDiem10 / tongTinChiDaHoc, 2) : 0;
            double tk4 = tongTinChiDaHoc > 0 ? Math.Round(tongDiem4 / tongTinChiDaHoc, 2) : 0;

            // Xếp loại toàn khóa
            string xepLoai;
            if (tk4 >= 3.6) xepLoai = "Xuất sắc";
            else if (tk4 >= 3.2) xepLoai = "Giỏi";
            else if (tk4 >= 2.5) xepLoai = "Khá";
            else if (tk4 >= 2.0) xepLoai = "Trung bình";
            else xepLoai = "Yếu";

            // Tạo GroupBox Tổng kết
            GroupBox gbTongKet = new GroupBox();
            gbTongKet.Text = "TỔNG KẾT TOÀN KHÓA";
            gbTongKet.Width = flowLayoutPanel1.Width - 40;
            gbTongKet.Height = 160; // Cao hơn xíu
            gbTongKet.Font = new Font("Segoe UI", 11, FontStyle.Regular); // Chữ to hơn
            gbTongKet.ForeColor = Color.DarkRed; // Màu đỏ cho nổi bật

            Label lblNoiDung = new Label();
            lblNoiDung.Text = $"Điểm TB tích lũy (Hệ 10):   {tk10}\n" +
                              $"Điểm TB tích lũy (Hệ 4):    {tk4}\n" +
                              $"Tổng số tín chỉ đã học:     {tongTinChiDaHoc}\n" +
                              $"Tổng số tín chỉ ĐẠT:        {tongTinChiDat}\n" +
                              $"Xếp loại toàn khóa:         {xepLoai}";

            lblNoiDung.Dock = DockStyle.Fill;
            lblNoiDung.Padding = new Padding(10, 10, 0, 0);
            lblNoiDung.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblNoiDung.ForeColor = Color.Black;

            gbTongKet.Controls.Add(lblNoiDung);
            flowLayoutPanel1.Controls.Add(gbTongKet);
        }

        // --- CÁC HÀM HỖ TRỢ TÍNH TOÁN ---
        private decimal QuyDoiHe4(double diem10)
        {
            if (diem10 >= 8.5) return 4.0m;
            if (diem10 >= 7.0) return 3.0m;
            if (diem10 >= 5.5) return 2.0m;
            if (diem10 >= 4.0) return 1.0m;
            return 0.0m;
        }

        private string QuyDoiDiemChu(double diem10)
        {
            if (diem10 >= 8.5) return "A";
            if (diem10 >= 7.0) return "B";
            if (diem10 >= 5.5) return "C";
            if (diem10 >= 4.0) return "D";
            return "F";
        }

        private void TinhTongKetHocKy(List<DiemChiTietDTO> list, out double dtb10, out double dtb4, out int tcDat, out string xepLoai)
        {
            double tongDiem10 = 0;
            double tongDiem4 = 0;
            int tongTinChi = 0;
            tcDat = 0;

            foreach (var item in list)
            {
                if (item.DiemTongKet != null)
                {
                    double d10 = (double)item.DiemTongKet;
                    double d4 = (double)item.DiemHe4;
                    int tc = item.SoTinChi;

                    tongDiem10 += d10 * tc;
                    tongDiem4 += d4 * tc;
                    tongTinChi += tc;

                    if (d10 >= 4.0) tcDat += tc;
                }
            }

            dtb10 = tongTinChi > 0 ? Math.Round(tongDiem10 / tongTinChi, 2) : 0;
            dtb4 = tongTinChi > 0 ? Math.Round(tongDiem4 / tongTinChi, 2) : 0;

            if (dtb4 >= 3.6) xepLoai = "Xuất sắc";
            else if (dtb4 >= 3.2) xepLoai = "Giỏi";
            else if (dtb4 >= 2.5) xepLoai = "Khá";
            else if (dtb4 >= 2.0) xepLoai = "Trung bình";
            else xepLoai = "Yếu";
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayLaiTraCuulicked?.Invoke(this, EventArgs.Empty);
        }
    }
}