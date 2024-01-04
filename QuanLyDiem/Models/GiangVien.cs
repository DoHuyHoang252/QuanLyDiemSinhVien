using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDiem.Models
{
    public class GiangVien {
        [Key]
        public string MaGiangVien { get; set; }
        public string TenGiangVien { get; set; }
        public string GioiTinh { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }
        public string MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        public Khoa? Khoa {get; set;}
        public string MaChuyenNganh { get; set; }
        [ForeignKey("MaChuyenNganh")]
        public ChuyenNganh? ChuyenNganh {get; set;}
    }
}