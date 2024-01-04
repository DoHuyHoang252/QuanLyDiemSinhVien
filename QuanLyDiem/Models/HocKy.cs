using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDiem.Models
{
    public class HocKy {
        public string MaKhoaHoc { get; set; }
        [ForeignKey("MaKhoaHoc")]
        public KhoaHoc? KhoaHoc {get; set;}
        [Key]
        public string MaHocKy { get; set; }
        public string TenHocKy { get; set; }
    }
}