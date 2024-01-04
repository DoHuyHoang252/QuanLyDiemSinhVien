using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDiem.Models
{
    public class SinhVien {
        [Key]
        public string MaSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public string GioiTinh { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }
        public string TinhTrang { get; set; }
        public string MaChuyenNganh { get; set; }
        [ForeignKey("MaChuyenNganh")]
        public ChuyenNganh? ChuyenNganh {get; set;}
        public string MaKhoaHoc { get; set; }
        [ForeignKey("MaKhoaHoc")]
        public KhoaHoc? KhoaHoc {get; set;}
        public double DTBTLHe10 {get; set;}
        public double DTBTLHe4 {get; set;}
        public int SoTinChiTichLuy {get; set;}
        public List<BangDiem>? BangDiem { get; set; }
        public List<HocPhan>? HocPhan { get; set; }
        public List<DiemRenLuyen>? DiemRenLuyen { get; set; }
    }
}