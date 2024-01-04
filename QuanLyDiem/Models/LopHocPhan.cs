using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDiem.Models
{
    public class LopHocPhan {
        [Key]
        public string MaLopHocPhan { get; set; }
        public string TenLopHocPhan { get; set; }
        public string MaHocPhan { get; set; }
        [ForeignKey("MaHocPhan")]
        public HocPhan? HocPhan {get; set;}
        public string MaGiangVien { get; set; }
        [ForeignKey("MaGiangVien")]
        public GiangVien? GiangVien {get; set;}
        public string MaHocKy { get; set; }
        [ForeignKey("MaHocKy")]
        public HocKy? HocKy {get; set;}
        public List<BangDiem>? BangDiem { get; set; }
    }
}