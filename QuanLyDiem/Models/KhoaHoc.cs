using System;
using System.ComponentModel.DataAnnotations;
namespace QuanLyDiem.Models
{
    public class KhoaHoc {
        [Key]
        public string MaKhoaHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        [DataType(DataType.Date)]
        public DateTime NamBatDau {get; set;}
        [DataType(DataType.Date)]
        public DateTime NamKetThuc {get; set;}
    }
}