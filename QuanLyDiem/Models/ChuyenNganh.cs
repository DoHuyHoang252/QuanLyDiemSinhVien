using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDiem.Models
{
    public class ChuyenNganh {
        [Key]
        public string MaChuyenNganh { get; set; }
        public string TenChuyenNganh { get; set; }
        public string MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        public Khoa? Khoa {get; set;}
    }
}