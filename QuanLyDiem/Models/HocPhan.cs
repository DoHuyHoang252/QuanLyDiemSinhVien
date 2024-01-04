using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDiem.Models
{
    public class HocPhan {
        [Key]
        public string MaHocPhan { get; set; }
        public string TenHocPhan { get; set; }
        public int SoTinChi { get; set; }
        public string MaChuyenNganh { get; set; }
        [ForeignKey("MaChuyenNganh")]
        public ChuyenNganh? ChuyenNganh {get; set;}
    }
}