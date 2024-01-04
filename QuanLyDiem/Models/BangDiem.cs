using System.Linq.Expressions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
namespace QuanLyDiem.Models
{
    public class BangDiem : Helper {
        [Key]
        public int MaBangDiem { get; set; }
        public string MaSinhVien { get; set; }
        [ForeignKey("MaSinhVien")]
        [JsonIgnore]
        public SinhVien? SinhVien {get; set;}
        public string TenSinhVien {get; set;} = "";
        public string MaHocPhan { get; set; }
        [ForeignKey("MaHocPhan")]
        public HocPhan? HocPhan {get; set;}
        public int SoTinChi {get; set;}
        public string MaLopHocPhan { get; set; }
        [ForeignKey("MaLopHocPhan")]
        public LopHocPhan? LopHocPhan {get; set;}
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0, 10, ErrorMessage ="Điểm không hợp lệ")]
        public double DiemChuyenCan { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0, 10, ErrorMessage ="Điểm không hợp lệ")]
        public double DiemKiemTra { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0, 10, ErrorMessage ="Điểm không hợp lệ")]
        public double DiemThi { get; set; }
        public double DiemTong =>  Math.Round(DiemChuyenCan*0.1 + DiemKiemTra*0.3 + DiemThi*0.6,2);
        public double DiemTongHe4 => ChuyenDoiDiemHe4(DiemTong);
        public string DiemChu => ChuyenDoiDiemChu(DiemTong);
        public string TenHocPhan { get; set; } ="";
    }
}