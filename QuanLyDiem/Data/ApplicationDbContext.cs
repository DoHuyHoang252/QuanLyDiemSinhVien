using QuanLyDiem.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDiem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<QuanLyDiem.Models.SinhVien> SinhVien { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.LopHocPhan> LopHocPhan { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.KhoaHoc> KhoaHoc { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.Khoa> Khoa { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.HocPhan> HocPhan { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.HocKy> HocKy { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.GiangVien> GiangVien { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.ChuyenNganh> ChuyenNganh { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.BangDiem> BangDiem { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.YeuCauPhucKhao> YeuCauPhucKhao { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.YeuCauSuaDiem> YeuCauSuaDiem { get; set; } = default!;
        public DbSet<QuanLyDiem.Models.User> User { get; set; } = default!;

        public async Task<User> GetUserAsync(string username, string password)
        {
            return await User.FirstOrDefaultAsync(u => u.username == username && u.password == password);
        }

        public DbSet<QuanLyDiem.Models.DiemRenLuyen> DiemRenLuyen { get; set; } = default!;
    }
}