using System.Linq.Expressions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace QuanLyDiem.Models
{
    public class DiemRenLuyen
    {
        [Key]
        public int MaDiemRenLuyen { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm")]
        [Range(0, 100, ErrorMessage ="Điểm không hợp lệ")]
        public int DiemRL {get; set;}
        public string MaSinhVien { get; set; }
        [ForeignKey("MaSinhVien")]
        public SinhVien? SinhVien {get; set;}
        public string MaHocKy { get; set; }
        [ForeignKey("MaHocKy")]
        public HocKy? HocKy {get; set;}
    }
}