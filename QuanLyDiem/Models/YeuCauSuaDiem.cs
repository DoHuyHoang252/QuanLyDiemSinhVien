using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Plugins;
namespace QuanLyDiem.Models
{
    public class YeuCauSuaDiem {
        [Key]
        public int MaYeuCauSuaDiem { get; set; }
        public string MaGiangVien { get; set; }
        [ForeignKey("MaGiangVien")]
        public GiangVien? GiangVien{get; set;}
        public string MaSinhVien { get; set; }
        [ForeignKey("MaSinhVien")]
        public SinhVien? SinhVien{get; set;}
        public string MaHocPhan { get; set; }
        [ForeignKey("MaHocPhan")]
        public HocPhan? HocPhan {get; set;}
        public double DiemChuyenCan { get; set; }
        public BangDiem? BangDiem {get; set;}
        public double DiemKiemTra { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0, 10, ErrorMessage ="Điểm không hợp lệ")]
        public double DiemChuyenCanMoi { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0, 10, ErrorMessage ="Điểm không hợp lệ")]
        public double DiemKiemTraMoi { get; set; }
        public string LyDo { get; set; } = "";
        public string TrangThai {get; set;} = "Đang chờ xử lý";

    }
}