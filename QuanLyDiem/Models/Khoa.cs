using System;
using System.ComponentModel.DataAnnotations;
namespace QuanLyDiem.Models
{
    public class Khoa {
        [Key]
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
    }
}