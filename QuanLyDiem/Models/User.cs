using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyDiem.Models
{
    public class User {
        [Key]
        public int UserID { get; set; }
        [MinLength(8, ErrorMessage = "Độ dài tối thiểu là 8 kí tự")]
        [MaxLength(20, ErrorMessage = "Độ dài tối thiểu là 20 kí tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
        [Display(Name = "Tên đăng nhập")]
        public string username { get; set; }
        [MinLength(8, ErrorMessage = "Độ dài tối thiểu là 8 kí tự")]
        [MaxLength(20, ErrorMessage = "Độ dài tối thiểu là 20 kí tự")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
        [StringLength(50, ErrorMessage = "Tên phải có ít nhất 2 kí tự", MinimumLength = 2)]
        [Display(Name = "Tên")]
        public string firstName { get; set; }
        [StringLength(50, ErrorMessage = "Họ phải có ít nhất 2 kí tự", MinimumLength = 2)]
        [Display(Name = "Họ")]
        public string lastName { get; set; }
    }
}