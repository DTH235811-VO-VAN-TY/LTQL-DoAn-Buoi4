using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyDiemSV.Data;
using GUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class UC_Diem : UserControl
    {
        QLDSVDbContext context = new QLDSVDbContext();
        BindingSource bsDiem = new BindingSource();
        public event Action OnQuayLai;

        string currentMaSV = "";
        bool xuLyThem = false;

        public UC_Diem()
        {
            InitializeComponent();
            this.Load += UC_Diem_Load;
        }

        private void UC_Diem_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadCboHocKy(); 
            
            CauHinhCotGridDiem();
            bsDiem.CurrentChanged += BsDiem_CurrentChanged;

            // Đăng ký sự kiện khi chọn học kỳ (nếu Designer chưa bắt)
            cboHocKy.SelectedIndexChanged += cboHocKy_SelectedIndexChanged;
        }

        #region 1. CẤU HÌNH & LOAD COMBOBOX

        private void CauHinhCotGridDiem()
        {
            dgvBangDiem.AutoGenerateColumns = false;
            dgvBangDiem.Columns.Clear();

            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "ID", Visible = false });
            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Môn", DataPropertyName = "MaMon", Width = 100 });
            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên Môn", DataPropertyName = "TenMon", Width = 250 });
            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "TC", DataPropertyName = "SoTinChi", Width = 50 });
            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đ.Quá Trình", DataPropertyName = "DiemGK" });
            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đ.Cuối Kỳ", DataPropertyName = "DiemCK" });
            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "TK(10)", DataPropertyName = "DiemTongKet" });
            dgvBangDiem.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "TK(CH)", DataPropertyName = "DiemChu" });
        }

        private void LoadCboHocKy()
        {
            try
            {
                var listHK = context.HocKy.OrderByDescending(x => x.TenHK).ToList();
                cboHocKy.DataSource = listHK;
                cboHocKy.DisplayMember = "TenHK";
                cboHocKy.ValueMember = "MaHK";

                // Mặc định chọn học kỳ mới nhất
                if (listHK.Count > 0) cboHocKy.SelectedIndex = 0;
            }
            catch { }
        }

        // Sự kiện: Khi chọn Học kỳ -> Load lại danh sách Môn học tương ứng

        private void LoadComboBoxMonHoc(string maHK)
        {
            try
            {
                // Lọc Lớp học phần theo Học kỳ đã chọn
                var listLHP = context.LopHocPhan
                    .Where(x => x.MaHK == maHK) // Chỉ lấy môn thuộc học kỳ này
                    .Join(context.MonHoc,
                          lhp => lhp.MaMon,
                          mh => mh.MaMon,
                          (lhp, mh) => new { lhp.MaLHP, mh.TenMon })
                    .ToList()
                    .GroupBy(x => x.TenMon)
                    .Select(g => new
                    {
                        MaLHP = g.First().MaLHP,
                        TenHienThi = g.Key
                    })
                    .OrderBy(x => x.TenHienThi)
                    .ToList();

                cboMaMon.DataSource = listLHP;
                cboMaMon.DisplayMember = "TenHienThi";
                cboMaMon.ValueMember = "MaLHP";
                cboMaMon.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải môn học: " + ex.Message);
            }
        }

        #endregion

        #region 2. XỬ LÝ DỮ LIỆU & HIỂN THỊ

        public class DiemViewModel
        {
            public int ID { get; set; }
            public int MaLHP { get; set; }
            public string MaMon { get; set; }
            public string TenMon { get; set; }
            public int SoTinChi { get; set; }
            public decimal? DiemGK { get; set; }
            public decimal? DiemCK { get; set; }

            public decimal? DiemTongKet => (DiemGK.HasValue && DiemCK.HasValue) ? (DiemGK * 0.4m + DiemCK * 0.6m) : null;

            public double? DiemHe4
            {
                get
                {
                    if (DiemTongKet == null) return null;
                    float tk = (float)DiemTongKet;
                    if (tk >= 8.5) return 4.0;
                    if (tk >= 7.0) return 3.0;
                    if (tk >= 5.5) return 2.0;
                    if (tk >= 4.0) return 1.0;
                    return 0;
                }
            }

            public string DiemChu
            {
                get
                {
                    if (DiemTongKet == null) return "";
                    float tk = (float)DiemTongKet;
                    if (tk >= 8.5) return "A";
                    if (tk >= 7.0) return "B";
                    if (tk >= 5.5) return "C";
                    if (tk >= 4.0) return "D";
                    return "F";
                }
            }
        }

        public void LoadThongTinSinhVien(string maSV)
        {
            this.currentMaSV = maSV;

            // Join thêm bảng GiangVien để lấy tên GVCN
            var thongTinSV = (from sv in context.SinhVien
                              join lop in context.LopHanhChinh on sv.MaLop equals lop.MaLop
                              join nganh in context.Nganh on lop.MaNganh equals nganh.MaNganh
                              join khoa in context.Khoa on nganh.MaKhoa equals khoa.MaKhoa
                              // Left Join Giáo viên (đề phòng lớp chưa có GVCN thì không bị lỗi)
                              join gv in context.GiangVien on lop.MaGVCN equals gv.MaGV into gvGroup
                              from gv in gvGroup.DefaultIfEmpty()
                              where sv.MaSV == maSV
                              select new
                              {
                                  sv.MaSV,
                                  sv.HoTen,
                                  lop.TenLop,
                                  nganh.TenNganh,
                                  khoa.TenKhoa,
                                  TenGVCN = (gv != null) ? gv.HoTen : "Chưa phân công"
                              }).FirstOrDefault();

            if (thongTinSV != null)
            {
                lblMaSV.Text = thongTinSV.MaSV;
                lblHoTen.Text = thongTinSV.HoTen;
                lblLop.Text = thongTinSV.TenLop;
                lblKhoa.Text = thongTinSV.TenKhoa;
                lblNganh.Text = thongTinSV.TenNganh;

                // Hiển thị tên GVCN (dùng label lblCVHT có sẵn trên giao diện)
                lblCVHT.Text = thongTinSV.TenGVCN;
            }
            LoadBangDiemSinhVien(maSV);
        }

        private void LoadBangDiemSinhVien(string maSV)
        {
            var listDiemRaw = from kq in context.KetQuaHocTap
                              join lhp in context.LopHocPhan on kq.MaLHP equals lhp.MaLHP
                              join mh in context.MonHoc on lhp.MaMon equals mh.MaMon
                              where kq.MaSV == maSV
                              select new DiemViewModel
                              {
                                  ID = kq.MaKQ,
                                  MaLHP = kq.MaLHP,
                                  MaMon = mh.MaMon,
                                  TenMon = mh.TenMon,
                                  SoTinChi = mh.SoTinChi,
                                  DiemGK = kq.DiemGK,
                                  DiemCK = kq.DiemCK
                              };

            bsDiem.DataSource = listDiemRaw.ToList();
            dgvBangDiem.DataSource = bsDiem;

            // Xóa Binding cũ
            txtDiemQT.DataBindings.Clear();
            txtDiemCK.DataBindings.Clear();
            txtSTC.DataBindings.Clear();
            txtTenMon.DataBindings.Clear();
            txtDiemThiLan1.DataBindings.Clear();
            txtDiemThiLan2.DataBindings.Clear();

            // Binding mới
            txtDiemQT.DataBindings.Add("Text", bsDiem, "DiemGK", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDiemCK.DataBindings.Add("Text", bsDiem, "DiemCK", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSTC.DataBindings.Add("Text", bsDiem, "SoTinChi", true);
            txtTenMon.DataBindings.Add("Text", bsDiem, "TenMon", true);

            TinhTongKetHocKy(bsDiem.DataSource as List<DiemViewModel>);
        }

        private void BsDiem_CurrentChanged(object sender, EventArgs e)
        {
            if (bsDiem.Current != null && !xuLyThem)
            {
                var item = (DiemViewModel)bsDiem.Current;
                cboMaMon.SelectedValue = item.MaLHP;

                // (Tùy chọn) Có thể tự động set lại comboBox1 (Học kỳ) theo môn đang chọn nếu muốn
            }
        }

        private void TinhTongKetHocKy(List<DiemViewModel> list)
        {
            if (list == null || list.Count == 0)
            {
                lblSoMon.Text = "0"; lblSTCDat.Text = "0"; lblSTCTichLuy.Text = "0";
                lblDiemHe10.Text = "0"; lblDiemHe4.Text = "0"; lblXepLoaiHK.Text = "";
                return;
            }

            int soMon = list.Count;
            lblSoMon.Text = soMon.ToString();

            // --- YÊU CẦU: CHỈ XẾP LOẠI KHI CÓ ĐỦ 5 MÔN TRỞ LÊN ---
            if (soMon < 5)
            {
                lblXepLoaiHK.Text = "Chưa đủ môn xếp loại";

                // Vẫn tính toán các chỉ số khác cho sinh viên xem
                lblSTCDat.Text = list.Where(x => (x.DiemHe4 ?? 0) >= 1).Sum(x => x.SoTinChi).ToString();
                lblSTCTichLuy.Text = list.Sum(x => x.SoTinChi).ToString();

                double td = list.Where(x => x.DiemTongKet != null).Sum(x => (double)x.DiemTongKet * x.SoTinChi);
                int tc = list.Sum(x => x.SoTinChi);
                lblDiemHe10.Text = tc > 0 ? Math.Round(td / tc, 2).ToString() : "0";

                return; // Dừng, không xếp loại
            }

            // Nếu đủ 5 môn thì tính tiếp
            int tcDat = list.Where(x => (x.DiemHe4 ?? 0) >= 1).Sum(x => x.SoTinChi);
            int tcTichLuy = list.Sum(x => x.SoTinChi);

            double tongDiem = list.Where(x => x.DiemTongKet != null).Sum(x => (double)x.DiemTongKet * x.SoTinChi);
            double tongDiem4 = list.Where(x => x.DiemHe4 != null).Sum(x => (double)x.DiemHe4 * x.SoTinChi);

            double dtb10 = tcTichLuy > 0 ? tongDiem / tcTichLuy : 0;
            double dtb4 = tcTichLuy > 0 ? tongDiem4 / tcTichLuy : 0;

            lblSTCDat.Text = tcDat.ToString();
            lblSTCTichLuy.Text = tcTichLuy.ToString();
            lblDiemHe10.Text = Math.Round(dtb10, 2).ToString();
            lblDiemHe4.Text = Math.Round(dtb4, 2).ToString();

            if (dtb4 >= 3.6) lblXepLoaiHK.Text = "Xuất sắc";
            else if (dtb4 >= 3.2) lblXepLoaiHK.Text = "Giỏi";
            else if (dtb4 >= 2.5) lblXepLoaiHK.Text = "Khá";
            else if (dtb4 >= 2.0) lblXepLoaiHK.Text = "Trung bình";
            else lblXepLoaiHK.Text = "Yếu";
        }

        #endregion

        #region 3. CÁC NÚT CHỨC NĂNG

        private void btnAdThem_SV_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);

            cboMaMon.SelectedIndex = -1;
            txtTenMon.Clear();
            txtSTC.Clear();
            txtDiemQT.Clear();
            txtDiemCK.Clear();
            txtDiemThiLan1.Clear();
            txtDiemThiLan2.Clear();
            txtGhichu.Clear();

            cboMaMon.Focus();
        }

        private void btnAdSua_SV_Click(object sender, EventArgs e)
        {
            if (bsDiem.Current == null) return;
            xuLyThem = false;
            BatTatChucNang(true);
            cboMaMon.Enabled = false;
        }

        private void btnAdLua_SV_Click(object sender, EventArgs e)
        {
            if (cboMaMon.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn môn học!");
                return;
            }

            decimal? dGK = string.IsNullOrEmpty(txtDiemQT.Text) ? null : decimal.Parse(txtDiemQT.Text);
            decimal? dCK = string.IsNullOrEmpty(txtDiemCK.Text) ? null : decimal.Parse(txtDiemCK.Text);

            try
            {
                int maLHP = (int)cboMaMon.SelectedValue;

                if (xuLyThem)
                {
                    var check = context.KetQuaHocTap.FirstOrDefault(x => x.MaSV == currentMaSV && x.MaLHP == maLHP);
                    if (check != null)
                    {
                        MessageBox.Show("Sinh viên đã có điểm môn này!");
                        return;
                    }

                    KetQuaHocTap kq = new KetQuaHocTap();
                    kq.MaSV = currentMaSV;
                    kq.MaLHP = maLHP;
                    kq.DiemGK = dGK;
                    kq.DiemCK = dCK;
                    context.KetQuaHocTap.Add(kq);
                }
                else
                {
                    var viewItem = (DiemViewModel)bsDiem.Current;
                    var kq = context.KetQuaHocTap.Find(viewItem.ID);
                    if (kq != null)
                    {
                        kq.DiemGK = dGK;
                        kq.DiemCK = dCK;
                    }
                }

                context.SaveChanges();
                MessageBox.Show("Lưu thành công!");
                BatTatChucNang(false);
                LoadBangDiemSinhVien(currentMaSV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void cboMaMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xuLyThem && cboMaMon.SelectedIndex != -1)
            {
                try
                {
                    int maLHP = (int)cboMaMon.SelectedValue;
                    var mon = (from lhp in context.LopHocPhan
                               join mh in context.MonHoc on lhp.MaMon equals mh.MaMon
                               where lhp.MaLHP == maLHP
                               select new { mh.TenMon, mh.SoTinChi }).FirstOrDefault();
                    if (mon != null)
                    {
                        txtTenMon.Text = mon.TenMon;
                        txtSTC.Text = mon.SoTinChi.ToString();
                    }
                }
                catch { }
            }
        }

        private void btnAdXoa_SV_Click(object sender, EventArgs e)
        {
            if (bsDiem.Current == null) return;
            if (MessageBox.Show("Xóa điểm môn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var item = (DiemViewModel)bsDiem.Current;
                    var kq = context.KetQuaHocTap.Find(item.ID);
                    if (kq != null)
                    {
                        context.KetQuaHocTap.Remove(kq);
                        context.SaveChanges();
                        LoadBangDiemSinhVien(currentMaSV);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        private void btnAdLamLai_SV_Click(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            bsDiem.ResetBindings(false);
        }

        private void btnQuayLai_Click_1(object sender, EventArgs e) => OnQuayLai?.Invoke();

        private void BatTatChucNang(bool mo)
        {
            btnAdLua_SV.Enabled = mo;
            btnAdLamLai_SV.Enabled = mo;
            cboMaMon.Enabled = mo;
            cboHocKy.Enabled = !mo; // Khi đang thêm/sửa thì khóa chọn học kỳ lại cho an toàn
            txtDiemQT.Enabled = mo;
            txtDiemCK.Enabled = mo;
            txtTenMon.Enabled = mo;
            txtSTC.Enabled = mo;

            txtDiemThiLan1.Enabled = false;
            txtDiemThiLan2.Enabled = false;

            txtGhichu.Enabled = mo;

            btnAdThem_SV.Enabled = !mo;
            btnAdSua_SV.Enabled = !mo;
            btnAdXoa_SV.Enabled = !mo;
        }
        #endregion

        private void cboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHocKy.SelectedValue != null)
            {
                string maHK = cboHocKy.SelectedValue.ToString();
                LoadComboBoxMonHoc(maHK);
            }
        }
    }
}