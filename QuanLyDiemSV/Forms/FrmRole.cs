using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyDiemSV.Data; // Namespace chứa Context và Model Role

namespace QuanLyDiemSV.Forms
{
    public partial class FrmRole : Form
    {
        // Khởi tạo Context và BindingSource
        QLDSVDbContext context = new QLDSVDbContext();
        BindingSource bsRole = new BindingSource();
        bool xuLyThem = false;

        public FrmRole()
        {
            InitializeComponent();
        }

        private void FrmRole_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LoadData();
        }

        #region 1. HÀM HỖ TRỢ & LOAD DỮ LIỆU

        private void BatTatChucNang(bool giaTri)
        {
            // Các nút thao tác
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;

            // Các ô nhập liệu (Tên biến theo file Designer mới)
            dgvRoleID.Enabled = giaTri;      // Đây là TextBox nhập ID (tên hơi lạ nhưng mình dùng đúng theo Designer)
            txtTenQuyenHan.Enabled = giaTri; // TextBox Tên quyền
            txtMota.Enabled = giaTri;        // TextBox Mô tả

            // Nút chức năng
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnThoat.Enabled = !giaTri;
        }

        private void LoadData()
        {
            try
            {
                // Lấy danh sách Quyền
                var listRole = context.Role.ToList();
                bsRole.DataSource = listRole;

                // Xóa Binding cũ
                dgvRoleID.DataBindings.Clear();
                txtTenQuyenHan.DataBindings.Clear();
                txtMota.DataBindings.Clear();

                // Tạo Binding mới (Liên kết dữ liệu từ lưới vào ô nhập)
                // 1. RoleID -> dgvRoleID
                dgvRoleID.DataBindings.Add("Text", bsRole, "RoleID", true, DataSourceUpdateMode.Never);

                // 2. RoleName -> txtTenQuyenHan
                txtTenQuyenHan.DataBindings.Add("Text", bsRole, "RoleName", true, DataSourceUpdateMode.Never);

                // 3. MoTa -> txtMota
                txtMota.DataBindings.Add("Text", bsRole, "MoTa", true, DataSourceUpdateMode.Never);

                // Hiển thị lên Grid (dgvRole)
                dgvRole.AutoGenerateColumns = false;
                dgvRole.DataSource = bsRole;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        #endregion

        #region 2. XỬ LÝ NÚT BẤM (CRUD)

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);

            // Xóa trắng các ô nhập
            dgvRoleID.Enabled = false;
            txtTenQuyenHan.Clear();
            txtMota.Clear();

            dgvRoleID.Focus();
            txtTenQuyenHan.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (bsRole.Current == null) return;

            xuLyThem = false;
            BatTatChucNang(true);

            // Khi sửa thì KHÔNG được sửa khóa chính (ID)
            dgvRoleID.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bsRole.Current == null) return;
            var role = (Role)bsRole.Current;

            if (MessageBox.Show($"Bạn có chắc muốn xóa quyền '{role.RoleName}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // Kiểm tra ràng buộc: Nếu quyền này đang có tài khoản sử dụng thì không cho xóa
                    // (Giả sử bạn đã có bảng UserAccount liên kết với Role)
                    bool dangSuDung = context.UserAccount.Any(u => u.RoleID == role.RoleID);
                    if (dangSuDung)
                    {
                        MessageBox.Show("Không thể xóa quyền này vì đang có tài khoản sử dụng!\nHãy xóa hoặc chuyển quyền các tài khoản đó trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    context.Role.Remove(role);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra nhập liệu Tên (Luôn cần)
            if (string.IsNullOrWhiteSpace(txtTenQuyenHan.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên quyền!");
                txtTenQuyenHan.Focus();
                return;
            }

            try
            {
                if (xuLyThem) // --- THÊM MỚI ---
                {
                 //ID do CSDL tự sinh, không cần lấy từ ô nhập liệu

                    Role r = new Role();
                    r.RoleName = txtTenQuyenHan.Text.Trim();
                    r.MoTa = txtMota.Text.Trim();

                    context.Role.Add(r);
                }
                else // --- CẬP NHẬT (SỬA) ---
                {
                    

                    if (!int.TryParse(dgvRoleID.Text.Trim(), out int roleID))
                    {
                        MessageBox.Show("Không xác định được ID để sửa!");
                        return;
                    }

                    var r = context.Role.Find(roleID);
                    if (r != null)
                    {
                        r.RoleName = txtTenQuyenHan.Text.Trim();
                        r.MoTa = txtMota.Text.Trim();

                        context.Role.Update(r);
                    }
                }

                context.SaveChanges();
                LoadData();
                BatTatChucNang(false);
                MessageBox.Show("Lưu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            bsRole.ResetBindings(false); // Hủy bỏ thao tác, load lại giá trị cũ lên textbox
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng form này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #endregion
    }
}